using Xunit;

namespace CRM.ArchitectureTests;

public sealed class LeadQualificationArchitectureTests
{
    [Fact]
    public void LeadQualification_DoesNotUnlock_ProductiveRoutes_Or_RuntimeDependencies()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var source = string.Join(Environment.NewLine, Directory.GetFiles(Path.Combine(root, "src"), "*.cs", SearchOption.AllDirectories)
            .Where(path => path.Contains($"{Path.DirectorySeparatorChar}LeadQualification{Path.DirectorySeparatorChar}") || Path.GetFileName(path) == "LeadQualificationContracts.cs")
            .Select(File.ReadAllText));

        Assert.Contains("LeadQualificationPolicy", source);
        Assert.Contains("ILeadQualificationService", source);
        Assert.DoesNotContain("\"/api/crm/leads\"", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("Authorization", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("ConnectionString", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("DbContext", source, StringComparison.OrdinalIgnoreCase);
    }

    private static string FindRepositoryRoot()
    {
        var current = AppContext.BaseDirectory;
        while (current is not null)
        {
            if (File.Exists(Path.Combine(current, "CRM.sln")))
            {
                return current;
            }

            current = Directory.GetParent(current)?.FullName;
        }

        throw new DirectoryNotFoundException("Repository root containing CRM.sln was not found.");
    }
}

