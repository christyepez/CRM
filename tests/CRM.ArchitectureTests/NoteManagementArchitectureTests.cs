using Xunit;

namespace CRM.ArchitectureTests;

public sealed class NoteManagementArchitectureTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void Application_UsesPolicyAndFoundationStoreOnly()
    {
        var r=Root(); var s=File.ReadAllText(Path.Combine(r,"src","CRM.Application","NoteManagement","NoteManagementService.cs"));
        Assert.Contains("NoteManagementPolicy.Evaluate",s,StringComparison.Ordinal);
        Assert.Contains("INoteFoundationStore",s,StringComparison.Ordinal);
        foreach(var x in new[]{"DbContext","SqlConnection","HttpClient","Authorization"}) Assert.DoesNotContain(x,s,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void S2002_RegistersServiceAndStoreWithoutNoteRoutes()
    {
        var r=Root(); var p=File.ReadAllText(Path.Combine(r,"src","CRM.Api","Program.cs"));
        Assert.Contains("INoteManagementService, NoteManagementService",p,StringComparison.Ordinal);
        Assert.Contains("INoteFoundationStore, InMemoryNoteFoundationStore",p,StringComparison.Ordinal);
        Assert.DoesNotContain("MapGet(\"/api/crm/foundation/notes",p,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm",p,StringComparison.OrdinalIgnoreCase);
    }
}
