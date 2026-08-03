using Xunit;

namespace CRM.ArchitectureTests;

public sealed class Sprint10ProductizationReadinessDecisionArchitectureTests
{
    [Fact]
    public void Sprint10P1_EndpointServiceAndDecisionMarkersExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var contracts = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint10ProductizationReadinessDecisionContracts.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint10ProductizationReadinessDecisionStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-10/productization-readiness-decision", program);
        Assert.Contains("CrmSprint10ProductizationReadinessDecisionStatusService", program);
        Assert.DoesNotContain("/api/crm/foundation/sprint-10/productization-readiness-decision/probe", program);
        Assert.Contains("CrmSprint10ProductizationReadinessDecisionStatusResponse", contracts);
        Assert.Contains("GoForControlledNonProductionProductizationPreparation", service);
        Assert.Contains("NoGoForProduction", service);
        Assert.Contains("NoGoUntilP5", service);
        Assert.Contains("Sprint10P2CommonDbControlledActivationPlan", service);
        Assert.Contains("Sprint 10 P1 Productization Readiness Decision: Exists", service);
    }

    [Fact]
    public void Sprint10P1_DoesNotIntroduceRuntimeActivation()
    {
        var root = FindRepositoryRoot();
        var text = string.Join("\n", new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint10ProductizationReadinessDecisionContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint10ProductizationReadinessDecisionStatusService.cs"),
            Path.Combine(root, "src", "CRM.Api", "Program.cs")
        }.Select(File.ReadAllText));

        Assert.DoesNotContain("/api/crm/foundation/sprint-10/productization-readiness-decision/probe", text);
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
