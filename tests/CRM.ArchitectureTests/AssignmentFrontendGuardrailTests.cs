using Xunit;

namespace CRM.ArchitectureTests;

public sealed class AssignmentFrontendGuardrailTests
{
    [Fact]
    public void S2404_Frontend_RemainsOpaqueReferenceOnly()
    {
        var root = AppContext.BaseDirectory;
        while (!File.Exists(Path.Combine(root, "CRM.sln"))) root = Directory.GetParent(root)!.FullName;
        var main = File.ReadAllText(Path.Combine(root, "frontend", "crm-web", "src", "main.ts"));
        Assert.Contains("path: 'foundation/assignments'", main, StringComparison.Ordinal);
        Assert.Contains("apiBaseUrl='/api/crm/foundation/assignments'", main, StringComparison.Ordinal);
        Assert.Contains("Assignee reference id", main, StringComparison.Ordinal);
        Assert.Contains("AssigneeReferenceId is not resolved against Portal Security", main, StringComparison.Ordinal);
        Assert.DoesNotContain("/api/crm/assignments'", main, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("type=\"file\"", main, StringComparison.OrdinalIgnoreCase);
        var start = main.IndexOf("class AssignmentManagementApiService", StringComparison.Ordinal);
        var end = main.IndexOf("const routes: Routes = [", start, StringComparison.Ordinal);
        var slice = main[start..end];
        Assert.DoesNotContain("/api/security", slice, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("getUsers", slice, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("getRoles", slice, StringComparison.OrdinalIgnoreCase);
    }
}
