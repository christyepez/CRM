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

    [Fact]
    public void LeadQualificationApi_IsFoundationOnly_AndUsesApplicationService()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var apiContract = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Foundation", "LeadQualificationApiContracts.cs"));
        var endpointStart = program.IndexOf("app.MapPost(\"/api/crm/foundation/leads/{leadId}/qualification\"", StringComparison.Ordinal);
        var endpointEnd = program.IndexOf("app.MapGet(\"/api/crm/foundation/accounts\"", StringComparison.Ordinal);
        var endpointBlock = program[endpointStart..endpointEnd];

        Assert.Contains("/api/crm/foundation/leads/{leadId}/qualification", program);
        Assert.Contains("ILeadQualificationService", program);
        Assert.DoesNotContain("/api/crm/leads/{leadId}/qualification", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.DoesNotContain("LeadQualificationPolicy", endpointBlock);
        Assert.DoesNotContain("ILeadFoundationStore", endpointBlock);
        Assert.Contains("ToApplicationRequest", apiContract);
        Assert.Contains("ToStatusCode", apiContract);
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

