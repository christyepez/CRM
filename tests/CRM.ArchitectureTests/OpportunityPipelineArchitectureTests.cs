using Xunit;

namespace CRM.ArchitectureTests;

public sealed class OpportunityPipelineArchitectureTests
{
    [Fact]
    public void OpportunityPipelineDomainRules_DoNotDependOnOuterLayers()
    {
        var root = FindRepositoryRoot();
        var dir = Path.Combine(root,"src","CRM.Domain","OpportunityManagement");
        var source = string.Join(Environment.NewLine, Directory.EnumerateFiles(dir,"*.cs").Select(File.ReadAllText));
        Assert.Contains("OpportunityPipelinePolicy", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Application", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Infrastructure", source, StringComparison.Ordinal);
        Assert.DoesNotContain("CRM.Api", source, StringComparison.Ordinal);
        Assert.DoesNotContain("SqlConnection", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Authorization", source, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void OpportunityRuntimeRoutes_RemainAbsentInS1401()
    {
        var root=FindRepositoryRoot();
        var program=File.ReadAllText(Path.Combine(root,"src","CRM.Api","Program.cs"));
        Assert.DoesNotContain("/api/crm/opportunities",program,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("/api/crm/foundation/opportunities",program,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/opportunities",program,StringComparison.OrdinalIgnoreCase);
    }

    private static string FindRepositoryRoot(){ var current=AppContext.BaseDirectory; while(!string.IsNullOrWhiteSpace(current)){ if(File.Exists(Path.Combine(current,"CRM.sln"))) return current; current=Directory.GetParent(current)?.FullName; } throw new DirectoryNotFoundException(); }
}
