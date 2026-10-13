using Xunit;

namespace CRM.ArchitectureTests;

public sealed class NoteManagementArchitectureTests
{
    private static string Root()
    {
        var current = AppContext.BaseDirectory;
        while (!string.IsNullOrWhiteSpace(current))
        {
            if (File.Exists(Path.Combine(current, "CRM.sln"))) return current;
            current = Directory.GetParent(current)?.FullName;
        }
        throw new DirectoryNotFoundException();
    }

    [Fact]
    public void Application_UsesPolicyAndFoundationStoreOnly()
    {
        var source = File.ReadAllText(Path.Combine(Root(), "src", "CRM.Application", "NoteManagement", "NoteManagementService.cs"));
        Assert.Contains("NoteManagementPolicy.Evaluate", source, StringComparison.Ordinal);
        Assert.Contains("INoteFoundationStore", source, StringComparison.Ordinal);
        foreach (var marker in new[] { "DbContext", "SqlConnection", "HttpClient", "Authorization" })
            Assert.DoesNotContain(marker, source, StringComparison.OrdinalIgnoreCase);
    }
    [Fact]
    public void FoundationApi_IsRegisteredWithoutProductiveOrDeleteRoutes()
    {
        var program = File.ReadAllText(Path.Combine(Root(), "src", "CRM.Api", "Program.cs"));
        Assert.Contains("INoteManagementService, NoteManagementService", program, StringComparison.Ordinal);
        Assert.Contains("INoteFoundationStore, InMemoryNoteFoundationStore", program, StringComparison.Ordinal);
        Assert.Contains("MapGet(\"/api/crm/foundation/notes", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapPost(\"/api/crm/foundation/notes", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("MapPut(\"/api/crm/foundation/notes/{id}\"", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("/archive", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapGet(\"/api/crm/notes", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapPost(\"/api/crm/notes", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapPut(\"/api/crm/notes", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/notes", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("MapDelete(\"/api/crm/notes", program, StringComparison.OrdinalIgnoreCase);
    }
}
