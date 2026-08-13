using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmControlledRuntimePilotFirstSliceScaffoldStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledOnlyP14Scaffold()
    {
        var status = new CrmControlledRuntimePilotFirstSliceScaffoldStatusService().GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal(CrmControlledRuntimePilotFirstSliceScaffoldStatusService.StatusName, status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CrmSprint10P14ControlledRuntimePilotFirstImplementationSliceScaffoldExists);
        Assert.True(status.CrmSprint10P13FirstSliceDesignReviewed);
        Assert.Equal("PreparationOnly", status.ProductizationStatus);
        Assert.Equal("NoGo", status.ProductionActivationDecision);
        Assert.False(status.CrmProductionReady);
        Assert.True(status.FirstImplementationSliceScaffoldOnly);
        Assert.True(status.ConditionalFutureGoDefined);
        Assert.False(status.ConditionalFutureGoExecuted);
        Assert.False(status.RuntimePortalCouplingEnabled);
        Assert.False(status.RuntimePortalCallsEnabled);
        Assert.False(status.ProductivePortalNavigationEnabled);
        Assert.False(status.ProductivePortalGatewayRoutesEnabled);
        Assert.False(status.CommonDbRuntimeEnabled);
        Assert.Equal("FirstSliceScaffoldPreparedDisabledOnly", status.ControlledRuntimePilotFirstImplementationSliceScaffoldReadiness);
        Assert.Equal(CrmControlledRuntimePilotFirstSliceScaffoldStatusService.NextGate, status.NextGate);
    }

    [Fact]
    public void GetFeatureFlags_DefaultsRemainFalse()
    {
        var flags = new CrmControlledRuntimePilotFirstSliceScaffoldStatusService().GetFeatureFlags();

        Assert.All(flags, flag => Assert.EndsWith("=false", flag));
        Assert.Contains("Crm:ControlledRuntimePilot:FirstSlice:PortalClientEnabled=false", flags);
    }
}
