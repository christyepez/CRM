namespace CRM.Application.Foundation;

public sealed record CrmCommonDbControlledRealConnectivityProbeContract(
    string SecretName,
    bool ProbeAttempted,
    bool ProviderConfigured,
    bool ConnectionAttempted,
    bool Connected,
    bool TimeoutApplied,
    int TimeoutSeconds,
    bool ConnectionStringReturned,
    bool ConnectionStringLogged);

public sealed record CrmCommonDbControlledRealConnectivityGateContract(
    string Gate,
    bool Required,
    bool Passed,
    string Reason);

public sealed record CrmCommonDbControlledRealConnectivityObservationContract(
    string Area,
    bool Value,
    string Evidence);

public sealed record CrmCommonDbControlledRealConnectivityBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmCommonDbControlledRealConnectivityProbeRequest(string SecretName);

public sealed record CrmCommonDbControlledRealConnectivityStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool CommonDbControlledRealConnectivityExists,
    bool CommonDbControlledRealConnectivityApproved,
    bool CommonDbControlledRealConnectivityEnabled,
    bool CommonDbConnectivityAttempted,
    bool CommonDbConnected,
    bool SecretProviderAvailabilityMetadataUsed,
    bool SecretValueReturnedToApi,
    bool ConnectionStringResolved,
    bool ConnectionStringMaterializedInPublicContract,
    bool ConnectionStringLogged,
    bool ConnectionStringReturnedToApi,
    bool SqlConnectionCreated,
    bool DbConnectionCreated,
    bool DbConnectionOpened,
    bool EfRuntimeEnabled,
    bool AddDbContextRuntimeEnabled,
    bool UseSqlServerEnabled,
    bool MigrationsCreated,
    bool DatabaseSchemaChanged,
    bool ProductivePersistenceEnabled,
    bool ProductiveCrudEnabled,
    bool ApiRequiresDatabase,
    bool NonProductionOnly,
    bool FailClosedByDefault,
    string NextGate,
    string Warning,
    CrmCommonDbControlledRealConnectivityProbeContract Probe,
    IReadOnlyCollection<CrmCommonDbControlledRealConnectivityGateContract> Gates,
    IReadOnlyCollection<CrmCommonDbControlledRealConnectivityObservationContract> Observations,
    IReadOnlyCollection<CrmCommonDbControlledRealConnectivityBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
