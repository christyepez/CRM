using Xunit;

namespace CRM.ArchitectureTests;

public sealed class SecretProviderRuntimeEnablementTrialArchitectureTests
{
    [Fact]
    public void Sprint9P2_EndpointServiceAndAdapterMarkersExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSecretProviderRuntimeEnablementTrialStatusService.cs"));
        var adapter = File.ReadAllText(Path.Combine(root, "src", "CRM.Infrastructure", "Security", "Secrets", "SecretProviderRuntimeTrialService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial", program);
        Assert.Contains("/api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial/probe", program);
        Assert.Contains("Crm:RuntimeTrials:SecretProviderEnabled", program);
        Assert.Contains("SecretProviderRuntimeEnablementTrial", service);
        Assert.Contains("Secret Provider runtime trial is disabled by default and never returns secret values", service);
        Assert.Contains("Sprint9P3CommonDbRuntimeConnectivityTrial", service);
        Assert.Contains("ISecretProviderRuntime", adapter);
        Assert.Contains("SecretNameNotAllowed", adapter);
        Assert.Contains("ProductionBlocked", adapter);
    }

    [Fact]
    public void Sprint9P2_DoesNotIntroduceForbiddenRuntime()
    {
        var root = FindRepositoryRoot();
        var text = string.Join("\n", new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSecretProviderRuntimeEnablementTrialContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSecretProviderRuntimeEnablementTrialStatusService.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Security", "Secrets", "SecretProviderRuntimeTrialService.cs"),
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
