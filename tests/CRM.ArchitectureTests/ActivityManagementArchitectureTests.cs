using Xunit;

namespace CRM.ArchitectureTests;

public sealed class ActivityManagementArchitectureTests
{
    [Fact]
    public void ActivityManagementDomainRules_DoNotDependOnOuterLayers()
    {
        var source = ReadActivityManagementSources();

        Assert.Contains("ActivityManagementPolicy", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Application", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Api", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Infrastructure", source, StringComparison.Ordinal);
        Assert.DoesNotContain("SqlConnection", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Authorization", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductiveActivityRoute_RemainsAbsent()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.DoesNotContain("MapGet(\"/api/crm/activities", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapPost(\"/api/crm/activities", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapPut(\"/api/crm/activities", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/activities", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapGet(\"/api/crm/foundation/activities", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapPost(\"/api/crm/foundation/activities", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapPut(\"/api/crm/foundation/activities/{id}", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapPost(\"/api/crm/foundation/activities/{id}/complete", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapPost(\"/api/crm/foundation/activities/{id}/cancel", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/activities", program, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ActivityApplicationService_DependsOnAbstractionsAndDomainPolicyOnly()
    {
        var source = ReadActivityApplicationSources();

        Assert.Contains("IActivityFoundationStore", source, StringComparison.Ordinal);
        Assert.Contains("ActivityManagementPolicy", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Infrastructure", source, StringComparison.Ordinal);
        Assert.DoesNotContain("SqlConnection", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Authorization", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ActivityFoundationStore_IsInfrastructureOnlyAndAvoidsCommonDb()
    {
        var source = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Infrastructure", "Persistence", "Foundation", "InMemoryActivityFoundationStore.cs"));

        Assert.Contains("IActivityFoundationStore", source, StringComparison.Ordinal);
        Assert.DoesNotContain("DbContext", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SqlConnection", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", source, StringComparison.OrdinalIgnoreCase);
    }

    private static string ReadActivityManagementSources()
    {
        var directory = Path.Combine(FindRepositoryRoot(), "src", "CRM.Domain", "ActivityManagement");
        return string.Join(
            Environment.NewLine,
            Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories).Select(File.ReadAllText));
    }

    private static string ReadActivityApplicationSources()
    {
        var directory = Path.Combine(FindRepositoryRoot(), "src", "CRM.Application", "ActivityManagement");
        return string.Join(
            Environment.NewLine,
            Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories).Select(File.ReadAllText));
    }

    private static string FindRepositoryRoot()
    {
        var current = AppContext.BaseDirectory;
        while (!string.IsNullOrWhiteSpace(current))
        {
            if (File.Exists(Path.Combine(current, "CRM.sln")))
            {
                return current;
            }

            current = Directory.GetParent(current)?.FullName;
        }

        throw new DirectoryNotFoundException("Could not locate CRM repository root.");
    }
}
