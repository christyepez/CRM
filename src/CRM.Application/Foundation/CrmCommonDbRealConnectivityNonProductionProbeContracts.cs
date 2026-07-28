namespace CRM.Application.Foundation;

public sealed record CrmCommonDbRealConnectivityNonProductionProbeGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string Reason);

public sealed record CrmCommonDbRealConnectivityNonProductionProbeObservationContract(
    string Observation,
    bool Passed,
    string Notes);

public sealed record CrmCommonDbRealConnectivityNonProductionProbeDependencyContract(
    string Dependency,
    bool Required,
    bool Available,
    string Status);

public sealed record CrmCommonDbRealConnectivityNonProductionProbeBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmCommonDbRealConnectivityNonProductionProbeStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool CommonDbRealConnectivityNonProductionProbeExists,
    bool CommonDbRealConnectivityApprovalGranted,
    bool SecretProviderRealNonProductionApprovalGranted,
    bool SecretProviderRealRuntimeProbeEnabled,
    bool ConnectionStringResolved,
    bool ConnectionStringValueMaterialized,
    bool ConnectionStringLogged,
    bool ConnectionStringReturnedToApi,
    bool CommonDbProbeEnabled,
    bool CommonDbProbeAttempted,
    bool CommonDbConnected,
    bool SqlConnectionCreated,
    bool DbConnectionCreated,
    bool UseSqlServerEnabled,
    bool EfRuntimeEnabled,
    bool AddDbContextRuntimeEnabled,
    bool MigrationsCreated,
    bool DatabaseSchemaChanged,
    bool ProductivePersistenceEnabled,
    bool ApiRequiresDatabase,
    bool UsesSecretProviderRuntime,
    bool UsesSyntheticFallback,
    string SyntheticConnectionReference,
    bool ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted,
    bool NonProductionOnly,
    bool RollbackRequired,
    bool ObservabilityRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmCommonDbRealConnectivityNonProductionProbeDependencyContract> Dependencies,
    IReadOnlyCollection<CrmCommonDbRealConnectivityNonProductionProbeGateContract> Gates,
    IReadOnlyCollection<CrmCommonDbRealConnectivityNonProductionProbeObservationContract> Observations,
    IReadOnlyCollection<CrmCommonDbRealConnectivityNonProductionProbeBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
