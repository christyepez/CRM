using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledOnlyP21Scaffold()
    {
        var status = new CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService().GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal(CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService.StatusName, status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CrmSprint10P21ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldExists);
        Assert.True(status.CrmSprint10P20ActivationReadinessReviewed);
        Assert.Equal("PreparationOnly", status.ProductizationStatus);
        Assert.Equal("NoGo", status.ProductionActivationDecision);
        Assert.False(status.CrmProductionReady);
        Assert.True(status.NonProductionActivationScaffoldOnly);
        Assert.False(status.NonProductionActivationExecuted);
        Assert.True(status.ConditionalFutureGoDefined);
        Assert.False(status.ConditionalFutureGoExecuted);
        Assert.False(status.RuntimePortalCouplingEnabled);
        Assert.False(status.RuntimePortalCallsEnabled);
        Assert.False(status.ProductivePortalNavigationEnabled);
        Assert.False(status.ProductivePortalGatewayRoutesEnabled);
        Assert.False(status.CommonDbRuntimeEnabled);
        Assert.Equal("NonProductionActivationScaffoldPreparedDisabledOnly", status.ControlledRuntimePilotFirstSliceNonProductionActivationScaffoldReadiness);
        Assert.Equal(CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService.NextGate, status.NextGate);
    }

    [Fact]
    public void GetFeatureFlags_DefaultsRemainFalse()
    {
        var flags = new CrmControlledRuntimePilotFirstSliceNonProductionActivationScaffoldStatusService().GetFeatureFlags();

        Assert.All(flags, flag => Assert.EndsWith("=false", flag));
        Assert.Contains("Crm:ControlledRuntimePilot:FirstSlice:NonProductionActivation:PortalClientEnabled=false", flags);
    }
}
