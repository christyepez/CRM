using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmLockedProductiveRouteStubTrialStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsLockedStubTrialWithoutRuntimeRegistration()
    {
        var status = new CrmLockedProductiveRouteStubTrialStatusService().GetStatus();

        Assert.Equal("LockedProductiveRouteStubTrial", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.LockedProductiveRouteStubTrialExists);
        Assert.False(status.LockedProductiveRouteStubRegistrationApproved);
        Assert.False(status.LockedProductiveRouteStubsRegistered);
        Assert.False(status.ProductiveRoutesRegistered);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ProductiveAuthorizationEnabled);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.False(status.RuntimeFlagDefaultEnabled);
        Assert.Equal(423, status.LockedResponseIfEnabled);
        Assert.Equal(404, status.DefaultNegativeRouteStatus);
        Assert.True(status.FoundationCrudStillSeparate);
        Assert.False(status.DbRequired);
        Assert.False(status.AuthRuntimeRequired);
        Assert.False(status.PortalRuntimeRequired);
        Assert.Equal(CrmLockedProductiveRouteStubTrialStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmLockedProductiveRouteStubTrialStatusService.WarningText, status.Warning);
        Assert.All(status.FutureRoutes, route =>
        {
            Assert.False(route.RegisteredByDefault);
            Assert.Equal(423, route.LockedResponseIfEnabled);
            Assert.DoesNotContain("DELETE", route.Method);
        });
        Assert.Contains(status.Decisions, decision => decision.Decision == "Stub trial strategy" && decision.Value == CrmLockedProductiveRouteStubTrialStatusService.StubTrialDecision);
    }
}
