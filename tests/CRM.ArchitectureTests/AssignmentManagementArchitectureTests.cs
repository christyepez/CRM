using Xunit;

namespace CRM.ArchitectureTests;

public sealed class AssignmentManagementArchitectureTests
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
    public void S2402_IsReferenceOnlyFoundationSlice_WithoutApiRoutes()
    {
        var root = Root();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "AssignmentManagement", "AssignmentManagementService.cs"));
        Assert.Contains("IAssignmentManagementService, AssignmentManagementService", program);
        Assert.Contains("IAssignmentFoundationStore, InMemoryAssignmentFoundationStore", program);
        Assert.DoesNotContain("/api/crm/foundation/assignments", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("PortalIdentityRuntimeEnabled: false", service, StringComparison.Ordinal);
        Assert.DoesNotContain("Security API", service, StringComparison.OrdinalIgnoreCase);
    }
}
