namespace CRM.Application.Foundation;

public sealed record CrmSecretProviderRealNonProductionRuntimeProbeSecretContract(
    string LogicalName,
    string Purpose,
    bool LogicalNameAllowed,
    bool ValueRead,
    bool ValueReturned);

public sealed record CrmSecretProviderRealNonProductionRuntimeProbeGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string Reason);

public sealed record CrmSecretProviderRealNonProductionRuntimeProbeObservationContract(
    string Observation,
    bool Passed,
    string Notes);

public sealed record CrmSecretProviderRealNonProductionRuntimeProbeBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmSecretProviderRealNonProductionRuntimeProbeStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool SecretProviderRealNonProductionRuntimeProbeExists,
    bool SecretProviderRealNonProductionApprovalGranted,
    bool SecretProviderRealRuntimeProbeEnabled,
    bool SecretProviderRealRuntimeProbeAttempted,
    bool SecretProviderRealRuntimeConnected,
    bool RealSecretReadAttempted,
    bool RealSecretValueMaterialized,
    bool RealSecretValueLogged,
    bool SecretValueReturnedToApi,
    bool KeyVaultRuntimeClientCreated,
    bool KeyVaultRuntimeCallAttempted,
    bool AzureSecretSdkRuntimeEnabled,
    bool EnvSecretReadAttempted,
    bool EnvFileRequired,
    bool LogicalSecretNamesValidated,
    bool SecretValuesValidated,
    bool ProbeSkippedBecauseApprovalNotGranted,
    bool NonProductionOnly,
    bool RollbackRequired,
    bool ObservabilityRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSecretProviderRealNonProductionRuntimeProbeSecretContract> LogicalSecretNames,
    IReadOnlyCollection<CrmSecretProviderRealNonProductionRuntimeProbeGateContract> Gates,
    IReadOnlyCollection<CrmSecretProviderRealNonProductionRuntimeProbeObservationContract> Observations,
    IReadOnlyCollection<CrmSecretProviderRealNonProductionRuntimeProbeBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
