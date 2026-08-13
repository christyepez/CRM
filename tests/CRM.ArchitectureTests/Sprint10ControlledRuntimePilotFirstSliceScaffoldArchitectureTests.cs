using Xunit;

namespace CRM.ArchitectureTests;

public sealed class Sprint10ControlledRuntimePilotFirstSliceScaffoldArchitectureTests
{
    [Fact]
    public void Sprint10P14_EndpointServiceAndDisabledClientExist()
    {
        var root = GetRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmControlledRuntimePilotFirstSliceScaffoldStatusService.cs"));
        var client = File.ReadAllText(Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "ControlledRuntimePilot", "DisabledPortalRuntimeClient.cs"));

        Assert.Contains("/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-scaffold", program);
        Assert.Contains("CrmControlledRuntimePilotFirstSliceScaffoldStatusService", program);
        Assert.Contains("FirstSliceScaffoldPreparedDisabledOnly", service);
        Assert.Contains("ExternalCallAttempted: false", client);
    }

    [Fact]
    public void Sprint10P14_DoesNotActivatePortalRuntimeOrProduction()
    {
        var root = GetRepositoryRoot();
        var files = new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmControlledRuntimePilotFirstSliceScaffoldStatusService.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "ControlledRuntimePilot", "PortalRuntimeOptions.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "ControlledRuntimePilot", "DisabledPortalRuntimeClient.cs")
        };
        var text = string.Join(Environment.NewLine, files.Select(File.ReadAllText));

        Assert.Contains("ProductionActivationDecision: \"NoGo\"", text);
        Assert.Contains("CrmProductionReady: false", text);
        Assert.Contains("RuntimePortalCallsEnabled: false", text);
        Assert.Contains("CommonDbRuntimeEnabled: false", text);
        Assert.Contains("ConditionalFutureGoExecuted: false", text);
        Assert.DoesNotContain("HttpClient", text);
        Assert.DoesNotContain("UseSqlServer", text);
        Assert.DoesNotContain("client_secret", text, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("local" + "Storage", text, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("session" + "Storage", text, StringComparison.OrdinalIgnoreCase);
    }

    private static string GetRepositoryRoot()
    {
        var current = AppContext.BaseDirectory;
        while (!string.IsNullOrWhiteSpace(current))
        {
            if (File.Exists(Path.Combine(current, "CRM.sln")))
            {
                return current;
            }

            current = Directory.GetParent(current)?.FullName ?? string.Empty;
        }

        throw new InvalidOperationException("Repository root was not found.");
    }
}
