using Microsoft.EntityFrameworkCore;

namespace CRM.Runtime.Sql;

public sealed class FoundationRow
{
    public string TenantId { get; set; } = "";
    public string Store { get; set; } = "";
    public string Id { get; set; } = "";
    public string PayloadJson { get; set; } = "";
}

public sealed class FoundationDbContext : DbContext
{
    public FoundationDbContext(DbContextOptions<FoundationDbContext> options) : base(options)
    {
        if (Database.IsSqlServer())
        {
            var catalog = Database.GetDbConnection().Database;
            if (!catalog.StartsWith("CrmMigration_", StringComparison.Ordinal) || catalog.Length <= 13)
                throw new InvalidOperationException("SQL candidate requires a dedicated CrmMigration_ acceptance database.");
        }
    }
    public DbSet<FoundationRow> Records => Set<FoundationRow>();
    protected override void OnModelCreating(ModelBuilder model)
    {
        var row = model.Entity<FoundationRow>();
        row.ToTable("FoundationRecords", "crm");
        row.HasKey(x => new { x.TenantId, x.Store, x.Id });
        row.Property(x => x.TenantId).HasMaxLength(64).UseCollation("Latin1_General_100_BIN2");
        row.Property(x => x.Store).HasMaxLength(64).UseCollation("Latin1_General_100_BIN2");
        row.Property(x => x.Id).HasMaxLength(128).UseCollation("Latin1_General_100_CI_AS");
        row.Property(x => x.PayloadJson).IsRequired();
    }
}
