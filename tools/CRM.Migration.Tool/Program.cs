using System.Text.Json;
using CRM.Migration.Application;
using CRM.Migration.Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

if (args.Length != 3 || args[0] is not ("validate" or "export" or "import-confirmed"))
{
    Console.Error.WriteLine("Usage: <validate|export|import-confirmed> <tenant> <snapshot-file>. Import writes an EMPTY migration tenant only.");
    return 2;
}
try
{
    var tenant = args[1];
    SnapshotValidation.ValidateTenant(tenant);
    CrmSnapshot? snapshot = null;
    if (args[0] != "export")
    {
        var file = new FileInfo(args[2]);
        if (!file.Exists || file.Length > 20_000_000) throw new ArgumentException("Invalid snapshot file.");
        await using var input = file.OpenRead();
        snapshot = await JsonSerializer.DeserializeAsync<CrmSnapshot>(input, SnapshotValidation.JsonOptions)
            ?? throw new ArgumentException("Missing snapshot.");
        SnapshotValidation.Validate(snapshot, tenant, false);
    }
    if (args[0] == "validate")
    {
        Console.WriteLine(JsonSerializer.Serialize(new { validated = true, records = snapshot!.Records.Count }));
        return 0;
    }
    var connection = Environment.GetEnvironmentVariable("CRM_MIGRATION_CONNECTION")
        ?? throw new ArgumentException("Migration connection is not configured.");
    var parsed = new SqlConnectionStringBuilder(connection);
    if (!parsed.InitialCatalog.StartsWith("CrmMigration_", StringComparison.Ordinal) ||
        parsed.InitialCatalog.Length <= "CrmMigration_".Length)
        throw new ArgumentException("Only dedicated CrmMigration_ databases are allowed.");
    var options = new DbContextOptionsBuilder<SnapshotDbContext>().UseSqlServer(connection).Options;
    var service = new SnapshotMigrationService(new SqlSnapshotRepository(options, tenant), tenant);
    if (args[0] == "export")
    {
        var output = await service.ExportAsync();
        await using var file = new FileStream(args[2], FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await JsonSerializer.SerializeAsync(file, output, SnapshotValidation.JsonOptions);
        Console.WriteLine(JsonSerializer.Serialize(new { exported = true, records = output.Records.Count }));
    }
    else
    {
        var result = await service.ImportAsync(snapshot!);
        Console.WriteLine(JsonSerializer.Serialize(new { imported = result.Records, alreadyPresent = result.AlreadyPresent }));
    }
    return 0;
}
catch (ArgumentException)
{
    Console.Error.WriteLine("Validation failed; snapshot identity, scope, schema, integrity or configuration is invalid.");
    return 3;
}
catch (InvalidOperationException)
{
    Console.Error.WriteLine("Operation refused; existing state is incompatible. No overwrite permitted.");
    return 4;
}
catch (Exception)
{
    // Provider errors may contain credentials, paths or sensitive values: never print them.
    Console.Error.WriteLine("Migration operation failed. Inspect protected local diagnostics; source ownership was not changed.");
    return 5;
}
