using CRM.Application.Foundation;
using Xunit;

namespace CRM.UnitTests;

public sealed class CrmLockedRouteAuthorizationPolicyEvaluatorTests
{
    [Fact]
    public void Evaluate_ReturnsNotEvaluatedWhenPolicyIsDisabled()
    {
        var result = new CrmLockedRouteAuthorizationPolicyEvaluator().Evaluate(new(
            Route: "/api/crm/leads",
            Method: "GET",
            LockedRegistrationEnabled: true,
            LockedAuthorizationPolicyEnabled: false,
            NonProduction: true));

        Assert.False(result.PolicyEvaluated);
        Assert.Equal("NotEvaluatedBecauseDisabled", result.Decision);
        Assert.True(result.Locked);
        Assert.False(result.TokenReadAttempted);
        Assert.False(result.HeaderReadAttempted);
        Assert.False(result.AuthorizationHeaderReadAttempted);
        Assert.False(result.PortalHttpCallAttempted);
        Assert.False(result.SideEffectsAllowed);
        Assert.False(result.ProductiveCrudEnabled);
        Assert.False(result.ProductiveDomainExecutionEnabled);
        Assert.False(result.ProductivePersistenceEnabled);
        Assert.False(result.DeleteEndpointsEnabled);
    }

    [Fact]
    public void Evaluate_ReturnsBlockedBecauseRouteLockedOnlyWithExplicitNonProductionPolicy()
    {
        var result = new CrmLockedRouteAuthorizationPolicyEvaluator().Evaluate(new(
            Route: "/api/crm/accounts",
            Method: "post",
            LockedRegistrationEnabled: true,
            LockedAuthorizationPolicyEnabled: true,
            NonProduction: true));

        Assert.True(result.PolicyEvaluated);
        Assert.Equal("BlockedBecauseRouteLocked", result.Decision);
        Assert.Equal("POST", result.Method);
        Assert.False(result.PortalAuthRuntimeConnected);
        Assert.False(result.TokenReadAttempted);
        Assert.False(result.HeaderReadAttempted);
        Assert.False(result.SideEffectsAllowed);
        Assert.False(result.ProductiveCrudEnabled);
        Assert.Equal("Sprint8P6Sprint8GateDecision", result.NextGate);
    }
}
