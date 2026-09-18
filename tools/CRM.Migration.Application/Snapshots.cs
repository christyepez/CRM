using System.Collections.Frozen;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CRM.Migration.Application;

public sealed record SnapshotRecord(string Store, string Id, JsonElement Payload);
public sealed record CrmSnapshot(int SchemaVersion, string TenantId, DateTimeOffset CapturedAtUtc,
    IReadOnlyList<SnapshotRecord> Records, string DataSha256);
public sealed record ImportResult(int Records, bool AlreadyPresent);

public interface ISnapshotRepository
{
    Task<IReadOnlyList<SnapshotRecord>> ReadAsync(CancellationToken cancellationToken);
    Task<ImportResult> ImportIntoEmptyAsync(IReadOnlyList<SnapshotRecord> records, CancellationToken cancellationToken);
}

public static class SnapshotValidation
{
    // Contract names, not visual configuration. New kinds require a schema review.
    public static readonly IReadOnlySet<string> Stores = new[]
    { "LeadPreview", "AccountPreview", "Account", "ContactPreview", "CampaignPreview",
      "Activity", "Opportunity", "Case", "Interaction", "Note", "Segment", "Assignment",
      "DocumentMetadata", "Tag" }.ToFrozenSet(StringComparer.Ordinal);
    public static JsonSerializerOptions JsonOptions { get; } = new(JsonSerializerDefaults.Web)
    { PropertyNameCaseInsensitive = false, UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow, MaxDepth = 32 };

    public static void ValidateTenant(string tenant)
    {
        if (string.IsNullOrWhiteSpace(tenant) || tenant.Length > 64 ||
            tenant.Any(c => !char.IsAsciiLetterOrDigit(c) && c != '-' && c != '_'))
            throw new ArgumentException("Invalid tenant identifier.");
    }

    public static IReadOnlyList<SnapshotRecord> ValidateRecords(IReadOnlyList<SnapshotRecord>? records, bool allowEmpty)
    {
        if (records is null || records.Count > 100_000 || (!allowEmpty && records.Count == 0))
            throw new ArgumentException("Invalid snapshot record count.");
        var keys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var result = new List<SnapshotRecord>();
        long bytes = 0;
        foreach (var record in records)
        {
            if (record is null || !Stores.Contains(record.Store) || string.IsNullOrWhiteSpace(record.Id) ||
                record.Id.Length > 128 || record.Id != record.Id.Trim() || record.Id.Any(char.IsControl))
                throw new ArgumentException("Invalid snapshot record identity.");
            if (!keys.Add(record.Store + "\0" + record.Id))
                throw new ArgumentException("Duplicate snapshot record identity.");
            if (record.Payload.ValueKind != JsonValueKind.Object ||
                !record.Payload.TryGetProperty("id", out var id) || id.ValueKind != JsonValueKind.String ||
                !string.Equals(id.GetString(), record.Id, StringComparison.Ordinal))
                throw new ArgumentException("Payload identity does not match its envelope.");
            var canonical = Canonicalize(record.Payload);
            var size = Encoding.UTF8.GetByteCount(canonical);
            bytes += size;
            if (size > 256_000 || bytes > 16_000_000)
                throw new ArgumentException("Snapshot payload size limit exceeded.");
            using var doc = JsonDocument.Parse(canonical, new JsonDocumentOptions { MaxDepth = 32 });
            result.Add(record with { Payload = doc.RootElement.Clone() });
        }
        return result.OrderBy(r => r.Store, StringComparer.Ordinal).ThenBy(r => r.Id, StringComparer.Ordinal).ToArray();
    }

    public static string Digest(string tenant, IReadOnlyList<SnapshotRecord> records)
    {
        ValidateTenant(tenant);
        var ordered = ValidateRecords(records, true);
        var data = JsonSerializer.Serialize(new { schemaVersion = 1, tenantId = tenant, records = ordered }, JsonOptions);
        using var doc = JsonDocument.Parse(data);
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(Canonicalize(doc.RootElement))));
    }

    public static IReadOnlyList<SnapshotRecord> Validate(CrmSnapshot snapshot, string tenant, bool allowEmpty)
    {
        ValidateTenant(tenant);
        if (snapshot is null || snapshot.SchemaVersion != 1 || snapshot.TenantId != tenant ||
            snapshot.CapturedAtUtc.Offset != TimeSpan.Zero || snapshot.CapturedAtUtc == default)
            throw new ArgumentException("Invalid snapshot header or tenant.");
        var records = ValidateRecords(snapshot.Records, allowEmpty);
        byte[] supplied;
        try { supplied = Convert.FromHexString(snapshot.DataSha256 ?? ""); }
        catch (FormatException) { throw new ArgumentException("Invalid snapshot checksum."); }
        var expected = Convert.FromHexString(Digest(tenant, records));
        if (!CryptographicOperations.FixedTimeEquals(supplied, expected))
            throw new ArgumentException("Snapshot checksum mismatch.");
        return records;
    }

    public static string Canonicalize(JsonElement element)
    {
        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { MaxDepth = 32 }))
            Write(writer, element);
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    private static void Write(Utf8JsonWriter writer, JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var properties = element.EnumerateObject().ToArray();
            if (properties.Select(p => p.Name).Distinct(StringComparer.OrdinalIgnoreCase).Count() != properties.Length)
                throw new ArgumentException("Duplicate or ambiguous JSON property.");
            writer.WriteStartObject();
            foreach (var property in properties.OrderBy(p => p.Name, StringComparer.Ordinal))
            { writer.WritePropertyName(property.Name); Write(writer, property.Value); }
            writer.WriteEndObject();
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            writer.WriteStartArray();
            foreach (var item in element.EnumerateArray()) Write(writer, item);
            writer.WriteEndArray();
        }
        else element.WriteTo(writer);
    }
}

public sealed class SnapshotMigrationService(ISnapshotRepository repository, string tenant)
{
    public async Task<CrmSnapshot> ExportAsync(CancellationToken cancellationToken = default)
    {
        SnapshotValidation.ValidateTenant(tenant);
        var records = SnapshotValidation.ValidateRecords(await repository.ReadAsync(cancellationToken), true);
        return new(1, tenant, DateTimeOffset.UtcNow, records, SnapshotValidation.Digest(tenant, records));
    }

    public Task<ImportResult> ImportAsync(CrmSnapshot snapshot, bool allowEmpty = false,
        CancellationToken cancellationToken = default)
    {
        var records = SnapshotValidation.Validate(snapshot, tenant, allowEmpty);
        return repository.ImportIntoEmptyAsync(records, cancellationToken);
    }
}
