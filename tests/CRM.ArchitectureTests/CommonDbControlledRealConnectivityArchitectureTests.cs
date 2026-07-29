using Xunit;

namespace CRM.ArchitectureTests;

public sealed class CommonDbControlledRealConnectivityArchitectureTests
{
    [Fact]
    public void Sprint8P3_EndpointAndServiceAreFailClosedByDefault()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmCommonDbControlledRealConnectivityStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-8/common-db-controlled-real-connectivity", program);
        Assert.Contains("CrmCommonDbControlledRealConnectivityStatusService", program);
        Assert.Contains("DisabledCommonDbConnectivityProbe", program);
        Assert.Contains("CommonDbControlledRealConnectivity", service);
        Assert.Contains("CommonDbControlledRealConnectivityEnabled: false", service);
        Assert.Contains("CommonDbConnectivityAttempted: false", service);
        Assert.Contains("CommonDbConnected: false", service);
        Assert.Contains("ConnectionStringReturnedToApi: false", service);
        Assert.Contains("MigrationsCreated: false", service);
        Assert.Contains("Sprint8P4PortalAuthControlledRealRuntimeValidation", service);
    }

    [Fact]
    public void Sprint8P3_DoesNotIntroduceForbiddenRuntimeDependencies()
    {
        var root = FindRepositoryRoot();
        var p3Files = new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmCommonDbControlledRealConnectivityContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmCommonDbControlledRealConnectivityStatusService.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Persistence", "RuntimeProbe", "ICommonDbConnectivityProbe.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Persistence", "RuntimeProbe", "CommonDbConnectivityProbeOptions.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Persistence", "RuntimeProbe", "DisabledCommonDbConnectivityProbe.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Persistence", "RuntimeProbe", "ControlledNonProductionCommonDbConnectivityProbe.cs")
        };

        var text = string.Join("\n", p3Files.Select(File.ReadAllText));

        Assert.DoesNotContain("System.Data.SqlClient", text);
        Assert.DoesNotContain("Microsoft.Data.SqlClient", text);
        Assert.DoesNotContain("UseSqlServer(", text);
        Assert.DoesNotContain("AddDbContext(", text);
        Assert.DoesNotContain("DbContext(", text);
        Assert.DoesNotContain("MigrationBuilder", text);
        Assert.DoesNotContain("HttpClient", text);
        Assert.DoesNotContain("Request.Headers", text);
        Assert.DoesNotContain("Headers[", text);
        Assert.DoesNotContain("AddAuthentication", text);
        Assert.DoesNotContain("UseAuthentication", text);
        Assert.DoesNotContain("JwtBearer", text);
        Assert.DoesNotContain("CookieAuthentication", text);
    }

    [Fact]
    public void Sprint8P3_DocumentationExists()
    {
        var root = FindRepositoryRoot();
        foreach (var path in new[]
        {
            "docs/data/crm-sprint-8-p3-common-db-controlled-real-connectivity.md",
            "docs/data/crm-common-db-controlled-real-connectivity-policy.md",
            "docs/data/crm-common-db-controlled-real-connectivity-contract.md",
            "docs/data/crm-common-db-controlled-real-connectivity-safety-boundary.md",
            "docs/operations/crm-common-db-controlled-real-connectivity-runbook.md",
            "docs/operations/crm-common-db-controlled-real-connectivity-rollback.md",
            "docs/architecture/crm-common-db-controlled-real-connectivity-architecture.md"
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
