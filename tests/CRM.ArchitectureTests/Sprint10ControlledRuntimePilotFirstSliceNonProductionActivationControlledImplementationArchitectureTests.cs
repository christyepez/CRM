using Xunit;

namespace CRM.ArchitectureTests;

public sealed class Sprint10ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationArchitectureTests
{
    [Fact]
    public void Sprint10P24_EndpointServiceAndDisabledActivationServiceExist()
    {
        var root = GetRepositoryRoot();
        var program = File.ReadAllText(Path.Combine(root, "src", "CRM.Api", "Program.cs"));
        var service = File.ReadAllText(Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService.cs"));
        var disabledService = File.ReadAllText(Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "ControlledRuntimePilot", "DisabledControlledNonProductionActivationService.cs"));

        Assert.Contains("/api/crm/foundation/sprint-10/controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-implementation", program);
        Assert.Contains("CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService", program);
        Assert.Contains("ControlledImplementationPreparedDisabledOnly", service);
        Assert.Contains("ExternalCallAttempted: false", disabledService);
    }

    [Fact]
    public void Sprint10P24_DoesNotActivatePortalRuntimeOrProduction()
    {
        var root = GetRepositoryRoot();
        var files = new[]
        {
            Path.Combine(root, "src", "CRM.Application", "Foundation", "CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "ControlledRuntimePilot", "ControlledNonProductionActivationOptions.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "ControlledRuntimePilot", "ControlledNonProductionActivationFeatureFlags.cs"),
            Path.Combine(root, "src", "CRM.Infrastructure", "Portal", "ControlledRuntimePilot", "DisabledControlledNonProductionActivationService.cs")
        };
        var text = string.Join(Environment.NewLine, files.Select(File.ReadAllText));

        Assert.Contains("ProductionActivationDecision: \"NoGo\"", text);
        Assert.Contains("CrmProductionReady: false", text);
        Assert.Contains("RuntimePortalCallsEnabled: false", text);
        Assert.Contains("RuntimePortalCouplingEnabled: false", text);
        Assert.Contains("CommonDbRuntimeEnabled: false", text);
        Assert.Contains("ConditionalGoFutureExecuted: false", text);
        Assert.Contains("ControlledImplementationExecuted: false", text);
        Assert.DoesNotContain("Http" + "Client", text);
        Assert.DoesNotContain("Use" + "SqlServer", text);
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
