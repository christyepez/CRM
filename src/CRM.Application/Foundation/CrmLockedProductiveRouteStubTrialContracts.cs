namespace CRM.Application.Foundation;

public sealed record CrmLockedProductiveRouteStubContract(
    string Route,
    string Method,
    bool RegisteredByDefault,
    int LockedResponseIfEnabled,
    string Behavior);

public sealed record CrmLockedProductiveRouteStubTrialGateContract(
    string Gate,
    string Owner,
    bool Required,
    bool Approved,
    string RequiredEvidence);

public sealed record CrmLockedProductiveRouteStubTrialDecisionContract(
    string Decision,
    string Value,
    string Reason);

public sealed record CrmLockedProductiveRouteStubBlockedItemContract(
    string Item,
    string Reason);

public sealed record CrmLockedProductiveRouteStubTrialStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool LockedProductiveRouteStubTrialExists,
    bool LockedProductiveRouteStubRegistrationApproved,
    bool LockedProductiveRouteStubsRegistered,
    bool ProductiveRoutesRegistered,
    bool ProductiveCrudEnabled,
    bool ProductiveAuthorizationEnabled,
    bool DeleteEndpointsEnabled,
    bool RuntimeFlagDefaultEnabled,
    int LockedResponseIfEnabled,
    int DefaultNegativeRouteStatus,
    bool FoundationCrudStillSeparate,
    bool DbRequired,
    bool AuthRuntimeRequired,
    bool PortalRuntimeRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmLockedProductiveRouteStubContract> FutureRoutes,
    IReadOnlyCollection<CrmLockedProductiveRouteStubTrialGateContract> Gates,
    IReadOnlyCollection<CrmLockedProductiveRouteStubTrialDecisionContract> Decisions,
    IReadOnlyCollection<CrmLockedProductiveRouteStubBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
