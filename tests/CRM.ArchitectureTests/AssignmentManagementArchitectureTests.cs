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
    public void S2403_ExposesOnlyFoundationAssignmentRoutes()
    {
        var root = Root();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "AssignmentManagement", "AssignmentManagementService.cs"));
        Assert.Contains("/api/crm/foundation/assignments", program, StringComparison.Ordinal);
        Assert.Contains("MapPut(\"/api/crm/foundation/assignments/{id}\"", program, StringComparison.Ordinal);
        Assert.DoesNotContain("MapDelete(\"/api/crm/foundation/assignments", program, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("\"/api/crm/assignments\"", program, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("PortalIdentityRuntimeEnabled: false", service, StringComparison.Ordinal);
        Assert.DoesNotContain("Security API", service, StringComparison.OrdinalIgnoreCase);
    }
}
