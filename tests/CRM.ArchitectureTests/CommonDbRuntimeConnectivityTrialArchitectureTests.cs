using Xunit;

namespace CRM.ArchitectureTests;

public sealed class CommonDbRuntimeConnectivityTrialArchitectureTests
{
    [Fact]
    public void Sprint9P3_EndpointServiceAndAdapterMarkersExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmCommonDbRuntimeConnectivityTrialStatusService.cs"));
        var adapter = File.ReadAllText(Path.Combine(root, "src", "CRM.Infrastructure", "Data", "CommonDb", "CommonDbRuntimeConnectivityTrialService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial", program);
        Assert.Contains("/api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial/probe", program);
        Assert.Contains("Crm:RuntimeTrials:CommonDbConnectivityEnabled", program);
        Assert.Contains("CommonDbRuntimeConnectivityTrial", service);
        Assert.Contains("Common DB runtime connectivity trial is disabled by default and never exposes connection strings", service);
        Assert.Contains("Sprint9P4PortalAuthRuntimeValidationTrial", service);
        Assert.Contains("ICommonDbConnectivityProbe", adapter);
        Assert.Contains("SecretNameNotAllowed", adapter);
        Assert.Contains("ProductionBlocked", adapter);
    }

    [Fact]
    public void Sprint9P3_DoesNotIntroduceForbiddenRuntime()
    {
        var root = FindRepositoryRoot();
        var text = string.Join("\n", new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmCommonDbRuntimeConnectivityTrialContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmCommonDbRuntimeConnectivityTrialStatusService.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Data", "CommonDb", "CommonDbRuntimeConnectivityTrialService.cs"),
            Path.Combine(root, "src", "CRM.Api", "Program.cs")
        }.Select(File.ReadAllText));

        Assert.DoesNotContain("MapDelete", text);
        Assert.DoesNotContain("UseSqlServer(", text);
        Assert.DoesNotContain("AddDbContext(", text);
        Assert.DoesNotContain("MigrationBuilder", text);
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
