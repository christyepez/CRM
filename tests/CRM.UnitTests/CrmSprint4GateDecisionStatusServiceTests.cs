using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint4GateDecisionStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsSprint4GateDecisionWithoutRealActivation()
    {
        var status = new CrmSprint4GateDecisionStatusService().GetStatus();

        Assert.Equal("Sprint4GateDecision", status.Status);
        Assert.True(status.FoundationMode);
        Assert.Equal("GoForNonProductionFoundationPilot", status.OverallDecision);
        Assert.Equal("NoGo", status.RealActivationDecision);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal("NoGo", status.DurablePersistenceDecision);
        Assert.Equal("NoGoForRuntimeActivation", status.CommonDbRuntimeDecision);
        Assert.Equal("NoGoForRuntimeActivation", status.PortalAuthRuntimeDecision);
        Assert.Equal("NoGo", status.ProductiveRoutesDecision);
        Assert.Equal("NoGo", status.ProductiveCrudDecision);
        Assert.Equal("NoGo", status.DeleteDecision);
        Assert.Equal("NoGo", status.ProductiveUiDecision);
        Assert.Equal("GoFoundationOnly", status.NonProductionE2EPilotDecision);
        Assert.Equal("Go", status.Sprint5PlanningDecision);
        Assert.Equal(CrmSprint4GateDecisionStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSprint4GateDecisionStatusService.WarningText, status.Warning);
        Assert.All(status.CapabilityDecisions, decision => Assert.False(decision.RealActivationAllowed));
        Assert.Contains(status.Evidence, evidence => evidence.Area == "Negative routes" && evidence.Passed);
        Assert.Contains(status.Sprint5Roadmap, item => item.Gate == "Sprint5P1ControlledRuntimeProbeActivationPlan");
        Assert.Contains("Productive CRM routes and DELETE endpoints.", status.BlockedItems);
    }
}
