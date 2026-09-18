using System.Security.Cryptography;
using System.Text.Json;
using CRM.Application.Persistence;
using CRM.Migration.Application;
using CRM.Migration.Infrastructure;
using CRM.Runtime.Sql;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// Explicit synthetic acceptance harness, not a runtime initializer. Never targets existing databases.
if (args.Length != 1 || args[0] != "provision-and-test-confirmed") return 2;
var adminValue = Environment.GetEnvironmentVariable("CRM_SQL_ACCEPTANCE_ADMIN");
if (string.IsNullOrWhiteSpace(adminValue)) return 2;
var database = "CrmMigration_Acceptance_" + DateTime.UtcNow.ToString("yyyyMMddHHmmss") + "_" + Guid.NewGuid().ToString("N")[..8];
var login = "CrmAcceptance_" + Guid.NewGuid().ToString("N");
var password = "Aa1!" + Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
bool loginCreated = false;
try
{
    var adminBuilder = new SqlConnectionStringBuilder(adminValue);
    if (adminBuilder.DataSource != "tcp:127.0.0.1,1433" || adminBuilder.InitialCatalog != "master") return 2;
    await using var admin = new SqlConnection(adminBuilder.ConnectionString);
    await admin.OpenAsync();
    await Execute(admin, $"CREATE DATABASE [{database}];");
    var dbAdminBuilder = new SqlConnectionStringBuilder(adminBuilder.ConnectionString) { InitialCatalog = database };
    await using (var provision = new SqlConnection(dbAdminBuilder.ConnectionString))
    {
        await provision.OpenAsync();
        foreach (var path in new[] { "tools/CRM.Migration.Infrastructure/sql/001_snapshot_records.sql", "tools/CRM.Runtime.Sql/sql/002_foundation_records.sql" })
        {
            var script = await File.ReadAllTextAsync(path);
            await Execute(provision, script);
            await Execute(provision, script); // Idempotent schema application.
        }
    }
    await using (var create = admin.CreateCommand())
    {
        create.CommandText = $"DECLARE @statement nvarchar(max) = N'CREATE LOGIN [{login}] WITH PASSWORD = ' + QUOTENAME(@password, CHAR(39)) + N', CHECK_POLICY = ON;'; EXEC(@statement);";
        create.Parameters.AddWithValue("@password", password);
        await create.ExecuteNonQueryAsync(); loginCreated = true;
    }
    await using (var provision = new SqlConnection(dbAdminBuilder.ConnectionString))
    {
        await provision.OpenAsync();
        await Execute(provision, $"CREATE USER [{login}] FOR LOGIN [{login}]; GRANT SELECT, INSERT, UPDATE ON SCHEMA::crm TO [{login}];");
    }
    var user = new SqlConnectionStringBuilder(adminBuilder.ConnectionString)
    { InitialCatalog = database, UserID = login, Password = password, Pooling = false };
    var options = new DbContextOptionsBuilder<FoundationDbContext>().UseSqlServer(user.ConnectionString).Options;
    FoundationDbContext Db() => new(options);
    var previews = new SqlLeadFoundationStore(Db, "Tenant");
    var item = new CrmFoundationPreviewItemContract("synthetic-001", "Lead", "Synthetic", "Preview", DateTimeOffset.UtcNow,
        new Dictionary<string, string> { ["internal"] = "preserved" });
    await previews.SavePreviewAsync(item);
    Require((await new SqlLeadFoundationStore(Db, "Tenant").GetPreviewByIdAsync("SYNTHETIC-001"))?.Metadata["internal"] == "preserved");
    Require((await new SqlLeadFoundationStore(Db, "tenant").GetPreviewAsync()).Count == 0);
    Require((await new SqlContactFoundationStore(Db, "Tenant").GetPreviewAsync()).Count == 0);
    await previews.SavePreviewAsync(item with { DisplayName = "Updated" });
    Require((await previews.GetPreviewAsync()).Single().DisplayName == "Updated");
    // Prove least-privilege user cannot bulk-delete even by bypassing the adapter.
    await using (var restricted = new SqlConnection(user.ConnectionString))
    {
        await restricted.OpenAsync();
        bool refused = false;
        try { await Execute(restricted, "DELETE FROM crm.FoundationRecords;"); }
        catch (SqlException exception) when (exception.Number == 229) { refused = true; }
        Require(refused);
    }
    var snapshotOptions = new DbContextOptionsBuilder<SnapshotDbContext>().UseSqlServer(user.ConnectionString).Options;
    var service = new SnapshotMigrationService(new SqlSnapshotRepository(snapshotOptions, "Tenant"), "Tenant");
    var record = new SnapshotRecord("LeadPreview", "snapshot-001", JsonSerializer.SerializeToElement(new { id = "snapshot-001", name = "Synthetic" }));
    var snapshot = new CrmSnapshot(1, "Tenant", DateTimeOffset.UtcNow, [record], SnapshotValidation.Digest("Tenant", [record]));
    Require(!(await service.ImportAsync(snapshot)).AlreadyPresent);
    Require((await service.ImportAsync(snapshot)).AlreadyPresent);
    Require((await service.ExportAsync()).DataSha256 == snapshot.DataSha256);
    var other = record with { Payload = JsonSerializer.SerializeToElement(new { id = "snapshot-001", name = "Different" }) };
    bool overwriteRefused = false;
    try { await service.ImportAsync(snapshot with { Records = [other], DataSha256 = SnapshotValidation.Digest("Tenant", [other]) }); }
    catch (InvalidOperationException) { overwriteRefused = true; }
    Require(overwriteRefused);
    Require((await service.ExportAsync()).DataSha256 == snapshot.DataSha256);
    Console.WriteLine($"PASS: SQL Server synthetic acceptance; database={database}; tenant/store isolation, durable reopen, upsert, least privilege, snapshot replay and no-overwrite validated.");
    return 0;
}
catch
{
    Console.Error.WriteLine($"FAIL: SQL acceptance did not complete; isolated database={database}; existing databases unchanged. Provider details suppressed.");
    return 1;
}
finally
{
    if (loginCreated)
    {
        try
        {
            await using var admin = new SqlConnection(adminValue);
            await admin.OpenAsync(); await Execute(admin, $"ALTER LOGIN [{login}] DISABLE;");
        }
        catch { Console.Error.WriteLine("WARNING: temporary acceptance login cleanup requires administrator review."); }
    }
}

static async Task Execute(SqlConnection connection, string sql)
{
    await using var command = connection.CreateCommand(); command.CommandText = sql;
    command.CommandTimeout = 60; await command.ExecuteNonQueryAsync();
}
static void Require(bool condition)
{
    if (!condition) throw new InvalidOperationException("Acceptance assertion failed.");
}
