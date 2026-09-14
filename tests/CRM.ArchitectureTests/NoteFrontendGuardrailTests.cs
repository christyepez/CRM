using Xunit;

namespace CRM.ArchitectureTests;

public sealed class NoteFrontendGuardrailTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void NoteFrontend_UsesFoundationApiOnly()
    {
        var source=File.ReadAllText(Path.Combine(Root(),"frontend","crm-web","src","main.ts"));
        Assert.Contains("/foundation/notes",source,StringComparison.Ordinal);
        Assert.Contains("/api/crm/foundation/notes",source,StringComparison.Ordinal);
        Assert.Contains("Foundation only",source,StringComparison.Ordinal);
        Assert.Contains("Archive note",source,StringComparison.Ordinal);
        Assert.DoesNotContain("/api/crm/notes'",source,StringComparison.Ordinal);
        Assert.DoesNotContain("deleteNote",source,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("scheduleActivity",source,StringComparison.OrdinalIgnoreCase);
    }
}
