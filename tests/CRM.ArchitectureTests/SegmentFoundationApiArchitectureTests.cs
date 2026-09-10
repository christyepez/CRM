using Xunit;

namespace CRM.ArchitectureTests;

public sealed class SegmentFoundationApiArchitectureTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void SegmentApi_IsFoundationOnly_AndNoDelete()
    {
        var p=File.ReadAllText(Path.Combine(Root(),"src","CRM.Api","Program.cs"));
        foreach(var x in new[]{"MapGet(\"/api/crm/foundation/segments","MapPost(\"/api/crm/foundation/segments","MapPut(\"/api/crm/foundation/segments","/activate","/deactivate"}) Assert.Contains(x,p,StringComparison.Ordinal);
        foreach(var x in new[]{"MapGet(\"/api/crm/segments","MapPost(\"/api/crm/segments","MapPut(\"/api/crm/segments","MapDelete(\"/api/crm/segments","MapDelete(\"/api/crm/foundation/segments"}) Assert.DoesNotContain(x,p,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void SegmentApi_UsesTypedApplicationService()
    {
        var p=File.ReadAllText(Path.Combine(Root(),"src","CRM.Api","Program.cs"));
        Assert.Contains("ISegmentManagementService",p,StringComparison.Ordinal);
        Assert.Contains("ISegmentFoundationStore",p,StringComparison.Ordinal);
        Assert.Contains("InMemorySegmentFoundationStore",p,StringComparison.Ordinal);
    }
}
