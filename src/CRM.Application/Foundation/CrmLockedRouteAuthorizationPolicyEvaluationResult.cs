namespace CRM.Application.Foundation;

public sealed record CrmLockedRouteAuthorizationPolicyEvaluationResult(
    string Route,
    string Method,
    bool Locked,
    bool PolicyEvaluated,
    string Decision,
    bool PortalAuthRuntimeConnected,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool AuthorizationHeaderReadAttempted,
    bool PortalHttpCallAttempted,
    bool SideEffectsAllowed,
    bool ProductiveCrudEnabled,
    bool ProductiveDomainExecutionEnabled,
    bool ProductivePersistenceEnabled,
    bool DeleteEndpointsEnabled,
    string NextGate);
