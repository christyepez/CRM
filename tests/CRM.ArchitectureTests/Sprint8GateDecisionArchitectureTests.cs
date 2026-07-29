using Xunit;

namespace CRM.ArchitectureTests;

public sealed class Sprint8GateDecisionArchitectureTests
{
    [Fact]
    public void Sprint8P6_EndpointAndServiceMarkersExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint8GateDecisionStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-8/gate-decision", program);
        Assert.Contains("CrmSprint8GateDecisionStatusService", program);
        Assert.Contains("Sprint8GateDecision", service);
        Assert.Contains("GoForSprint9ControlledRuntimeActivationPlanning", service);
        Assert.Contains("RealProductionActivationDecision: \"NoGo\"", service);
        Assert.Contains("GoOnlyAsExplicitNonProductionFlag", service);
        Assert.Contains("GoOnlyAsExplicitNonProductionLocked423", service);
        Assert.Contains("ProductizationStatus: \"NotReady\"", service);
        Assert.Contains("Sprint9P1ControlledRuntimeActivationDecision", service);
        Assert.Contains("Sprint 8 gate decision only; no production activation", service);
    }

    [Fact]
    public void Sprint8P6_DoesNotIntroduceRuntimeActivation()
    {
        var root = FindRepositoryRoot();
        var text = string.Join("\n", new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint8GateDecisionContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint8GateDecisionStatusService.cs"),
            Path.Combine(root, "src", "CRM.Api", "Program.cs")
        }.Select(File.ReadAllText));

        Assert.DoesNotContain("MapDelete", text);
        Assert.DoesNotContain("UseSqlServer(", text);
        Assert.DoesNotContain("AddDbContext(", text);
        Assert.DoesNotContain("SqlConnection(", text);
        Assert.DoesNotContain("DbConnection(", text);
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
    }

    [Fact]
    public void Sprint8P6_DocumentationExists()
    {
        var root = FindRepositoryRoot();
        foreach (var path in new[]
        {
            "docs/releases/crm-sprint-8-closure.md",
            "docs/releases/crm-sprint-8-integrated-evidence.md",
            "docs/releases/crm-sprint-8-gate-decision.md",
            "docs/releases/crm-sprint-8-go-no-go.md",
            "docs/releases/crm-sprint-8-open-risks.md",
            "docs/releases/crm-sprint-8-decision-record.md",
            "docs/architecture/crm-sprint-8-gate-matrix.md",
            "docs/security/crm-sprint-8-security-gate-review.md",
            "docs/data/crm-sprint-8-persistence-gate-review.md",
            "docs/api/crm-sprint-8-api-gate-review.md",
            "docs/testing/crm-sprint-8-e2e-gate-review.md",
            "docs/roadmap/crm-sprint-9-options.md",
            "docs/roadmap/crm-sprint-9-recommended-path.md",
            "docs/roadmap/crm-sprint-9-gates.md"
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
