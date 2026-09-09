using Xunit;

namespace CRM.ArchitectureTests;

public sealed class CampaignManagementArchitectureTests
{
    [Fact]
    public void CampaignDomainRules_DoNotDependOnOuterLayers()
    {
        var root=FindRepositoryRoot();
        var dir=Path.Combine(root,"src","CRM.Domain","CampaignManagement");
        var source=string.Join(Environment.NewLine,Directory.EnumerateFiles(dir,"*.cs").Select(File.ReadAllText));
        Assert.Contains("CampaignManagementPolicy",source,StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Application",source,StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Infrastructure",source,StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Api",source,StringComparison.Ordinal);
        Assert.DoesNotContain("SqlConnection",source,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Authorization",source,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CampaignRuntimeRoutes_RemainAbsentInS1501()
    {
        var root=FindRepositoryRoot();
        var program=File.ReadAllText(Path.Combine(root,"src","CRM.Api","Program.cs"));
        foreach(var marker in new[]{"MapGet(\"/api/crm/campaigns","MapPost(\"/api/crm/campaigns","MapPut(\"/api/crm/campaigns","MapDelete(\"/api/crm/campaigns","MapGet(\"/api/crm/foundation/campaigns","MapPost(\"/api/crm/foundation/campaigns","MapPut(\"/api/crm/foundation/campaigns","MapDelete(\"/api/crm/foundation/campaigns"})
            Assert.DoesNotContain(marker,program,StringComparison.OrdinalIgnoreCase);
    }

    private static string FindRepositoryRoot(){var current=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(current)){if(File.Exists(Path.Combine(current,"CRM.sln")))return current;current=Directory.GetParent(current)?.FullName;}throw new DirectoryNotFoundException();}
}
