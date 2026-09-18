using System.Text.Json;
using CRM.Migration.Application;
using CRM.Migration.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CRM.Migration.Tests;

public sealed class SnapshotTests
{
    private static SnapshotRecord Record(string id = "001", string store = "LeadPreview", string? json = null) =>
        new(store, id, JsonSerializer.Deserialize<JsonElement>(json ?? "{\"id\":\"" + id + "\",\"name\":\"Synthetic\"}"));
    private static CrmSnapshot Snapshot(params SnapshotRecord[] records) =>
        new(1, "default", DateTimeOffset.UtcNow, records, SnapshotValidation.Digest("default", records));

    [Fact] public void ValidSnapshotRetainsIdentityAndPayload()
    {
        var records = SnapshotValidation.Validate(Snapshot(Record()), "default", false);
        Assert.Equal("001", Assert.Single(records).Id);
        Assert.Equal("Synthetic", records[0].Payload.GetProperty("name").GetString());
    }
    [Fact] public void DigestIgnoresPropertyAndRecordOrder()
    {
        var a = Record(json: "{\"id\":\"001\",\"metadata\":{\"b\":2,\"a\":1}}");
        var b = Record(json: "{\"metadata\":{\"a\":1,\"b\":2},\"id\":\"001\"}");
        Assert.Equal(SnapshotValidation.Digest("default", [a, Record("002")]),
            SnapshotValidation.Digest("default", [Record("002"), b]));
    }
    [Fact] public void DigestIncludesTenantAndPayload()
    {
        Assert.NotEqual(SnapshotValidation.Digest("default", [Record()]), SnapshotValidation.Digest("other", [Record()]));
        Assert.NotEqual(SnapshotValidation.Digest("default", [Record()]), SnapshotValidation.Digest("default", [Record(json:"{\"id\":\"001\",\"name\":\"Changed\"}")]));
    }
    [Theory]
    [InlineData("")][InlineData(" ")][InlineData("a/b")][InlineData("é")][InlineData("tenant\n")]
    public void InvalidTenantRejected(string tenant) => Assert.Throws<ArgumentException>(() => SnapshotValidation.ValidateTenant(tenant));
    [Fact] public void LongTenantRejected() => Assert.Throws<ArgumentException>(() => SnapshotValidation.ValidateTenant(new string('a', 65)));
    [Theory]
    [InlineData("id", "Unknown", "{\"id\":\"id\"}")]
    [InlineData("", "LeadPreview", "{\"id\":\"\"}")]
    [InlineData("id ", "LeadPreview", "{\"id\":\"id \"}")]
    [InlineData("id", "LeadPreview", "[]")]
    [InlineData("id", "LeadPreview", "{\"id\":42}")]
    [InlineData("id", "LeadPreview", "{\"id\":\"other\"}")]
    [InlineData("id", "LeadPreview", "{\"name\":\"MissingId\"}")]
    [InlineData("id", "LeadPreview", "{\"id\":\"id\",\"Id\":\"id\"}")]
    [InlineData("id", "LeadPreview", "{\"id\":\"id\",\"meta\":{\"x\":1,\"x\":2}}")]
    public void InvalidPayloadRejected(string id, string store, string json) =>
        Assert.Throws<ArgumentException>(() => SnapshotValidation.ValidateRecords([Record(id, store, json)], false));
    [Fact] public void DuplicateIdentityRejectedIgnoringCase() =>
        Assert.Throws<ArgumentException>(() => SnapshotValidation.ValidateRecords([Record("A"), Record("a")], false));
    [Fact] public void SameIdAcrossStoresAllowed() => Assert.Equal(2, SnapshotValidation.ValidateRecords([Record(), Record(store:"ContactPreview")],false).Count);
    [Theory][InlineData("DocumentMetadata")][InlineData("Tag")]
    public async Task AdditionalFoundationStoresRoundTripWithoutLosingPayload(string store)
    {
        await using var fixture = await SqliteFixture.CreateAsync();
        var service = new SnapshotMigrationService(fixture.Repository,"default");
        var snapshot = Snapshot(Record(store:store));
        Assert.False((await service.ImportAsync(snapshot)).AlreadyPresent);
        Assert.Equal(snapshot.DataSha256,(await service.ExportAsync()).DataSha256);
    }
    [Fact] public void EmptyRequiresExplicitPermission()
    {
        Assert.Throws<ArgumentException>(() => SnapshotValidation.ValidateRecords([], false));
        Assert.Empty(SnapshotValidation.ValidateRecords([], true));
    }
    [Fact] public void NullRecordsRejected() => Assert.Throws<ArgumentException>(() => SnapshotValidation.ValidateRecords(null, true));
    [Fact] public void ExcessivePayloadRejected() => Assert.Throws<ArgumentException>(() =>
        SnapshotValidation.ValidateRecords([Record(json:JsonSerializer.Serialize(new {id="001",content=new string('x',256_001)}))],false));
    [Fact] public void WrongSchemaRejected() => Assert.Throws<ArgumentException>(() => SnapshotValidation.Validate(Snapshot(Record()) with {SchemaVersion=2},"default",false));
    [Fact] public void WrongTenantRejected() => Assert.Throws<ArgumentException>(() => SnapshotValidation.Validate(Snapshot(Record()),"other",false));
    [Fact] public void NonUtcTimestampRejected() => Assert.Throws<ArgumentException>(() => SnapshotValidation.Validate(Snapshot(Record()) with {CapturedAtUtc=DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(1))},"default",false));
    [Theory][InlineData("")][InlineData("BAD")][InlineData("00")]
    public void WrongChecksumRejected(string checksum) => Assert.Throws<ArgumentException>(() => SnapshotValidation.Validate(Snapshot(Record()) with {DataSha256=checksum},"default",false));
    [Fact] public void ExtraEnvelopeFieldsRejected() => Assert.Throws<JsonException>(() =>
        JsonSerializer.Deserialize<CrmSnapshot>("{\"schemaVersion\":1,\"unexpected\":true}",SnapshotValidation.JsonOptions));

    [Fact] public async Task ApplicationValidatesBeforeRepositoryWrites()
    {
        var repo = new FakeRepository();
        var service = new SnapshotMigrationService(repo,"default");
        await Assert.ThrowsAsync<ArgumentException>(()=>service.ImportAsync(Snapshot(Record()) with {DataSha256="00"}));
        Assert.Equal(0,repo.Imports);
        Assert.Equal(1,(await service.ImportAsync(Snapshot(Record()))).Records);
        Assert.Equal(1,repo.Imports);
        Assert.Single((await service.ExportAsync()).Records);
    }
    [Fact] public async Task EmptyExportValidatesAndCancellationPropagates()
    {
        var repo = new FakeRepository();
        var service = new SnapshotMigrationService(repo,"default");
        Assert.Empty(SnapshotValidation.Validate(await service.ExportAsync(),"default",true));
        using var cancelled = new CancellationTokenSource(); cancelled.Cancel();
        await Assert.ThrowsAsync<OperationCanceledException>(()=>service.ExportAsync(cancelled.Token));
    }
    [Fact] public async Task RelationalImportRoundTripsIsIdempotentAndRefusesOverwrite()
    {
        await using var fixture = await SqliteFixture.CreateAsync();
        var service = new SnapshotMigrationService(fixture.Repository,"default");
        var snapshot = Snapshot(Record(), Record("002", "ContactPreview"));
        Assert.False((await service.ImportAsync(snapshot)).AlreadyPresent);
        Assert.True((await service.ImportAsync(snapshot)).AlreadyPresent);
        var exported = await service.ExportAsync();
        Assert.Equal(snapshot.DataSha256, exported.DataSha256);
        await Assert.ThrowsAsync<InvalidOperationException>(()=>service.ImportAsync(Snapshot(Record("003"))));
        Assert.Equal(snapshot.DataSha256,(await service.ExportAsync()).DataSha256);
    }
    [Fact] public async Task RelationalRepositorySeparatesTenantCaseAndRollsBackCancelledImport()
    {
        await using var fixture = await SqliteFixture.CreateAsync();
        var first = new SnapshotMigrationService(fixture.Repository,"default");
        await first.ImportAsync(Snapshot(Record()));
        var other = new SqlSnapshotRepository(fixture.Options,"Default");
        Assert.Empty(await other.ReadAsync(default));
        using var cancelled = new CancellationTokenSource(); cancelled.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(()=>other.ImportIntoEmptyAsync([Record()],cancelled.Token));
        Assert.Empty(await other.ReadAsync(default));
    }

    private sealed class FakeRepository : ISnapshotRepository
    {
        private IReadOnlyList<SnapshotRecord> records=[];
        public int Imports {get;private set;}
        public Task<IReadOnlyList<SnapshotRecord>> ReadAsync(CancellationToken ct){ct.ThrowIfCancellationRequested();return Task.FromResult(records);}
        public Task<ImportResult> ImportIntoEmptyAsync(IReadOnlyList<SnapshotRecord> value,CancellationToken ct){ct.ThrowIfCancellationRequested();Imports++;records=value;return Task.FromResult(new ImportResult(value.Count,false));}
    }
    private sealed class SqliteFixture : IAsyncDisposable
    {
        private readonly SqliteConnection connection;
        public DbContextOptions<SnapshotDbContext> Options {get;}
        public SqlSnapshotRepository Repository => new(Options,"default");
        private SqliteFixture(SqliteConnection conn){connection=conn;Options=new DbContextOptionsBuilder<SnapshotDbContext>().UseSqlite(conn).Options;}
        public static async Task<SqliteFixture> CreateAsync()
        {
            var conn=new SqliteConnection("Data Source=:memory:"); await conn.OpenAsync();
            conn.CreateCollation("Latin1_General_100_BIN2",StringComparer.Ordinal.Compare);
            var fixture=new SqliteFixture(conn);
            await using var db=new SnapshotDbContext(fixture.Options); await db.Database.EnsureCreatedAsync();
            return fixture;
        }
        public ValueTask DisposeAsync()=>connection.DisposeAsync();
    }
}
