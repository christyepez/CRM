using Xunit;

namespace CRM.ArchitectureTests;

public sealed class OpportunityApplicationArchitectureTests
{
    [Fact] public void OpportunityApplication_DependsOnPolicyAndStoreAbstractionOnly(){var root=Root();var dir=Path.Combine(root,"src","CRM.Application","OpportunityManagement");var s=string.Join(Environment.NewLine,Directory.EnumerateFiles(dir,"*.cs").Select(File.ReadAllText));Assert.Contains("OpportunityPipelinePolicy",s,StringComparison.Ordinal);Assert.Contains("IOpportunityFoundationStore",s,StringComparison.Ordinal);Assert.DoesNotContain("CRM.Infrastructure",s,StringComparison.Ordinal);Assert.DoesNotContain("SqlConnection",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("DbContext",s,StringComparison.OrdinalIgnoreCase);}
    [Fact] public void OpportunityFoundationStore_IsInMemoryOnly(){var root=Root();var s=File.ReadAllText(Path.Combine(root,"src","CRM.Infrastructure","Persistence","Foundation","InMemoryOpportunityFoundationStore.cs"));Assert.Contains("IOpportunityFoundationStore",s,StringComparison.Ordinal);Assert.DoesNotContain("SqlConnection",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("DbContext",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("ConnectionString",s,StringComparison.OrdinalIgnoreCase);}
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
}
