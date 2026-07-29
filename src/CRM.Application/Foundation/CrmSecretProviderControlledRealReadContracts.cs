namespace CRM.Application.Foundation;

public sealed record CrmSecretProviderControlledRealReadSecretContract(
    string SecretName,
    bool Approved,
    bool ValueApproved,
    bool ValueReturnedToApi);

public sealed record CrmSecretProviderControlledRealReadGateContract(
    string Gate,
    bool Required,
    bool Passed,
    string Reason);

public sealed record CrmSecretProviderControlledRealReadObservationContract(
    string Area,
    bool Value,
    string Evidence);

public sealed record CrmSecretProviderControlledRealReadBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmSecretProviderControlledRealReadProbeRequest(string SecretName);

public sealed record CrmSecretProviderControlledRealReadStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool SecretProviderControlledRealNonProductionReadExists,
    bool SecretProviderControlledRealNonProductionReadApproved,
    bool SecretProviderControlledRealNonProductionReadEnabled,
    bool SecretProviderControlledRealNonProductionReadAttempted,
    bool RealSecretReadAttempted,
    bool RealSecretValueMaterialized,
    bool RealSecretValueLogged,
    bool SecretValueReturnedToApi,
    bool SecretValuePersisted,
    bool SecretValueCached,
    bool KeyVaultRuntimeClientCreated,
    bool KeyVaultRuntimeCallAttempted,
    bool AzureSecretSdkRuntimeEnabled,
    bool UsesApprovedSecretNamesOnly,
    bool NonProductionOnly,
    bool FailClosedByDefault,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSecretProviderControlledRealReadSecretContract> Secrets,
    IReadOnlyCollection<CrmSecretProviderControlledRealReadGateContract> Gates,
    IReadOnlyCollection<CrmSecretProviderControlledRealReadObservationContract> Observations,
    IReadOnlyCollection<CrmSecretProviderControlledRealReadBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
