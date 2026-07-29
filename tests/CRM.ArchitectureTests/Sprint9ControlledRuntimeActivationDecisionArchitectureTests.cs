using Xunit;

namespace CRM.ArchitectureTests;

public sealed class Sprint9ControlledRuntimeActivationDecisionArchitectureTests
{
    [Fact]
    public void Sprint9P1_EndpointAndServiceMarkersExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmControlledRuntimeActivationDecisionStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-9/controlled-runtime-activation-decision", program);
        Assert.Contains("CrmControlledRuntimeActivationDecisionStatusService", program);
        Assert.Contains("ControlledRuntimeActivationDecision", service);
        Assert.Contains("ApprovedForNonProductionTrialsOnly", service);
        Assert.Contains("ProductionDecision = \"NoGo\"", service);
        Assert.Contains("RuntimeTrialsEnabledNow: false", service);
        Assert.Contains("ProductionRuntimeEnabledNow: false", service);
        Assert.Contains("ProductiveRoutesEnabledNow: false", service);
        Assert.Contains("ProductiveCrudEnabledNow: false", service);
        Assert.Contains("DeleteEnabledNow: false", service);
        Assert.Contains("Sprint9P2SecretProviderRuntimeEnablementTrial", service);
        Assert.Contains("Sprint 9 P1 is an approval decision only; no runtime trial is enabled now", service);
    }

    [Fact]
    public void Sprint9P1_DoesNotIntroduceRuntimeActivation()
    {
        var root = FindRepositoryRoot();
        var text = string.Join("\n", new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmControlledRuntimeActivationDecisionContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmControlledRuntimeActivationDecisionStatusService.cs"),
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
    public void Sprint9P1_DocumentationExists()
    {
        var root = FindRepositoryRoot();
        foreach (var path in new[]
        {
            "docs/releases/crm-sprint-9-p1-controlled-runtime-activation-decision.md",
            "docs/architecture/crm-controlled-runtime-activation-decision.md",
            "docs/security/crm-controlled-runtime-activation-approval-policy.md",
            "docs/operations/crm-controlled-runtime-activation-runbook.md",
            "docs/operations/crm-controlled-runtime-activation-rollback.md",
            "docs/testing/crm-controlled-runtime-activation-test-strategy.md",
            "docs/architecture/crm-sprint-9-gate-matrix.md",
            "docs/security/crm-sprint-9-security-gate-review.md",
            "docs/data/crm-sprint-9-runtime-data-gate-review.md",
            "docs/api/crm-sprint-9-runtime-api-gate-review.md",
            "docs/testing/crm-sprint-9-runtime-e2e-gate-review.md"
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
