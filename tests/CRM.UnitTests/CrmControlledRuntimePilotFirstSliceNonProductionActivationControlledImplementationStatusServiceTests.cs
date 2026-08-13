using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDisabledOnlyP24ControlledImplementation()
    {
        var status = new CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService().GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal(CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService.StatusName, status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.CrmSprint10P24ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationExists);
        Assert.True(status.CrmSprint10P23FinalApprovalGateReviewed);
        Assert.Equal("PreparationOnly", status.ProductizationStatus);
        Assert.Equal("NoGo", status.ProductionActivationDecision);
        Assert.False(status.CrmProductionReady);
        Assert.True(status.NonProductionActivationControlledImplementationPrepared);
        Assert.False(status.NonProductionActivationControlledImplementationExecuted);
        Assert.True(status.ConditionalGoFutureDefined);
        Assert.False(status.ConditionalGoFutureExecuted);
        Assert.False(status.NonProductionActivationExecuted);
        Assert.True(status.ConditionalFutureGoDefined);
        Assert.False(status.ConditionalFutureGoExecuted);
        Assert.False(status.RuntimePortalCouplingEnabled);
        Assert.False(status.RuntimePortalCallsEnabled);
        Assert.False(status.ProductivePortalNavigationEnabled);
        Assert.False(status.ProductivePortalGatewayRoutesEnabled);
        Assert.False(status.CommonDbRuntimeEnabled);
        Assert.Equal("ControlledImplementationPreparedDisabledOnly", status.ControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationReadiness);
        Assert.Equal(CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService.NextGate, status.NextGate);
    }

    [Fact]
    public void GetFeatureFlags_DefaultsRemainFalse()
    {
        var flags = new CrmControlledRuntimePilotFirstSliceNonProductionActivationControlledImplementationStatusService().GetFeatureFlags();

        Assert.All(flags, flag => Assert.EndsWith("=false", flag));
        Assert.Contains("Crm:ControlledRuntimePilot:FirstSlice:NonProductionControlledImplementation:PortalClientEnabled=false", flags);
    }
}
