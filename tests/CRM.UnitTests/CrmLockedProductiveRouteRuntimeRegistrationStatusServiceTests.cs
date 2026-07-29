using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmLockedProductiveRouteRuntimeRegistrationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsP5LockedRegistrationContract()
    {
        var status = new CrmLockedProductiveRouteRuntimeRegistrationStatusService().GetStatus();

        Assert.Equal("LockedProductiveRouteRuntimeRegistrationWith423", status.Status);
        Assert.True(status.FoundationMode);
        Assert.True(status.LockedProductiveRouteRuntimeRegistrationExists);
        Assert.False(status.LockedProductiveRouteRuntimeRegistrationApprovalGranted);
        Assert.False(status.LockedProductiveRouteRuntimeRegistrationEnabled);
        Assert.False(status.ProductiveRoutesRegisteredByDefault);
        Assert.True(status.ProductiveRoutesRegisteredWhenExplicitlyEnabled);
        Assert.Equal(404, status.DefaultNegativeRouteStatus);
        Assert.Equal(423, status.ExplicitlyEnabledLockedRouteStatus);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ProductiveDomainExecutionEnabled);
        Assert.False(status.ProductivePersistenceEnabled);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.False(status.PortalAuthRuntimeEnabled);
        Assert.False(status.DbRuntimeEnabled);
        Assert.False(status.SideEffectsAllowed);
        Assert.Equal(CrmLockedProductiveRouteRuntimeRegistrationStatusService.NextGate, status.NextGate);
        Assert.Equal(CrmLockedProductiveRouteRuntimeRegistrationStatusService.WarningText, status.Warning);
    }

    [Fact]
    public void GetRoutes_ReturnsPlannedLockedRoutesWithoutDeleteOrRuntime()
    {
        var routes = new CrmLockedProductiveRouteRuntimeRegistrationStatusService().GetRoutes();

        Assert.Collection(
            routes,
            route => Assert.Equal("/api/crm/leads", route.Route),
            route => Assert.Equal("/api/crm/accounts", route.Route),
            route => Assert.Equal("/api/crm/contacts", route.Route));

        foreach (var route in routes)
        {
            Assert.Equal(["GET", "POST", "PUT", "PATCH"], route.Methods);
            Assert.Equal(404, route.DefaultStatus);
            Assert.Equal(423, route.ExplicitlyEnabledStatus);
            Assert.False(route.DeleteEnabled);
            Assert.False(route.DomainExecutionEnabled);
            Assert.False(route.PersistenceEnabled);
            Assert.False(route.PortalAuthRuntimeEnabled);
        }
    }
}
