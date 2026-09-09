using Xunit;

namespace CRM.ArchitectureTests;

public sealed class CampaignCrossLayerGuardrailTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void Frontend_UsesFoundationCampaignRoutesOnly_AndNoDelete()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type CampaignStatus"); var b=s.IndexOf("type ActivityType"); var c=s[a..b];
        Assert.Contains("/api/crm/foundation/campaigns",c,StringComparison.Ordinal);
        Assert.DoesNotContain("/api/crm/campaigns",c,StringComparison.Ordinal);
        Assert.DoesNotContain("deleteCampaign",c,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(".delete<",c,StringComparison.OrdinalIgnoreCase);
    }
    [Fact]
    public void Frontend_TerminalLifecycle_IsReadOnly_AndDuplicateProtected()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type CampaignStatus"); var b=s.IndexOf("type ActivityType"); var c=s[a..b];
        Assert.Contains("selectedCampaignReadonly()",c,StringComparison.Ordinal);
        Assert.Contains("campaignForm.disable",c,StringComparison.Ordinal);
        Assert.Contains("this.isSubmitting()",c,StringComparison.Ordinal);
        Assert.Contains("isSubmitting.set(true)",c,StringComparison.Ordinal);
    }

    [Fact]
    public void Api_AndProgram_KeepProductiveCampaignLocked()
    {
        var r=Root(); var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));
        Assert.Contains("/api/crm/foundation/campaigns",p,StringComparison.Ordinal);
        foreach(var x in new[]{"MapGet(\"/api/crm/campaigns","MapPost(\"/api/crm/campaigns","MapPut(\"/api/crm/campaigns","MapDelete(\"/api/crm/campaigns","MapDelete(\"/api/crm/foundation/campaigns"}) Assert.DoesNotContain(x,p,StringComparison.OrdinalIgnoreCase);
    }
    [Fact]
    public void CampaignApplication_UsesPolicyAndFoundationStore_WithoutOuterRuntimeDependencies()
    {
        var r=Root();
        var service=File.ReadAllText(Path.Combine(r,"src","CRM.Application","CampaignManagement","CampaignManagementService.cs"));
        Assert.Contains("CampaignManagementPolicy.Evaluate",service,StringComparison.Ordinal);
        Assert.Contains("ICampaignFoundationStore",service,StringComparison.Ordinal);
        Assert.DoesNotContain("DbContext",service,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("SqlConnection",service,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("Authorization",service,StringComparison.OrdinalIgnoreCase);
    }
}
