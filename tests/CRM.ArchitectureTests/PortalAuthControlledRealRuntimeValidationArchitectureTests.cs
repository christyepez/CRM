using Xunit;

namespace CRM.ArchitectureTests;

public sealed class PortalAuthControlledRealRuntimeValidationArchitectureTests
{
    [Fact]
    public void Sprint8P4_EndpointAndServiceAreFailClosedByDefault()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmPortalAuthControlledRealRuntimeValidationStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation", program);
        Assert.Contains("DisabledPortalAuthRuntimeValidationProbe", program);
        Assert.Contains("PortalAuthControlledRealRuntimeValidation", service);
        Assert.Contains("PortalAuthControlledRealRuntimeValidationEnabled: false", service);
        Assert.Contains("PortalAuthRuntimeValidationAttempted: false", service);
        Assert.Contains("PortalAuthRuntimeConnected: false", service);
        Assert.Contains("TokenReadAttempted: false", service);
        Assert.Contains("HeaderReadAttempted: false", service);
        Assert.Contains("Sprint8P5LockedRouteAuthorizationPolicyIntegration", service);
    }

    [Fact]
    public void Sprint8P4_DoesNotIntroduceForbiddenRuntimeDependencies()
    {
        var root = FindRepositoryRoot();
        var p4Files = new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmPortalAuthControlledRealRuntimeValidationContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmPortalAuthControlledRealRuntimeValidationStatusService.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "RuntimeProbe", "IPortalAuthRuntimeValidationProbe.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "RuntimeProbe", "PortalAuthRuntimeValidationProbeOptions.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "RuntimeProbe", "DisabledPortalAuthRuntimeValidationProbe.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "RuntimeProbe", "ControlledNonProductionPortalAuthRuntimeValidationProbe.cs")
        };

        var text = string.Join("\n", p4Files.Select(File.ReadAllText));

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
    }

    [Fact]
    public void Sprint8P4_DocumentationExists()
    {
        var root = FindRepositoryRoot();
        foreach (var path in new[]
        {
            "docs/integration/crm-sprint-8-p4-portal-auth-controlled-real-runtime-validation.md",
            "docs/integration/crm-portal-auth-controlled-real-runtime-validation-policy.md",
            "docs/integration/crm-portal-auth-controlled-real-runtime-validation-contract.md",
            "docs/security/crm-portal-auth-controlled-runtime-token-boundary.md",
            "docs/security/crm-portal-auth-controlled-runtime-redaction.md",
            "docs/operations/crm-portal-auth-controlled-runtime-validation-runbook.md",
            "docs/operations/crm-portal-auth-controlled-runtime-validation-rollback.md",
            "docs/architecture/crm-portal-auth-controlled-runtime-validation-architecture.md"
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
