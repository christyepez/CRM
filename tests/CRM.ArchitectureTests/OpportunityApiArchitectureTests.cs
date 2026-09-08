using Xunit;

namespace CRM.ArchitectureTests;

public sealed class OpportunityApiArchitectureTests
{
    [Fact]
    public void OpportunityFoundationApi_UsesApplicationInterfaceAndNoProductiveRoute()
    {
        var root = FindRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));

        Assert.Contains("IOpportunityManagementService", program, StringComparison.Ordinal);
        Assert.Contains("/api/crm/foundation/opportunities", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapGet(\"/api/crm/opportunities", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapPost(\"/api/crm/opportunities", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapPut(\"/api/crm/opportunities", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/opportunities", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/opportunities", program, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void OpportunityApiContracts_AreExplicitAndDoNotBindDomainEntity()
    {
        var root = FindRoot();
        var source = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Foundation", "OpportunityManagementApiContracts.cs"));

        Assert.Contains("FoundationOpportunityCreateRequest", source, StringComparison.Ordinal);
        Assert.Contains("FoundationOpportunityUpdateRequest", source, StringComparison.Ordinal);
        Assert.Contains("FoundationOpportunityProgressRequest", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Domain.Entities.Opportunity", source, StringComparison.Ordinal);
        Assert.DoesNotContain("DbContext", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Authorization", source, StringComparison.OrdinalIgnoreCase);
    }

    private static string FindRoot()
    {
        var current = AppContext.BaseDirectory;
        while (!string.IsNullOrWhiteSpace(current))
        {
            if (File.Exists(Path.Combine(current, "CRM.sln"))) return current;
            current = Directory.GetParent(current)?.FullName;
        }
        throw new DirectoryNotFoundException();
    }
}
