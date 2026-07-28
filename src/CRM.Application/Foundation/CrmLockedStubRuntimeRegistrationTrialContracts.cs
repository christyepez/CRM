namespace CRM.Application.Foundation;

public sealed record CrmLockedStubRuntimeRegistrationRouteContract(
    string Route,
    string Methods,
    bool RegisteredByDefault,
    int DefaultNegativeRouteStatus,
    int FutureLockedResponseStatusIfExplicitlyEnabled,
    string Behavior);

public sealed record CrmLockedStubRuntimeRegistrationSafetyGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string Notes);

public sealed record CrmLockedStubRuntimeRegistrationObservabilityContract(
    string Signal,
    bool Required,
    bool RuntimeEmitted,
    string Notes);

public sealed record CrmLockedStubRuntimeRegistrationBlockedItemContract(
    string Item,
    string Reason,
    string RequiredGate);

public sealed record CrmLockedStubRuntimeRegistrationTrialStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool LockedStubRuntimeRegistrationTrialExists,
    bool LockedStubRuntimeRegistrationApprovalGranted,
    bool LockedStubRuntimeRegistrationEnabled,
    bool LockedStubsRegisteredAtRuntime,
    bool ProductiveRoutesRegistered,
    bool ProductiveCrudEnabled,
    bool DeleteEndpointsEnabled,
    int DefaultNegativeRouteStatus,
    int FutureLockedResponseStatusIfExplicitlyEnabled,
    bool RuntimeFlagDefaultEnabled,
    bool UsesDomainServices,
    bool UsesFoundationStores,
    bool UsesDatabase,
    bool UsesPortalAuth,
    bool UsesTokenOrHeaderReads,
    bool NonProductionOnly,
    bool RollbackRequired,
    bool ObservabilityRequired,
    string NextGate,
    string Warning,
    string RuntimeRegistrationDecision,
    IReadOnlyCollection<CrmLockedStubRuntimeRegistrationRouteContract> FutureRoutes,
    IReadOnlyCollection<CrmLockedStubRuntimeRegistrationSafetyGateContract> SafetyGates,
    IReadOnlyCollection<CrmLockedStubRuntimeRegistrationObservabilityContract> Observability,
    IReadOnlyCollection<CrmLockedStubRuntimeRegistrationBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
