namespace CRM.Application.Foundation;

public sealed record CrmLockedRouteAuthorizationPolicyEvaluationRequest(
    string Route,
    string Method,
    bool LockedRegistrationEnabled,
    bool LockedAuthorizationPolicyEnabled,
    bool NonProduction);
