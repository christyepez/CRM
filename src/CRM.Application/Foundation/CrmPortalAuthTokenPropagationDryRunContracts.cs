namespace CRM.Application.Foundation;

public sealed record CrmPortalAuthTokenPropagationDryRunDependencyContract(
    string Dependency,
    bool Required,
    bool Available,
    string Notes);

public sealed record CrmPortalAuthTokenPropagationDryRunSafetyGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string Notes);

public sealed record CrmPortalAuthTokenPropagationDryRunObservabilityContract(
    string Signal,
    bool Required,
    bool RuntimeEmitted,
    string Notes);

public sealed record CrmPortalAuthTokenPropagationDryRunBlockedItemContract(
    string Item,
    string Reason,
    string RequiredGate);

public sealed record CrmPortalAuthTokenPropagationDryRunStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool PortalAuthTokenPropagationDryRunContractExists,
    bool PortalAuthDryRunApprovalGranted,
    bool PortalAuthDryRunEnabled,
    bool PortalAuthRuntimeConnected,
    bool TokenReadAttempted,
    bool HeaderReadAttempted,
    bool PortalHttpAttempted,
    bool UsesSyntheticTokenMetadata,
    string SyntheticTokenReference,
    string SyntheticUserReference,
    bool RealTokenUsed,
    bool RealHeadersRead,
    bool LoginImplementedByCrm,
    bool IdentityImplementedByCrm,
    bool PermissionsPersistedInCrm,
    bool ProductiveAuthorizationEnabled,
    bool NonProductionOnly,
    bool RollbackRequired,
    bool ObservabilityRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmPortalAuthTokenPropagationDryRunDependencyContract> Dependencies,
    IReadOnlyCollection<CrmPortalAuthTokenPropagationDryRunSafetyGateContract> SafetyGates,
    IReadOnlyCollection<CrmPortalAuthTokenPropagationDryRunObservabilityContract> Observability,
    IReadOnlyCollection<CrmPortalAuthTokenPropagationDryRunBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
