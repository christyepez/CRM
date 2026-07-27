namespace CRM.Application.Foundation;

public sealed record CrmProductiveRouteStubContract(
    string Method,
    string Route,
    string Status,
    bool Registered,
    bool ExecutesBusinessLogic);

public sealed record CrmProductiveRouteStubSafetyGateContract(
    string Gate,
    string Decision,
    bool Approved,
    string RequiredBeforeEnablement);

public sealed record CrmProductiveRouteStubDecisionContract(
    string Decision,
    string Strategy,
    string Reason);

public sealed record CrmProductiveRoutesLockedStubStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    string LockedStubsStrategy,
    bool ProductiveRoutesRegistered,
    bool LockedStubsRegistered,
    bool ProductiveCrudEnabled,
    bool ProductiveAuthorizationEnabled,
    bool DeleteEndpointsEnabled,
    bool DbRequired,
    bool AuthRuntimeRequired,
    bool FoundationCrudStillSeparate,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmProductiveRouteStubContract> FutureRoutes,
    IReadOnlyCollection<CrmProductiveRouteStubSafetyGateContract> SafetyGates,
    CrmProductiveRouteStubDecisionContract Decision,
    IReadOnlyCollection<string> Risks);
