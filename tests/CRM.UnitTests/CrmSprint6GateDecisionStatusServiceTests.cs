using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint6GateDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSprint6GateDecisionWithoutRealActivation()
    {
        var status = new CrmSprint6GateDecisionStatusService().GetStatus();

        Assert.Equal("Sprint6GateDecision", status.Status);
        Assert.True(status.FoundationMode);
        Assert.Equal("GoForSprint7ControlledNonProductionActivationPlanning", status.OverallDecision);
        Assert.Equal("NoGo", status.RealActivationDecision);
        Assert.Equal("NoGo", status.SecretProviderRealRuntimeDecision);
        Assert.Equal("NoGo", status.CommonDbRealConnectionDecision);
        Assert.Equal("NoGo", status.PortalAuthRealRuntimeDecision);
        Assert.Equal("NoGo", status.LockedStubRuntimeRegistrationDecision);
        Assert.Equal("NoGo", status.ProductiveRoutesDecision);
        Assert.Equal("NoGo", status.ProductiveCrudDecision);
        Assert.Equal("NoGo", status.DeleteDecision);
        Assert.Equal("NoGo", status.ProductiveUiDecision);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal("Go", status.Sprint7PlanningDecision);
        Assert.Equal(CrmSprint6GateDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSprint6GateDecisionStatusService.WarningText, status.Warning);
        Assert.Contains(status.CapabilityDecisions, decision => decision.Capability == "Productive Routes" && decision.Decision == "NoGo");
        Assert.Contains(status.Sprint7Roadmap, item => item.Package == "Sprint 7 P1");
        Assert.Contains(status.BlockedItems, item => item == "Real secret provider runtime");
    }
}
