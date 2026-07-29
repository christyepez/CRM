namespace CRM.Application.Foundation;

public sealed record CrmLockedRouteAuthorizationPolicyDecisionContract(
    string Decision,
    bool PolicyEvaluated,
    bool Locked,
    bool PortalAuthRuntimeConnected,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool SideEffectsAllowed,
    bool ProductiveCrudEnabled);

public sealed record CrmLockedRouteAuthorizationPolicyRouteContract(
    string Route,
    IReadOnlyCollection<string> LockedMethods,
    bool DeleteEnabled,
    int DefaultStatus,
    int LockedStatus);

public sealed record CrmLockedRouteAuthorizationPolicyGateContract(
    string Gate,
    bool Required,
    bool Passed,
    string Reason);

public sealed record CrmLockedRouteAuthorizationPolicyObservationContract(
    string Area,
    bool Value,
    string Evidence);

public sealed record CrmLockedRouteAuthorizationPolicyBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmLockedRouteAuthorizationPolicyIntegrationStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool LockedRouteAuthorizationPolicyIntegrationExists,
    bool LockedRouteAuthorizationPolicyIntegrationApproved,
    bool LockedRouteAuthorizationPolicyIntegrationEnabled,
    bool AuthorizationPolicyEvaluated,
    string AuthorizationPolicyDecision,
    bool PortalAuthMetadataUsed,
    bool PortalAuthRuntimeRequired,
    bool PortalAuthRuntimeConnected,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool AuthorizationHeaderReadAttempted,
    bool PortalHttpCallAttempted,
    bool ProductiveRoutesRegisteredByDefault,
    int DefaultNegativeRouteStatus,
    bool LockedRoutesEnabledOnlyWithExplicitNonProductionFlag,
    int LockedRouteStatus,
    bool LockedRouteAuthorizationDecisionReturned,
    bool ProductiveCrudEnabled,
    bool ProductiveDomainExecutionEnabled,
    bool ProductivePersistenceEnabled,
    bool DeleteEndpointsEnabled,
    bool SideEffectsAllowed,
    bool DbRuntimeEnabled,
    bool EfRuntimeEnabled,
    bool NonProductionOnly,
    bool FailClosedByDefault,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmLockedRouteAuthorizationPolicyRouteContract> Routes,
    IReadOnlyCollection<CrmLockedRouteAuthorizationPolicyGateContract> Gates,
    IReadOnlyCollection<CrmLockedRouteAuthorizationPolicyObservationContract> Observations,
    IReadOnlyCollection<CrmLockedRouteAuthorizationPolicyBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
