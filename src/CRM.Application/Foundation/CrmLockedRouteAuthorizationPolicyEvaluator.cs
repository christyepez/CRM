namespace CRM.Application.Foundation;

public sealed class CrmLockedRouteAuthorizationPolicyEvaluator
{
    public const string DisabledDecision = "NotEvaluatedBecauseDisabled";
    public const string LockedDecision = "BlockedBecauseRouteLocked";
    public const string NextGate = "Sprint8P6Sprint8GateDecision";

    public CrmLockedRouteAuthorizationPolicyEvaluationResult Evaluate(CrmLockedRouteAuthorizationPolicyEvaluationRequest request)
    {
        var evaluated = request.LockedRegistrationEnabled &&
            request.LockedAuthorizationPolicyEnabled &&
            request.NonProduction;

        return new(
            Route: request.Route,
            Method: request.Method.ToUpperInvariant(),
            Locked: request.LockedRegistrationEnabled && request.NonProduction,
            PolicyEvaluated: evaluated,
            Decision: evaluated ? LockedDecision : DisabledDecision,
            PortalAuthRuntimeConnected: false,
            TokenReadAttempted: false,
            HeaderReadAttempted: false,
            AuthorizationHeaderReadAttempted: false,
            PortalHttpCallAttempted: false,
            SideEffectsAllowed: false,
            ProductiveCrudEnabled: false,
            ProductiveDomainExecutionEnabled: false,
            ProductivePersistenceEnabled: false,
            DeleteEndpointsEnabled: false,
            NextGate: NextGate);
    }
}
