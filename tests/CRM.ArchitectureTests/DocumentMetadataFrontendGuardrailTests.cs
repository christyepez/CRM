using Xunit;

namespace CRM.ArchitectureTests;

public sealed class DocumentMetadataFrontendGuardrailTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void S2204_UsesFoundationMetadataOnlyFrontend()
    {
        var main=File.ReadAllText(Path.Combine(Root(),"frontend","crm-web","src","main.ts"));
        Assert.Contains("/api/crm/foundation/documents",main,StringComparison.Ordinal);
        Assert.Contains("foundation/documents",main,StringComparison.Ordinal);
        Assert.Contains("DocumentMetadataPageComponent",main,StringComparison.Ordinal);
        Assert.Contains("No file picker, upload, download or binary storage",main,StringComparison.Ordinal);
        Assert.DoesNotContain("type=\"file\"",main,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("/api/crm/documents",main,StringComparison.Ordinal);
        Assert.DoesNotContain("FormData",main,StringComparison.Ordinal);
    }
}
