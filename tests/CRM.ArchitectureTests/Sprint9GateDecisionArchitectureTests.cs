using Xunit;

namespace CRM.ArchitectureTests;

public sealed class Sprint9GateDecisionArchitectureTests
{
    [Fact]
    public void Sprint9P6_EndpointServiceAndGateMarkersExist()
    {
        var root = FindRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var contracts = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint9GateDecisionContracts.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint9GateDecisionStatusService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-9/gate-decision", program);
        Assert.Contains("CrmSprint9GateDecisionStatusService", program);
        Assert.DoesNotContain("/api/crm/foundation/sprint-9/gate-decision/probe", program);
        Assert.Contains("CrmSprint9GateDecisionStatusResponse", contracts);
        Assert.Contains("GoForSprint10ControlledProductizationReadinessPlanning", service);
        Assert.Contains("NoGoForProduction", service);
        Assert.Contains("Sprint10P1ProductizationReadinessDecision", service);
        Assert.Contains("Sprint 9 gate decision only; production activation remains NoGo", service);
    }

    [Fact]
    public void Sprint9P6_DoesNotIntroduceProductiveRuntimeActivation()
    {
        var root = FindRepositoryRoot();
        var text = string.Join("\n", new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint9GateDecisionContracts.cs"),
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmSprint9GateDecisionStatusService.cs"),
            Path.Combine(root, "src", "CRM.Api", "Program.cs")
        }.Select(File.ReadAllText));

        Assert.DoesNotContain("/api/crm/foundation/sprint-9/gate-decision/probe", text);
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
