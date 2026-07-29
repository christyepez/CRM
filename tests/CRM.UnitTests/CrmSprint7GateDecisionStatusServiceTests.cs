using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint7GateDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSprint7GateDecision()
    {
        var status = new CrmSprint7GateDecisionStatusService().GetStatus();

        Assert.Equal("Sprint7GateDecision", status.Status);
        Assert.True(status.FoundationMode);
        Assert.Equal("GoForSprint8ControlledRuntimeApprovalAndPilotPlanning", status.OverallDecision);
        Assert.Equal("NoGo", status.RealActivationDecision);
        Assert.Equal("NoGo", status.SecretProviderRealRuntimeDecision);
        Assert.Equal("NoGo", status.CommonDbRealConnectionDecision);
        Assert.Equal("NoGo", status.PortalAuthRealRuntimeDecision);
        Assert.Equal("GoOnlyAsExplicitNonProductionLocked423", status.LockedProductiveRouteRegistrationDecision);
        Assert.Equal("NoGo", status.ProductiveRoutesDefaultDecision);
        Assert.Equal("NoGo", status.ProductiveCrudDecision);
        Assert.Equal("NoGo", status.DeleteDecision);
        Assert.Equal("NoGo", status.ProductiveUiDecision);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal("Go", status.Sprint8PlanningDecision);
        Assert.Equal(CrmSprint7GateDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSprint7GateDecisionStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetSprint8Roadmap_ReturnsRecommendedPackages()
    {
        var roadmap = new CrmSprint7GateDecisionStatusService().GetSprint8Roadmap();

        Assert.Collection(
            roadmap,
            item => Assert.Equal("Sprint 8 P1", item.Package),
            item => Assert.Equal("Sprint 8 P2", item.Package),
            item => Assert.Equal("Sprint 8 P3", item.Package),
            item => Assert.Equal("Sprint 8 P4", item.Package),
            item => Assert.Equal("Sprint 8 P5", item.Package),
            item => Assert.Equal("Sprint 8 P6", item.Package));
    }
}
