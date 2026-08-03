using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint10ProductizationReadinessDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsPreparationOnlySprint10P1Decision()
    {
        var status = new CrmSprint10ProductizationReadinessDecisionStatusService().GetStatus();

        Assert.Equal("CRM", status.Module);
        Assert.Equal("Sprint10P1ProductizationReadinessDecision", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.Sprint10P1ProductizationReadinessDecisionExists);
        Assert.True(status.Sprint10P1Approved);
        Assert.True(status.Sprint9GateReviewed);
        Assert.True(status.Sprint9ProductionNoGoPreserved);
        Assert.Equal("GoForControlledNonProductionProductizationPreparation", status.Sprint10P1Decision);
        Assert.Equal("NoGo", status.ProductionActivationDecision);
        Assert.Equal("NoGoForProduction", status.ProductiveRuntimeActivationDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionPreparation", status.CommonDbControlledActivationDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionPreparation", status.PortalAuthControlledActivationDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionPreparation", status.ProductiveRouteControlledActivationDecision);
        Assert.Equal("NoGoUntilP5", status.ProductiveCrudPilotDecision);
        Assert.Equal("NoGo", status.ProductiveUiDecision);
        Assert.False(status.ProductionActivationApproved);
        Assert.False(status.ProductiveRuntimeActivationApprovedForProduction);
        Assert.True(status.CommonDbControlledPreparationApproved);
        Assert.True(status.PortalAuthControlledPreparationApproved);
        Assert.True(status.ProductiveRouteControlledPreparationApproved);
        Assert.False(status.ProductiveCrudPilotApproved);
        Assert.False(status.ProductiveUiApproved);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.ExplicitFlagsRequired);
        Assert.True(status.FailClosedByDefault);
        Assert.True(status.ObservabilityMetadataOnly);
        Assert.True(status.RollbackAvailable);
        Assert.Equal("PreparationOnly", status.ProductizationStatus);
        Assert.Equal(CrmSprint10ProductizationReadinessDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSprint10ProductizationReadinessDecisionStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetDecisions_KeepsProductionAndCrudBlocked()
    {
        var decisions = new CrmSprint10ProductizationReadinessDecisionStatusService().GetDecisions();

        Assert.Contains(decisions, item => item.Decision == "NoGo" && item.Area == "Production Activation");
        Assert.Contains(decisions, item => item.Decision == "NoGoForProduction");
        Assert.Contains(decisions, item => item.Decision == "NoGoUntilP5");
    }
}
