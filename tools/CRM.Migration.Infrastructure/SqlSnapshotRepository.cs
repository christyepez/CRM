using System.Data;
using System.Text.Json;
using CRM.Migration.Application;
using Microsoft.EntityFrameworkCore;

namespace CRM.Migration.Infrastructure;

public sealed class SnapshotRow
{
    public string TenantId { get; set; } = "";
    public string Store { get; set; } = "";
    public string Id { get; set; } = "";
    public string Payload { get; set; } = "";
}

public sealed class SnapshotDbContext(DbContextOptions<SnapshotDbContext> options) : DbContext(options)
{
    public DbSet<SnapshotRow> Records => Set<SnapshotRow>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var row = modelBuilder.Entity<SnapshotRow>();
        row.ToTable("MigrationSnapshotRecords", "crm");
        row.HasKey(x => new { x.TenantId, x.Store, x.Id });
        row.Property(x => x.TenantId).HasMaxLength(64).UseCollation("Latin1_General_100_BIN2");
        row.Property(x => x.Store).HasMaxLength(64).UseCollation("Latin1_General_100_BIN2");
        row.Property(x => x.Id).HasMaxLength(128);
        row.Property(x => x.Payload).IsRequired();
    }
}

public sealed class SqlSnapshotRepository(DbContextOptions<SnapshotDbContext> options, string tenant) : ISnapshotRepository
{
    public async Task<IReadOnlyList<SnapshotRecord>> ReadAsync(CancellationToken cancellationToken)
    {
        SnapshotValidation.ValidateTenant(tenant);
        await using var db = new SnapshotDbContext(options);
        var rows = await db.Records.AsNoTracking().Where(r => r.TenantId == tenant)
            .OrderBy(r => r.Store).ThenBy(r => r.Id).ToListAsync(cancellationToken);
        return rows.Select(r => new SnapshotRecord(r.Store, r.Id,
            JsonSerializer.Deserialize<JsonElement>(r.Payload))).ToArray();
    }

    public async Task<ImportResult> ImportIntoEmptyAsync(IReadOnlyList<SnapshotRecord> records, CancellationToken cancellationToken)
    {
        SnapshotValidation.ValidateTenant(tenant);
        var validated = SnapshotValidation.ValidateRecords(records, true);
        await using var db = new SnapshotDbContext(options);
        // Serializable protects the empty-range check against simultaneous imports.
        await using var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
        var existing = await db.Records.AsNoTracking().Where(r => r.TenantId == tenant).ToListAsync(cancellationToken);
        if (existing.Count != 0)
        {
            var old = existing.Select(r => new SnapshotRecord(r.Store, r.Id,
                JsonSerializer.Deserialize<JsonElement>(r.Payload))).ToArray();
            if (SnapshotValidation.Digest(tenant, old) != SnapshotValidation.Digest(tenant, validated))
                throw new InvalidOperationException("Target tenant already contains different data; overwrite refused.");
            await transaction.CommitAsync(cancellationToken);
            return new(validated.Count, true);
        }
        db.Records.AddRange(validated.Select(r => new SnapshotRow
        { TenantId = tenant, Store = r.Store, Id = r.Id, Payload = SnapshotValidation.Canonicalize(r.Payload) }));
        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new(validated.Count, false);
    }
}
