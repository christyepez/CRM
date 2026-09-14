using Xunit;

namespace CRM.ArchitectureTests;
public sealed class PipelineCatalogFrontendGuardrailTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
    [Fact] public void Frontend_IsReadOnlyFoundationOnly(){var s=File.ReadAllText(Path.Combine(Root(),"frontend","crm-web","src","main.ts"));Assert.Contains("foundation/pipelines",s);Assert.Contains("Pipeline Catalog",s);Assert.DoesNotContain("createPipeline",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("updatePipeline",s,StringComparison.OrdinalIgnoreCase);Assert.DoesNotContain("deletePipeline",s,StringComparison.OrdinalIgnoreCase);}
}
