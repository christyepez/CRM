using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint5GateDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSprint5GateDecisionWithoutRealActivation()
    {
        var status = new CrmSprint5GateDecisionStatusService().GetStatus();

        Assert.Equal("Sprint5GateDecision", status.Status);
        Assert.True(status.FoundationMode);
        Assert.Equal("GoForControlledNonProductionPreparation", status.OverallDecision);
        Assert.Equal("NoGo", status.RealActivationDecision);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal("NoGoForRuntimeRead", status.SecretProviderRuntimeDecision);
        Assert.Equal("NoGoForConnectionAttempt", status.CommonDbRuntimeDecision);
        Assert.Equal("NoGoForPortalHttpOrTokenRead", status.PortalAuthRuntimeDecision);
        Assert.Equal("NoGo", status.ProductiveRoutesDecision);
        Assert.Equal("NoGoForRuntimeRegistration", status.LockedStubRuntimeDecision);
        Assert.Equal("NoGo", status.ProductiveCrudDecision);
        Assert.Equal("NoGo", status.DeleteDecision);
        Assert.Equal("NoGo", status.ProductiveUiDecision);
        Assert.Equal("Go", status.Sprint6PlanningDecision);
        Assert.Equal(CrmSprint5GateDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSprint5GateDecisionStatusService.WarningText, status.Warning);
        Assert.Contains(status.CapabilityDecisions, decision => decision.Capability == "Secret Provider runtime" && decision.Decision == "NoGoForRuntimeRead");
        Assert.Contains(status.Sprint6Roadmap, item => item.Package == "Sprint 6 P1");
        Assert.Contains(status.BlockedItems, item => item == "Real activation");
    }
}
