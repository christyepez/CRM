using Microsoft.EntityFrameworkCore;

namespace CRM.Runtime.Sql;

public sealed class FoundationRow
{
    public string TenantId { get; set; } = "";
    public string Store { get; set; } = "";
    public string Id { get; set; } = "";
    public string PayloadJson { get; set; } = "";
}

public sealed class FoundationDbContext(DbContextOptions<FoundationDbContext> options) : DbContext(options)
{
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
