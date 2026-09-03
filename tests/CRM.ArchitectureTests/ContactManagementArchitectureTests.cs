using Xunit;

namespace CRM.ArchitectureTests;

public sealed class ContactManagementArchitectureTests
{
    [Fact]
    public void ContactManagementDomainRules_DoNotDependOnOuterLayers()
    {
        var source = ReadContactManagementSources();

        Assert.DoesNotContain("CRM.Application", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Api", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Infrastructure", source, StringComparison.Ordinal);
        Assert.DoesNotContain("SqlConnection", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ProductiveContactRoute_RemainsLocked()
    {
        var program = File.ReadAllText(Path.Combine(FindRepositoryRoot(), "src", "CRM.Api", "Program.cs"));

        Assert.DoesNotContain("MapPost(\"/api/crm/contacts", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapPut(\"/api/crm/contacts", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/contacts", program, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void ContactManagementApplicationService_UsesStoreAbstractionAndAvoidsRuntimeCoupling()
    {
        var source = ReadApplicationContactManagementSources();

        Assert.Contains("IContactFoundationStore", source, StringComparison.Ordinal);
        Assert.Contains("ContactManagementPolicy", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Infrastructure", source, StringComparison.Ordinal);
        Assert.DoesNotContain("SqlConnection", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("UseSqlServer", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Authorization", source, StringComparison.OrdinalIgnoreCase);
    }

    private static string ReadContactManagementSources()
    {
        var directory = Path.Combine(FindRepositoryRoot(), "src", "CRM.Domain", "ContactManagement");
        return string.Join(
            Environment.NewLine,
            Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories).Select(File.ReadAllText));
    }

    private static string ReadApplicationContactManagementSources()
    {
        var directory = Path.Combine(FindRepositoryRoot(), "src", "CRM.Application", "ContactManagement");
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
