namespace CRM.Application.Foundation;

public sealed record CrmSecretProviderRuntimeEnablementTrialProbeContract(string SecretName);

public sealed record CrmSecretProviderRuntimeEnablementTrialGateContract(
    string Gate,
    bool Required,
    bool Passed,
    string Reason);

public sealed record CrmSecretProviderRuntimeEnablementTrialObservationContract(
    string Area,
    bool Value,
    string Evidence);

public sealed record CrmSecretProviderRuntimeEnablementTrialBlockedItemContract(
    string Item,
    string Reason);

public sealed record CrmSecretProviderRuntimeEnablementTrialStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool SecretProviderRuntimeEnablementTrialExists,
    bool SecretProviderRuntimeEnablementTrialApproved,
    bool SecretProviderRuntimeEnablementTrialEnabled,
    bool SecretProviderRuntimeTrialAttempted,
    bool SecretProviderRuntimeConnected,
    bool RealSecretReadAttempted,
    bool RealSecretValueMaterialized,
    bool RealSecretValueLogged,
    bool SecretValueReturnedToApi,
    bool SecretValuePersisted,
    bool SecretValueCached,
    bool AllowedLogicalSecretNamesEnforced,
    bool NonProductionOnly,
    bool ProductionBlocked,
    bool FailClosedByDefault,
    bool RollbackAvailable,
    bool ObservabilityMetadataOnly,
    string NextGate,
    string Warning,
    IReadOnlyCollection<string> AllowedLogicalSecretNames,
    IReadOnlyCollection<CrmSecretProviderRuntimeEnablementTrialGateContract> Gates,
    IReadOnlyCollection<CrmSecretProviderRuntimeEnablementTrialObservationContract> Observations,
    IReadOnlyCollection<CrmSecretProviderRuntimeEnablementTrialBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
