using Xunit;

namespace CRM.ArchitectureTests;

public sealed class NoteManagementArchitectureTests
{
    private static string Root(){var c=AppContext.BaseDirectory;while(!string.IsNullOrWhiteSpace(c)){if(File.Exists(Path.Combine(c,"CRM.sln")))return c;c=Directory.GetParent(c)?.FullName;}throw new DirectoryNotFoundException();}

    [Fact]
    public void Application_UsesPolicyAndFoundationStoreOnly()
    {
        var s=File.ReadAllText(Path.Combine(Root(),"src","CRM.Application","NoteManagement","NoteManagementService.cs"));
        Assert.Contains("NoteManagementPolicy.Evaluate",s,StringComparison.Ordinal);
        Assert.Contains("INoteFoundationStore",s,StringComparison.Ordinal);
        foreach(var x in new[]{"DbContext","SqlConnection","HttpClient","Authorization"}) Assert.DoesNotContain(x,s,StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void FoundationApi_IsRegisteredWithoutProductiveOrDeleteRoutes()
    {
        var p=File.ReadAllText(Path.Combine(Root(),"src","CRM.Api","Program.cs"));
        Assert.Contains("INoteManagementService, NoteManagementService",p,StringComparison.Ordinal);
        Assert.Contains("INoteFoundationStore, InMemoryNoteFoundationStore",p,StringComparison.Ordinal);
        Assert.Contains("MapGet(\"/api/crm/foundation/notes",p,StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapPost(\"/api/crm/foundation/notes",p,StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapPut(\"/api/crm/foundation/notes/{id}\"",p,StringComparison.OrdinalIgnoreCase);
        Assert.Contains("/archive",p,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapGet(\"/api/crm/notes",p,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/notes",p,StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/notes",p,StringComparison.OrdinalIgnoreCase);
    }
}