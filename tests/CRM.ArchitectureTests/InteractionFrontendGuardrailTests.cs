using Xunit;

namespace CRM.ArchitectureTests;

public sealed class InteractionFrontendGuardrailTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void Frontend_UsesFoundationInteractionRoutesOnly_AndNoDelete()
    {
        var s=File.ReadAllText(Path.Combine(Root(),"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type InteractionStatus"); var b=s.IndexOf("type AccountStatus"); var c=s[a..b];
        Assert.Contains("/api/crm/foundation/interactions",c,StringComparison.Ordinal);
        Assert.DoesNotContain("/api/crm/interactions",c,StringComparison.Ordinal);
        Assert.DoesNotContain("deleteInteraction",c,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(".delete<",c,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Frontend_InteractionFieldsAndVoidAction_AreAligned()
    {
        var s=File.ReadAllText(Path.Combine(Root(),"frontend","crm-web","src","main.ts"));
        var a=s.IndexOf("type InteractionStatus"); var b=s.IndexOf("type AccountStatus"); var c=s[a..b];
        foreach(var x in new[]{"maxlength=\"160\"","maxlength=\"2000\"","voidInteraction","Occurred at UTC","relatedEntityId","this.isSubmitting()"}) Assert.Contains(x,c,StringComparison.Ordinal);
        foreach(var x in new[]{"'Recorded'","'Voided'","'Inbound'","'Outbound'"}) Assert.Contains(x,c,StringComparison.Ordinal);
    }
}
