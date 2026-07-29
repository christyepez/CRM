using Xunit;

namespace CRM.ArchitectureTests;

public sealed class Sprint7GateDecisionArchitectureTests
{
    [Fact]
    public void Sprint7GateDecision_IsFoundationOnlyAndDoesNotActivateRuntime()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint7GateDecisionStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-7/gate-decision", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-7/gate-decision", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-7/gate-decision", program);
        Assert.DoesNotContain("MapDelete", program);
        Assert.Contains("Sprint7GateDecision", service);
        Assert.Contains("GoForSprint8ControlledRuntimeApprovalAndPilotPlanning", service);
        Assert.Contains("Sprint8P1SecretProviderApprovalDecision", service);
        Assert.Contains("Sprint 7 gate decision only; no real activation", service);
        Assert.Contains("GoOnlyAsExplicitNonProductionLocked423", service);
        Assert.DoesNotContain("SqlConnection", service);
        Assert.DoesNotContain("DbConnection", service);
        Assert.DoesNotContain("UseSqlServer", service);
        Assert.DoesNotContain("AddDbContext", service);
        Assert.DoesNotContain("HttpClient", service);
        Assert.DoesNotContain("Request.Headers", service);
        Assert.DoesNotContain("AuthorizeAttribute", service);
    }

    [Fact]
    public void Sprint7GateDecision_DocumentationAndRoadmapExist()
    {
        var root = FindRepositoryRoot();
        foreach (var path in new[]
        {
            "docs/releases/crm-sprint-7-closure.md",
            "docs/releases/crm-sprint-7-integrated-evidence.md",
            "docs/releases/crm-sprint-7-gate-decision.md",
            "docs/releases/crm-sprint-7-go-no-go.md",
            "docs/releases/crm-sprint-7-open-risks.md",
            "docs/releases/crm-sprint-7-decision-record.md",
            "docs/architecture/crm-sprint-7-gate-matrix.md",
            "docs/security/crm-sprint-7-security-gate-review.md",
            "docs/data/crm-sprint-7-persistence-gate-review.md",
            "docs/api/crm-sprint-7-api-gate-review.md",
            "docs/testing/crm-sprint-7-e2e-gate-review.md",
            "docs/roadmap/crm-sprint-8-options.md",
            "docs/roadmap/crm-sprint-8-recommended-path.md",
            "docs/roadmap/crm-sprint-8-gates.md"
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
