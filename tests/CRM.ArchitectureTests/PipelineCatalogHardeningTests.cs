using Xunit;
namespace CRM.ArchitectureTests;
public sealed class PipelineCatalogHardeningTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}
    [Fact] public void ApiAndFrontend_ExposeReadOnlyCatalogOnly(){var r=Root();var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));var f=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));foreach(var verb in new[]{"MapPost(\"/api/crm/foundation/pipelines","MapPut(\"/api/crm/foundation/pipelines","MapDelete(\"/api/crm/foundation/pipelines","MapPatch(\"/api/crm/foundation/pipelines"})Assert.DoesNotContain(verb,p,StringComparison.OrdinalIgnoreCase);foreach(var x in new[]{"createPipeline","updatePipeline","deletePipeline","reorderPipeline"})Assert.DoesNotContain(x,f,StringComparison.OrdinalIgnoreCase);Assert.Contains("portalCatalogRuntimeEnabled",f,StringComparison.OrdinalIgnoreCase);}
}
