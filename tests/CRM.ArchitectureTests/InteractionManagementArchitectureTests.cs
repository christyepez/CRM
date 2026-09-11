using Xunit;

namespace CRM.ArchitectureTests;

public sealed class InteractionManagementArchitectureTests
{
    private static string Root()
    {
        var current = AppContext.BaseDirectory;
        while (!string.IsNullOrWhiteSpace(current))
        {
            if (File.Exists(Path.Combine(current, "CRM.sln")))
                return current;
            current = Directory.GetParent(current)?.FullName;
        }

        throw new DirectoryNotFoundException();
    }

    [Fact]
    public void InteractionDomain_RemainsDependencyFree()
    {
        var root = Root();
        var files = Directory.GetFiles(Path.Combine(root, "src", "CRM.Domain", "InteractionManagement"), "*.cs");
        var text = string.Join("\n", files.Select(File.ReadAllText));
        foreach (var marker in new[] { "CRM.Application", "CRM.Infrastructure", "CRM.Api", "Microsoft.EntityFrameworkCore", "HttpClient", "DbContext" })
            Assert.DoesNotContain(marker, text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void InteractionApplicationService_UsesPolicyAndFoundationStoreOnly()
    {
        var root = Root();
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "InteractionManagement", "InteractionManagementService.cs"));
        foreach (var marker in new[] { "InteractionManagementPolicy.Evaluate", "IInteractionFoundationStore", "NonProductionSeam", "CreateAsync", "UpdateAsync", "VoidAsync" })
            Assert.Contains(marker, service, StringComparison.Ordinal);
        foreach (var forbidden in new[] { "CRM.Infrastructure", "CRM.Api", "DbContext", "HttpClient", "SqlConnection" })
            Assert.DoesNotContain(forbidden, service, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void InteractionStore_IsInMemorySyntheticFoundationOnly()
    {
        var root = Root();
        var source = File.ReadAllText(Path.Combine(root, "src", "CRM.Infrastructure", "Persistence", "Foundation", "InMemoryInteractionFoundationStore.cs"));
        foreach (var marker in new[] { "IInteractionFoundationStore", "66666666-6666-6666-6666-666666666666", "Synthetic" })
            Assert.Contains(marker, source, StringComparison.Ordinal);
        foreach (var forbidden in new[] { "DbContext", "SqlConnection", "ConnectionString", "HttpClient", "crm-prod-sim", "8094", "Production" })
            Assert.DoesNotContain(forbidden, source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void InteractionServiceAndStore_AreRegisteredWithoutRoutes()
    {
        var root = Root();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        Assert.Contains("IInteractionManagementService, InteractionManagementService", program, StringComparison.Ordinal);
        Assert.Contains("IInteractionFoundationStore, InMemoryInteractionFoundationStore", program, StringComparison.Ordinal);
        foreach (var marker in new[]
        {
            "MapGet(\"/api/crm/foundation/interactions",
            "MapPost(\"/api/crm/foundation/interactions",
            "MapPut(\"/api/crm/foundation/interactions",
            "MapDelete(\"/api/crm/foundation/interactions",
            "MapGet(\"/api/crm/interactions",
            "MapPost(\"/api/crm/interactions",
            "MapPut(\"/api/crm/interactions",
            "MapDelete(\"/api/crm/interactions"
        })
        {
            Assert.DoesNotContain(marker, program, StringComparison.OrdinalIgnoreCase);
        }
    }
}
