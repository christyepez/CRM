namespace CRM.Application.Foundation;

public sealed record CrmSecretProviderSafeMockLogicalSecretContract(
    string LogicalName,
    string Purpose,
    bool SyntheticOnly,
    bool RuntimeUsable);

public sealed record CrmSecretProviderSafeMockValueContract(
    string LogicalName,
    string SyntheticValue,
    bool Synthetic,
    bool Sensitive,
    bool RuntimeUsable);

public sealed record CrmSecretProviderSafeMockSafetyGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string Notes);

public sealed record CrmSecretProviderSafeMockBlockedItemContract(
    string Item,
    string Reason,
    string RequiredGate);

public sealed record CrmSecretProviderSafeMockActivationStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool SecretProviderSafeMockExists,
    bool SecretProviderSafeMockEnabled,
    bool SecretProviderRuntimeConnected,
    bool SecretProviderReadsRealSecrets,
    bool SecretProviderReadsSyntheticValues,
    bool SecretProviderReadsEnabledForMockOnly,
    bool RealSecretsConfigured,
    bool EnvFileRequired,
    bool KeyVaultClientConfigured,
    bool AzureSdkForSecretsConfigured,
    bool SecretValuesExposedInLogs,
    bool CommonDbDryRunApprovalGranted,
    bool PortalAuthDryRunApprovalGranted,
    bool RealActivationApprovalGranted,
    bool NonProductionOnly,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSecretProviderSafeMockLogicalSecretContract> LogicalSecrets,
    IReadOnlyCollection<CrmSecretProviderSafeMockValueContract> SyntheticValues,
    IReadOnlyCollection<CrmSecretProviderSafeMockSafetyGateContract> SafetyGates,
    IReadOnlyCollection<CrmSecretProviderSafeMockBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
