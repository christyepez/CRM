using Xunit;

namespace CRM.ArchitectureTests;

public sealed class AccountCrossLayerGuardrailTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void Frontend_UsesFoundationAccountRoutesOnly_AndNoDelete()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type AccountStatus"); var b=s.IndexOf("type CampaignStatus"); var c=s[a..b];
        Assert.Contains("/api/crm/foundation/accounts",c,StringComparison.Ordinal);
        Assert.DoesNotContain("/api/crm/accounts",c,StringComparison.Ordinal);
        Assert.DoesNotContain("deleteAccount",c,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(".delete<",c,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Frontend_AccountFieldsLifecycle_AndDuplicateProtection_AreAligned()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type AccountStatus"); var b=s.IndexOf("type CampaignStatus"); var c=s[a..b];
        foreach(var x in new[]{"maxlength=\"160\"","maxlength=\"64\"","maxlength=\"120\"","maxlength=\"80\"","Activate account","Deactivate account","this.isSubmitting()"}) Assert.Contains(x,c,StringComparison.Ordinal);
        foreach(var x in new[]{"'Draft'","'Active'","'Inactive'"}) Assert.Contains(x,c,StringComparison.Ordinal);
    }

    [Fact]
    public void Application_UsesPolicyAndFoundationStore_WithoutOuterRuntimeDependencies()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"src","CRM.Application","AccountManagement","AccountManagementService.cs"));
        Assert.Contains("AccountManagementPolicy.Evaluate",s,StringComparison.Ordinal);
        Assert.Contains("IAccountFoundationStore",s,StringComparison.Ordinal);
        foreach(var x in new[]{"DbContext","SqlConnection","Authorization","HttpClient"}) Assert.DoesNotContain(x,s,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Api_MapsExpectedErrors_AndKeepsProductiveAccountLocked()
    {
        var r=Root();
        var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));
        var c=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Foundation","AccountManagementApiContracts.cs"));
        foreach(var x in new[]{"/api/crm/foundation/accounts","/activate","/deactivate"}) Assert.Contains(x,p,StringComparison.Ordinal);
        foreach(var x in new[]{"Status400BadRequest","Status404NotFound","Status409Conflict","InvalidStatusTransition"}) Assert.Contains(x,c,StringComparison.Ordinal);
        foreach(var x in new[]{"MapGet(\"/api/crm/accounts","MapPost(\"/api/crm/accounts","MapPut(\"/api/crm/accounts","MapDelete(\"/api/crm/accounts","MapDelete(\"/api/crm/foundation/accounts"}) Assert.DoesNotContain(x,p,StringComparison.OrdinalIgnoreCase);
    }
}
