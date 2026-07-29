using Xunit;

namespace CRM.ArchitectureTests;

public sealed class LockedRouteAuthorizationPolicyArchitectureTests
{
    [Fact]
    public void Sprint8P5_EndpointServiceAndEvaluatorExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmLockedRouteAuthorizationPolicyIntegrationStatusService.cs"));
        var evaluator = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmLockedRouteAuthorizationPolicyEvaluator.cs"));

        Assert.Contains("/api/crm/foundation/sprint-8/locked-route-authorization-policy-integration", program);
        Assert.Contains("CrmLockedRouteAuthorizationPolicyIntegrationStatusService", program);
        Assert.Contains("LockedRouteAuthorizationPolicyIntegrationEnabled: false", service);
        Assert.Contains("AuthorizationPolicyDecision: \"NotEvaluatedBecauseDisabled\"", service);
        Assert.Contains("Sprint8P6Sprint8GateDecision", service);
        Assert.Contains("BlockedBecauseRouteLocked", evaluator);
    }

    [Fact]
    public void Sprint8P5_DoesNotIntroduceForbiddenRuntimeDependencies()
    {
        var root = FindRepositoryRoot();
        var p5Files = new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmLockedRouteAuthorizationPolicyIntegrationContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmLockedRouteAuthorizationPolicyIntegrationStatusService.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmLockedRouteAuthorizationPolicyEvaluationRequest.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmLockedRouteAuthorizationPolicyEvaluationResult.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmLockedRouteAuthorizationPolicyEvaluator.cs"),
            Path.Combine(root, "src", "CRM.Api", "ProductiveRoutes", "LockedProductiveRouteRuntimeRegistration.cs")
        };

        var text = string.Join("\n", p5Files.Select(File.ReadAllText));

        Assert.DoesNotContain("HttpClient(", text);
        Assert.DoesNotContain("new HttpClient", text);
        Assert.DoesNotContain("Request.Headers", text);
        Assert.DoesNotContain("Headers[", text);
        Assert.DoesNotContain("AddAuthentication", text);
        Assert.DoesNotContain("UseAuthentication", text);
        Assert.DoesNotContain("UseAuthorization", text);
        Assert.DoesNotContain("AuthorizeAttribute", text);
        Assert.DoesNotContain("JwtBearer", text);
        Assert.DoesNotContain("CookieAuthentication", text);
        Assert.DoesNotContain("UseSqlServer(", text);
        Assert.DoesNotContain("AddDbContext(", text);
        Assert.DoesNotContain("MigrationBuilder", text);
        Assert.DoesNotContain("MapDelete", text);
    }

    [Fact]
    public void Sprint8P5_DocumentationExists()
    {
        var root = FindRepositoryRoot();
        foreach (var path in new[]
        {
            "docs/api/crm-sprint-8-p5-locked-route-authorization-policy-integration.md",
            "docs/api/crm-locked-route-authorization-policy-contract.md",
            "docs/api/crm-locked-route-authorization-policy-boundary.md",
            "docs/security/crm-locked-route-authorization-policy-security-review.md",
            "docs/security/crm-locked-route-authorization-policy-token-boundary.md",
            "docs/operations/crm-locked-route-authorization-policy-runbook.md",
            "docs/operations/crm-locked-route-authorization-policy-rollback.md",
            "docs/architecture/crm-locked-route-authorization-policy-architecture.md"
        })
        {
            Assert.True(File.Exists(Path.Combine(root, path)), path);
        }
    }

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null && !File.Exists(Path.Combine(directory.FullName, "CRM.sln")))
        {
            directory = directory.Parent;
        }

        return directory?.FullName ?? throw new InvalidOperationException("Repository root was not found.");
    }
}
