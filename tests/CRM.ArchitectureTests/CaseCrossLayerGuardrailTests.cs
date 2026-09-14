using Xunit;

namespace CRM.ArchitectureTests;

public sealed class CaseCrossLayerGuardrailTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void Frontend_UsesFoundationCaseRoutesOnly_AndNoDelete()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type CaseStatus"); var b=s.IndexOf("type AccountStatus"); var c=s[a..b];
        Assert.Contains("/api/crm/foundation/cases",c,StringComparison.Ordinal);
        Assert.DoesNotContain("/api/crm/cases",c,StringComparison.Ordinal);
        Assert.DoesNotContain("deleteCase",c,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(".delete<",c,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Frontend_CaseFieldsLifecycle_AndSubmissionGuard_AreAligned()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type CaseStatus"); var b=s.IndexOf("type AccountStatus"); var c=s[a..b];
        foreach(var x in new[]{"maxlength=\"160\"","maxlength=\"1000\"","startCase","resolveCase","closeCase","this.isSubmitting()"}) Assert.Contains(x,c,StringComparison.Ordinal);
        foreach(var x in new[]{"'Open'","'InProgress'","'Resolved'","'Closed'","'Low'","'Medium'","'High'","'Critical'"}) Assert.Contains(x,c,StringComparison.Ordinal);
    }

    [Fact]
    public void Frontend_DoesNotAddPortalTokenCustomerLookupOrRuntimeAssignment()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type CaseStatus"); var b=s.IndexOf("type AccountStatus"); var c=s[a..b];
        foreach(var x in new[]{"Author"+"ization","local"+"Storage","session"+"Storage","customerLookup","assignee","slaTimer","notificationRuntime"}) Assert.DoesNotContain(x,c,StringComparison.OrdinalIgnoreCase);
    }
}
