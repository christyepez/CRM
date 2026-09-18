using System.Data;
using System.Text.Json;
using CRM.Migration.Application;
using Microsoft.EntityFrameworkCore;

namespace CRM.Runtime.Sql;

// Infrastructure-only helper. Application services continue to consume the existing typed ports.
public sealed class SqlRecordStore<T> where T : class
{
    private readonly Func<FoundationDbContext> createContext;
    private readonly string tenant;
    private readonly string store;
    private readonly Func<T, string> getId;
    private readonly Func<T, JsonElement> encode;
    private readonly Func<JsonElement, T> decode;

    public SqlRecordStore(Func<FoundationDbContext> createContext, string tenant, string store,
        Func<T, string> getId, Func<T, JsonElement>? encode = null, Func<JsonElement, T>? decode = null)
    {
        SnapshotValidation.ValidateTenant(tenant);
        if (!SnapshotValidation.Stores.Contains(store)) throw new ArgumentException("Unknown CRM store.");
        this.createContext = createContext;
        this.tenant = tenant;
        this.store = store;
        this.getId = getId;
        this.encode = encode ?? (item => JsonSerializer.SerializeToElement(item, SnapshotValidation.JsonOptions));
        this.decode = decode ?? (payload => payload.Deserialize<T>(SnapshotValidation.JsonOptions)
            ?? throw new InvalidOperationException("Invalid stored CRM record."));
    }

    public async Task<IReadOnlyCollection<T>> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        await using var db = createContext();
        var rows = await db.Records.AsNoTracking().Where(x => x.TenantId == tenant && x.Store == store)
            .OrderBy(x => x.Id).ToListAsync(cancellationToken);
        return rows.Select(Read).ToArray();
    }

    public async Task<T?> ReadByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ValidateId(id);
        await using var db = createContext();
        var row = await db.Records.AsNoTracking().SingleOrDefaultAsync(
            x => x.TenantId == tenant && x.Store == store && x.Id == id, cancellationToken);
        return row is null ? null : Read(row);
    }

    public async Task<T> SaveAsync(T item, CancellationToken cancellationToken = default)
    {
        var id = getId(item);
        var records = SnapshotValidation.ValidateRecords([new(store, id, encode(item))], false);
        // Decode before writing to catch unsupported/invalid domain rehydration.
        var canonicalItem = decode(records[0].Payload);
        if (getId(canonicalItem) != id) throw new ArgumentException("CRM identity changed during encoding.");
        await using var db = createContext();
        await using var tx = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        var row = await db.Records.SingleOrDefaultAsync(
            x => x.TenantId == tenant && x.Store == store && x.Id == id, cancellationToken);
        if (row is null)
        {
            row = new() { TenantId = tenant, Store = store, Id = id };
            db.Records.Add(row);
        }
        else if (!string.Equals(row.Id, id, StringComparison.Ordinal))
            throw new ArgumentException("Existing CRM identity casing must be preserved.");
        row.PayloadJson = records[0].Payload.GetRawText();
        await db.SaveChangesAsync(cancellationToken);
        await tx.CommitAsync(cancellationToken);
        return canonicalItem;
    }

    private T Read(FoundationRow row)
    {
        using var doc = JsonDocument.Parse(row.PayloadJson);
        var record = SnapshotValidation.ValidateRecords([new(store, row.Id, doc.RootElement)], false)[0];
        var item = decode(record.Payload);
        if (getId(item) != row.Id) throw new InvalidOperationException("Stored CRM identity mismatch.");
        return item;
    }

    private static void ValidateId(string id)
    {
        if (string.IsNullOrWhiteSpace(id) || id.Length > 128 || id != id.Trim() || id.Any(char.IsControl))
            throw new ArgumentException("Invalid CRM identity.");
    }
}
