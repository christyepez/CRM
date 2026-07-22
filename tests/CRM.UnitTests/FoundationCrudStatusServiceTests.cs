using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class FoundationCrudStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsP4FoundationCrudGate()
    {
        var status = new FoundationCrudStatusService().GetStatus();

        Assert.Equal("FoundationCrudEnabled", status.Status);
        Assert.True(status.FoundationCrudEnabled);
        Assert.True(status.LeadFoundationCrudEnabled);
        Assert.True(status.AccountFoundationCrudEnabled);
        Assert.True(status.ContactFoundationCrudEnabled);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.DurablePersistence);
        Assert.False(status.DatabaseConfigured);
        Assert.False(status.PortalRuntimeConnected);
        Assert.Equal("FoundationSimulation", status.AuthorizationMode);
        Assert.Equal("Sprint2P5IntegrationReadinessReview", status.NextGate);
    }
}
