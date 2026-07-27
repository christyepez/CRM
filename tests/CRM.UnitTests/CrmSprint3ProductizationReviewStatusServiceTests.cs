using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmSprint3ProductizationReviewStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsNoGoForRealActivation()
    {
        var status = new CrmSprint3ProductizationReviewStatusService().GetStatus();

        Assert.Equal("Sprint3ProductizationReview", status.Status);
        Assert.True(status.FoundationMode);
        Assert.Equal("NoGoForRealActivation", status.OverallDecision);
        Assert.Equal("NotReady", status.ProductizationStatus);
        Assert.Equal("NoGo", status.DurablePersistenceDecision);
        Assert.Equal("NoGo", status.RealDatabaseDecision);
        Assert.Equal("NoGo", status.EfRuntimeDecision);
        Assert.Equal("NoGo", status.PortalAuthRuntimeDecision);
        Assert.Equal("NoGo", status.ProductiveApiRoutesDecision);
        Assert.Equal("NoGo", status.ProductiveCrmUiDecision);
        Assert.Equal("GoFoundationOnly", status.FoundationCapabilitiesDecision);
        Assert.Equal("Go", status.Sprint4PlanningDecision);
        Assert.Equal(CrmSprint3ProductizationReviewStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmSprint3ProductizationReviewStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetStatus_KeepsAllRuntimeActivationFlagsDisabled()
    {
        var status = new CrmSprint3ProductizationReviewStatusService().GetStatus();

        Assert.False(status.ProductiveRoutesRegistered);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.False(status.AuthRuntimeEnabled);
        Assert.False(status.PortalRuntimeConnected);
        Assert.False(status.DurablePersistenceEnabled);
        Assert.False(status.RealDatabaseConfigured);
        Assert.False(status.EfRuntimeEnabled);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.True(status.FoundationCrudStillSeparate);
    }
}
