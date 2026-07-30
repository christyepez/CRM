using Xunit;

namespace CRM.ArchitectureTests;

public sealed class PortalAuthRuntimeValidationTrialArchitectureTests
{
    [Fact]
    public void Sprint9P4_EndpointServiceAndAdapterMarkersExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmPortalAuthRuntimeValidationTrialStatusService.cs"));
        var adapter = File.ReadAllText(Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "Auth", "PortalAuthRuntimeValidationTrialService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial", program);
        Assert.Contains("/api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial/probe", program);
        Assert.Contains("Crm:RuntimeTrials:PortalAuthValidationEnabled", program);
        Assert.Contains("PortalAuthRuntimeValidationTrial", service);
        Assert.Contains("Portal Auth runtime validation trial is disabled by default and never reads authorization headers or tokens", service);
        Assert.Contains("Sprint9P5ProductiveRouteDryRunTrial", service);
        Assert.Contains("IPortalAuthRuntimeValidationProbe", adapter);
        Assert.Contains("SecretNameNotAllowed", adapter);
        Assert.Contains("ProductionBlocked", adapter);
    }

    [Fact]
    public void Sprint9P4_DoesNotIntroduceForbiddenRuntime()
    {
        var root = FindRepositoryRoot();
        var text = string.Join("\n", new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmPortalAuthRuntimeValidationTrialContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmPortalAuthRuntimeValidationTrialStatusService.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "Auth", "PortalAuthRuntimeValidationTrialService.cs"),
            Path.Combine(root, "src", "CRM.Api", "Program.cs")
        }.Select(File.ReadAllText));

        Assert.DoesNotContain("MapDelete", text);
        Assert.DoesNotContain("UseAuthentication", text);
        Assert.DoesNotContain("UseAuthorization", text);
        Assert.DoesNotContain("AddAuthentication", text);
        Assert.DoesNotContain("JwtBearer", text);
        Assert.DoesNotContain("CookieAuthentication", text);
        Assert.DoesNotContain("HttpClient(", text);
        Assert.DoesNotContain("new HttpClient", text);
        Assert.DoesNotContain("Request.Headers", text);
        Assert.DoesNotContain("Headers[", text);
        Assert.DoesNotContain("local" + "Storage", text);
        Assert.DoesNotContain("session" + "Storage", text);
        Assert.DoesNotContain("UseSqlServer(", text);
        Assert.DoesNotContain("AddDbContext(", text);
        Assert.DoesNotContain("MigrationBuilder", text);
        Assert.DoesNotContain("SqlConnection(", text);
        Assert.DoesNotContain("DbConnection(", text);
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
