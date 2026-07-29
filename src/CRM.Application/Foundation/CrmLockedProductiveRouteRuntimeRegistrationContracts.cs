namespace CRM.Application.Foundation;

public sealed record CrmLockedProductiveRouteContract(
    string Route,
    IReadOnlyCollection<string> Methods,
    int DefaultStatus,
    int ExplicitlyEnabledStatus,
    bool DeleteEnabled,
    bool DomainExecutionEnabled,
    bool PersistenceEnabled,
    bool PortalAuthRuntimeEnabled);

public sealed record CrmLockedProductiveRouteGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string Reason);

public sealed record CrmLockedProductiveRouteObservationContract(
    string Observation,
    bool Passed,
    string Notes);

public sealed record CrmLockedProductiveRouteBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmLockedProductiveRouteRuntimeRegistrationStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool LockedProductiveRouteRuntimeRegistrationExists,
    bool LockedProductiveRouteRuntimeRegistrationApprovalGranted,
    bool LockedProductiveRouteRuntimeRegistrationEnabled,
    bool ProductiveRoutesRegisteredByDefault,
    bool ProductiveRoutesRegisteredWhenExplicitlyEnabled,
    int DefaultNegativeRouteStatus,
    int ExplicitlyEnabledLockedRouteStatus,
    bool ProductiveCrudEnabled,
    bool ProductiveDomainExecutionEnabled,
    bool ProductivePersistenceEnabled,
    bool DeleteEndpointsEnabled,
    bool PortalAuthRuntimeRequired,
    bool PortalAuthRuntimeEnabled,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool DbRuntimeEnabled,
    bool EfRuntimeEnabled,
    bool MigrationsCreated,
    bool SideEffectsAllowed,
    bool NonProductionOnly,
    bool RollbackRequired,
    bool ObservabilityRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmLockedProductiveRouteContract> Routes,
    IReadOnlyCollection<CrmLockedProductiveRouteGateContract> Gates,
    IReadOnlyCollection<CrmLockedProductiveRouteObservationContract> Observations,
    IReadOnlyCollection<CrmLockedProductiveRouteBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
