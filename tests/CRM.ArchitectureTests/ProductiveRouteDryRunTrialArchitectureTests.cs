using Xunit;

namespace CRM.ArchitectureTests;

public sealed class ProductiveRouteDryRunTrialArchitectureTests
{
    [Fact]
    public void Sprint9P5_EndpointServiceAndTrialMarkersExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var statusService = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmProductiveRouteDryRunTrialStatusService.cs"));
        var evaluator = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmProductiveRouteDryRunTrialEvaluator.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "ProductiveRoutes", "ProductiveRouteDryRunTrialService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-9/productive-route-dry-run-trial", program);
        Assert.Contains("/api/crm/foundation/sprint-9/productive-route-dry-run-trial/probe", program);
        Assert.Contains("Crm:RuntimeTrials:ProductiveRouteDryRunEnabled", program);
        Assert.Contains("ProductiveRouteDryRunTrial", statusService);
        Assert.Contains("Productive route dry-run trial is disabled by default and never registers productive CRM routes", statusService);
        Assert.Contains("Sprint9P6Sprint9GateDecision", statusService);
        Assert.Contains("DatabaseWriteAttempted: false", statusService);
        Assert.Contains("SideEffectsAllowed: false", statusService);
        Assert.Contains("FlagDisabled", evaluator);
        Assert.Contains("ProductionBlocked", evaluator);
        Assert.Contains("DeleteBlocked", evaluator);
        Assert.Contains("PortalAuthMetadataDependencyValidated: true", service);
    }

    [Fact]
    public void Sprint9P5_DoesNotIntroduceProductiveRuntimeSideEffects()
    {
        var root = FindRepositoryRoot();
        var text = string.Join("\n", new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmProductiveRouteDryRunTrialContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmProductiveRouteDryRunTrialStatusService.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmProductiveRouteDryRunTrialEvaluator.cs"),
            Path.Combine(root, "src", "CRM.Api", "ProductiveRoutes", "ProductiveRouteDryRunTrialService.cs"),
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
