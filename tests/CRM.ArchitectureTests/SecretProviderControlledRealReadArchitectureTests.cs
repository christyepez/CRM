using Xunit;

namespace CRM.ArchitectureTests;

public sealed class SecretProviderControlledRealReadArchitectureTests
{
    [Fact]
    public void Sprint8P2_EndpointAndServiceAreFailClosedByDefault()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSecretProviderControlledRealReadStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read", program);
        Assert.Contains("CrmSecretProviderControlledRealReadStatusService", program);
        Assert.Contains("DisabledSecretProviderRuntime", program);
        Assert.Contains("SecretProviderControlledRealNonProductionRead", service);
        Assert.Contains("SecretProviderControlledRealNonProductionReadEnabled: false", service);
        Assert.Contains("SecretProviderControlledRealNonProductionReadAttempted: false", service);
        Assert.Contains("RealSecretReadAttempted: false", service);
        Assert.Contains("SecretValueReturnedToApi: false", service);
        Assert.Contains("SecretValuePersisted: false", service);
        Assert.Contains("SecretValueCached: false", service);
        Assert.Contains("Sprint8P3CommonDbControlledRealConnectivity", service);
    }

    [Fact]
    public void Sprint8P2_DoesNotIntroduceForbiddenRuntimeDependencies()
    {
        var root = FindRepositoryRoot();
        var p2Files = new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSecretProviderControlledRealReadContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSecretProviderControlledRealReadStatusService.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Security", "Secrets", "ISecretProviderRuntime.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Security", "Secrets", "SecretProviderRuntimeOptions.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Security", "Secrets", "DisabledSecretProviderRuntime.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Security", "Secrets", "ControlledNonProductionSecretProviderRuntime.cs")
        };

        var text = string.Join("\n", p2Files.Select(File.ReadAllText));

        Assert.DoesNotContain("SecretClient", text);
        Assert.DoesNotContain("DefaultAzureCredential", text);
        Assert.DoesNotContain("ManagedIdentityCredential", text);
        Assert.DoesNotContain("EnvironmentCredential", text);
        Assert.DoesNotContain("Environment.GetEnvironmentVariable", text);
        Assert.DoesNotContain("File.ReadAllText", text);
        Assert.DoesNotContain("SqlConnection", text);
        Assert.DoesNotContain("DbConnection", text);
        Assert.DoesNotContain("UseSqlServer", text);
        Assert.DoesNotContain("AddDbContext", text);
        Assert.DoesNotContain("HttpClient", text);
        Assert.DoesNotContain("Request.Headers", text);
        Assert.DoesNotContain("Headers[", text);
        Assert.DoesNotContain("AddAuthentication", text);
        Assert.DoesNotContain("UseAuthentication", text);
        Assert.DoesNotContain("JwtBearer", text);
        Assert.DoesNotContain("CookieAuthentication", text);
    }

    [Fact]
    public void Sprint8P2_DocumentationExists()
    {
        var root = FindRepositoryRoot();
        foreach (var path in new[]
        {
            "docs/security/crm-sprint-8-p2-secret-provider-controlled-real-nonproduction-read.md",
            "docs/security/crm-secret-provider-controlled-real-read-policy.md",
            "docs/security/crm-secret-provider-controlled-real-read-contract.md",
            "docs/security/crm-secret-provider-controlled-real-read-redaction.md",
            "docs/operations/crm-secret-provider-controlled-real-read-runbook.md",
            "docs/operations/crm-secret-provider-controlled-real-read-rollback.md",
            "docs/architecture/crm-secret-provider-controlled-real-read-architecture.md"
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
