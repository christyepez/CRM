using CRM.Infrastructure.Portal.ControlledRuntimePilot;
using Xunit;

namespace CRM.UnitTests;

public sealed class DisabledNonProductionActivationServiceTests
{
    [Fact]
    public void GetDryRunResult_RemainsNoOpAndFailClosed()
    {
        var service = new DisabledNonProductionActivationService(
            NonProductionActivationOptions.Disabled(),
            NonProductionActivationFeatureFlags.Disabled());

        var result = service.GetDryRunResult();

        Assert.True(result.DryRunOnly);
        Assert.False(result.ActivationAttempted);
        Assert.False(result.ActivationExecuted);
        Assert.False(result.ExternalCallAttempted);
        Assert.False(result.PortalCouplingEnabled);
        Assert.False(result.FeatureFlagsEnabled);
        Assert.Equal("Locked", result.Status);
    }

    [Fact]
    public void DisabledFeatureFlags_ReportNoEnabledFlags()
    {
        var flags = NonProductionActivationFeatureFlags.Disabled();

        Assert.False(flags.FirstSliceEnabled);
        Assert.False(flags.PortalClientEnabled);
        Assert.False(flags.HealthSmokeEnabled);
        Assert.False(flags.GatewayRoutesEnabled);
        Assert.False(flags.PortalNavigationEnabled);
        Assert.False(flags.AnyEnabled);
    }
}
