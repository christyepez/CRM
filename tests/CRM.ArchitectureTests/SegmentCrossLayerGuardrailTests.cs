using Xunit;

namespace CRM.ArchitectureTests;

public sealed class SegmentCrossLayerGuardrailTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void Frontend_UsesFoundationSegmentRoutesOnly_AndNoDelete()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type SegmentStatus"); var b=s.IndexOf("type AccountStatus"); var c=s[a..b];
        Assert.Contains("/api/crm/foundation/segments",c,StringComparison.Ordinal);
        Assert.DoesNotContain("/api/crm/segments",c,StringComparison.Ordinal);
        Assert.DoesNotContain("deleteSegment",c,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(".delete<",c,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Frontend_SegmentFieldsLifecycle_AndSubmissionGuard_AreAligned()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type SegmentStatus"); var b=s.IndexOf("type AccountStatus"); var c=s[a..b];
        foreach(var x in new[]{"maxlength=\"160\"","maxlength=\"1000\"","activateSegment","deactivateSegment","this.isSubmitting()"}) Assert.Contains(x,c,StringComparison.Ordinal);
        foreach(var x in new[]{"'Draft'","'Active'","'Inactive'"}) Assert.Contains(x,c,StringComparison.Ordinal);
    }

    [Fact]
    public void Application_UsesPolicyAndFoundationStore_WithoutOuterRuntimeDependencies()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"src","CRM.Application","SegmentManagement","SegmentManagementService.cs"));
        Assert.Contains("SegmentManagementPolicy.Evaluate",s,StringComparison.Ordinal);
        Assert.Contains("ISegmentFoundationStore",s,StringComparison.Ordinal);
        foreach(var x in new[]{"DbContext","SqlConnection","Authorization","HttpClient"}) Assert.DoesNotContain(x,s,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Api_MapsExpectedErrors_AndKeepsProductiveSegmentLocked()
    {
        var r=Root();
        var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));
        var c=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Foundation","SegmentManagementApiContracts.cs"));
        foreach(var x in new[]{"/api/crm/foundation/segments","/activate","/deactivate"}) Assert.Contains(x,p,StringComparison.Ordinal);
        foreach(var x in new[]{"Status400BadRequest","Status404NotFound","Status409Conflict","InvalidStatusTransition"}) Assert.Contains(x,c,StringComparison.Ordinal);
        foreach(var x in new[]{"MapGet(\"/api/crm/segments","MapPost(\"/api/crm/segments","MapPut(\"/api/crm/segments","MapDelete(\"/api/crm/segments","MapDelete(\"/api/crm/foundation/segments"}) Assert.DoesNotContain(x,p,StringComparison.OrdinalIgnoreCase);
    }
}
