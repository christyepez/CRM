namespace CRM.Application.Foundation;

public sealed record CrmCommonDbRuntimeConnectivityTrialProbeContract(string SecretName);

public sealed record CrmCommonDbRuntimeConnectivityTrialGateContract(
    string Gate,
    bool Required,
    bool Passed,
    string Reason);

public sealed record CrmCommonDbRuntimeConnectivityTrialObservationContract(
    string Area,
    bool Value,
    string Evidence);

public sealed record CrmCommonDbRuntimeConnectivityTrialBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmCommonDbRuntimeConnectivityTrialStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool CommonDbRuntimeConnectivityTrialExists,
    bool CommonDbRuntimeConnectivityTrialApproved,
    bool CommonDbRuntimeConnectivityTrialEnabled,
    bool CommonDbConnectionAttempted,
    bool CommonDbConnected,
    bool CommonDbConnectionStringResolved,
    bool CommonDbConnectionStringReturnedToApi,
    bool CommonDbConnectionStringLogged,
    bool CommonDbConnectionStringPersisted,
    bool CommonDbConnectionStringCached,
    bool SecretProviderMetadataDependencyValidated,
    bool SchemaCreated,
    bool MigrationExecuted,
    bool EfRuntimeEnabled,
    bool ProductivePersistenceEnabled,
    bool NonProductionOnly,
    bool ProductionBlocked,
    bool FailClosedByDefault,
    bool RollbackAvailable,
    bool ObservabilityMetadataOnly,
    string NextGate,
    string Warning,
    string ApprovedSecretName,
    IReadOnlyCollection<CrmCommonDbRuntimeConnectivityTrialGateContract> Gates,
    IReadOnlyCollection<CrmCommonDbRuntimeConnectivityTrialObservationContract> Observations,
    IReadOnlyCollection<CrmCommonDbRuntimeConnectivityTrialBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
