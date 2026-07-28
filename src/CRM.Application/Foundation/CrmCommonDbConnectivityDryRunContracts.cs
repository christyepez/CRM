namespace CRM.Application.Foundation;

public sealed record CrmCommonDbConnectivityDryRunDependencyContract(
    string Dependency,
    bool Required,
    bool Available,
    string Notes);

public sealed record CrmCommonDbConnectivityDryRunSafetyGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string Notes);

public sealed record CrmCommonDbConnectivityDryRunObservabilityContract(
    string Signal,
    bool Required,
    bool RuntimeEmitted,
    string Notes);

public sealed record CrmCommonDbConnectivityDryRunBlockedItemContract(
    string Item,
    string Reason,
    string RequiredGate);

public sealed record CrmCommonDbConnectivityDryRunStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool CommonDbConnectivityDryRunContractExists,
    bool CommonDbDryRunApprovalGranted,
    bool CommonDbDryRunEnabled,
    bool CommonDbConnectionAttempted,
    bool UsesSecretProviderSafeMockMetadata,
    bool UsesSyntheticConnectionReference,
    string SyntheticConnectionReference,
    bool RealConnectionStringUsed,
    bool ConnectionStringResolved,
    bool SqlConnectionCreated,
    bool DbConnectionCreated,
    bool EfRuntimeEnabled,
    bool MigrationsCreated,
    bool ApiRequiresDatabase,
    bool NonProductionOnly,
    bool RollbackRequired,
    bool ObservabilityRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmCommonDbConnectivityDryRunDependencyContract> Dependencies,
    IReadOnlyCollection<CrmCommonDbConnectivityDryRunSafetyGateContract> SafetyGates,
    IReadOnlyCollection<CrmCommonDbConnectivityDryRunObservabilityContract> Observability,
    IReadOnlyCollection<CrmCommonDbConnectivityDryRunBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
