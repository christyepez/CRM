using Xunit;

namespace CRM.ArchitectureTests;

public sealed class PipelineCatalogArchitectureTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void S2102_IsReadOnlySyntheticApplicationSlice()
    {
        var r=Root();
        var service=File.ReadAllText(Path.Combine(r,"src","CRM.Application","PipelineCatalog","PipelineCatalogService.cs"));
        var program=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));
        Assert.Contains("IPipelineCatalogSource",service,StringComparison.Ordinal);
        Assert.Contains("PortalCatalogRuntimeEnabled: false",service,StringComparison.Ordinal);
        Assert.Contains("IPipelineCatalogService, PipelineCatalogService",program,StringComparison.Ordinal);
        Assert.DoesNotContain("/api/crm/foundation/pipelines",program,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete",program,StringComparison.OrdinalIgnoreCase);
    }
}
