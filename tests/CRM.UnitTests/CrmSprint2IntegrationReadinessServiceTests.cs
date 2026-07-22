using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint2IntegrationReadinessServiceTests
{
    [Fact]
    public void GetStatus_ReturnsNotReadyAndContinueReviewDecision()
    {
        var status = new CrmSprint2IntegrationReadinessService().GetStatus();

        Assert.Equal("IntegrationReadinessReview", status.Status);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.False(status.DatabaseReady);
        Assert.False(status.AuthReady);
        Assert.False(status.ProductiveCrudReady);
        Assert.True(status.FoundationCrudEnabled);
        Assert.False(status.DurablePersistence);
        Assert.False(status.PortalRuntimeConnected);
        Assert.Equal("ContinueReview", status.RecommendedDecision);
        Assert.Equal("Sprint2P6ProductizationGateDecision", status.NextGate);
        Assert.Equal(CrmSprint2IntegrationReadinessService.WarningText, status.Warning);
        Assert.Contains(status.Gates, gate => gate.Gate == "DurableDatabase" && !gate.Ready);
        Assert.Contains(status.Risks, risk => risk.BlocksProductization);
        Assert.Contains(status.Decisions, decision => decision.RecommendedDecision == "DoNotActivate");
    }
}
