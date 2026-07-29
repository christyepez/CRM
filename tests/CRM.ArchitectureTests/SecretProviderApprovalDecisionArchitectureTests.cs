using Xunit;

namespace CRM.ArchitectureTests;

public sealed class SecretProviderApprovalDecisionArchitectureTests
{
    [Fact]
    public void Sprint8P1_IsDecisionOnlyAndDoesNotReadSecrets()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSecretProviderApprovalDecisionStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-8/secret-provider-approval-decision", program);
        Assert.DoesNotContain("MapPost(\"/api/crm/foundation/sprint-8/secret-provider-approval-decision", program);
        Assert.DoesNotContain("MapPut(\"/api/crm/foundation/sprint-8/secret-provider-approval-decision", program);
        Assert.Contains("SecretProviderApprovalDecision", service);
        Assert.Contains("ApprovedForControlledNonProductionReadPlanning", service);
        Assert.Contains("SecretProviderRealReadApprovedForNextSprint: true", service);
        Assert.Contains("SecretProviderRealReadEnabledNow: false", service);
        Assert.Contains("RealSecretReadAttempted: false", service);
        Assert.Contains("SecretValueReturnedToApi: false", service);
        Assert.Contains("Sprint8P2SecretProviderControlledRealNonProductionRead", service);
        Assert.DoesNotContain("SecretClient", service);
        Assert.DoesNotContain("DefaultAzureCredential", service);
        Assert.DoesNotContain("ManagedIdentityCredential", service);
        Assert.DoesNotContain("EnvironmentCredential", service);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", service);
        Assert.DoesNotContain("File.ReadAllText", service);
        Assert.DoesNotContain("SqlConnection", service);
        Assert.DoesNotContain("DbConnection", service);
        Assert.DoesNotContain("UseSqlServer", service);
        Assert.DoesNotContain("HttpClient", service);
        Assert.DoesNotContain("Request.Headers", service);
    }

    [Fact]
    public void Sprint8P1_DocumentationExists()
    {
        var root = FindRepositoryRoot();
        foreach (var path in new[]
        {
            "docs/security/crm-sprint-8-p1-secret-provider-approval-decision.md",
            "docs/security/crm-secret-provider-approval-decision-policy.md",
            "docs/security/crm-secret-provider-controlled-read-approval-criteria.md",
            "docs/security/crm-secret-provider-approved-logical-secret-names.md",
            "docs/security/crm-secret-provider-redaction-approval.md",
            "docs/operations/crm-secret-provider-controlled-read-runbook.md",
            "docs/operations/crm-secret-provider-controlled-read-rollback.md",
            "docs/architecture/crm-secret-provider-controlled-read-architecture-decision.md"
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
