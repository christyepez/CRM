using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmLockedRouteAuthorizationPolicyIntegrationStatusServiceTests
{
    [Fact]
    public void GetStatus_ReturnsDefaultFailClosedP5Contract()
    {
        var status = new CrmLockedRouteAuthorizationPolicyIntegrationStatusService().GetStatus();

        Assert.Equal("LockedRouteAuthorizationPolicyIntegration", status.Status);
        Assert.True(status.LockedRouteAuthorizationPolicyIntegrationExists);
        Assert.True(status.LockedRouteAuthorizationPolicyIntegrationApproved);
        Assert.False(status.LockedRouteAuthorizationPolicyIntegrationEnabled);
        Assert.False(status.AuthorizationPolicyEvaluated);
        Assert.Equal("NotEvaluatedBecauseDisabled", status.AuthorizationPolicyDecision);
        Assert.True(status.PortalAuthMetadataUsed);
        Assert.False(status.PortalAuthRuntimeRequired);
        Assert.False(status.PortalAuthRuntimeConnected);
        Assert.False(status.TokenReadAttempted);
        Assert.False(status.HeaderReadAttempted);
        Assert.False(status.AuthorizationHeaderReadAttempted);
        Assert.False(status.PortalHttpCallAttempted);
        Assert.False(status.ProductiveRoutesRegisteredByDefault);
        Assert.Equal(404, status.DefaultNegativeRouteStatus);
        Assert.True(status.LockedRoutesEnabledOnlyWithExplicitNonProductionFlag);
        Assert.Equal(423, status.LockedRouteStatus);
        Assert.False(status.LockedRouteAuthorizationDecisionReturned);
        Assert.False(status.ProductiveCrudEnabled);
        Assert.False(status.ProductiveDomainExecutionEnabled);
        Assert.False(status.ProductivePersistenceEnabled);
        Assert.False(status.DeleteEndpointsEnabled);
        Assert.False(status.SideEffectsAllowed);
        Assert.False(status.DbRuntimeEnabled);
        Assert.False(status.EfRuntimeEnabled);
        Assert.True(status.NonProductionOnly);
        Assert.True(status.FailClosedByDefault);
        Assert.Equal("Sprint8P6Sprint8GateDecision", status.NextGate);
        Assert.Contains("disabled by default", status.Warning);
        Assert.Equal(3, status.Routes.Count);
    }
}
