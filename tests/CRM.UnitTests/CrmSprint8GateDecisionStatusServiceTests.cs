using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint8GateDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSprint8GateDecision()
    {
        var status = new CrmSprint8GateDecisionStatusService().GetStatus();

        Assert.Equal("Sprint8GateDecision", status.Status);
        Assert.True(status.FoundationMode);
        Assert.Equal("GoForSprint9ControlledRuntimeActivationPlanning", status.OverallDecision);
        Assert.Equal("NoGo", status.RealProductionActivationDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionFlag", status.SecretProviderControlledReadDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionFlag", status.CommonDbControlledConnectivityDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionFlag", status.PortalAuthControlledValidationDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionLocked423", status.LockedRouteAuthorizationPolicyDecision);
        Assert.Equal("NoGo", status.ProductiveRoutesDefaultDecision);
        Assert.Equal("NoGo", status.ProductiveCrudDecision);
        Assert.Equal("NoGo", status.DeleteDecision);
        Assert.Equal("NoGo", status.ProductiveUiDecision);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal("Go", status.Sprint9PlanningDecision);
        Assert.Equal(CrmSprint8GateDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSprint8GateDecisionStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetSprint9Roadmap_ReturnsRecommendedPackages()
    {
        var roadmap = new CrmSprint8GateDecisionStatusService().GetSprint9Roadmap();

        Assert.Collection(
            roadmap,
            item => Assert.Equal("Sprint 9 P1", item.Package),
            item => Assert.Equal("Sprint 9 P2", item.Package),
            item => Assert.Equal("Sprint 9 P3", item.Package),
            item => Assert.Equal("Sprint 9 P4", item.Package),
            item => Assert.Equal("Sprint 9 P5", item.Package),
            item => Assert.Equal("Sprint 9 P6", item.Package));
    }
}
