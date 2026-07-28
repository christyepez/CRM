namespace CRM.Application.Foundation;

public sealed record CrmSecretProviderRealNonProductionSecretNameContract(
    string LogicalName,
    string Purpose,
    bool ValueIncluded,
    bool ApprovedForRuntimeRead);

public sealed record CrmSecretProviderRealNonProductionApprovalGateContract(
    string Gate,
    bool Required,
    bool Granted,
    string EvidenceRequired);

public sealed record CrmSecretProviderRealNonProductionEvidenceContract(
    string Evidence,
    bool RequiredForRuntimeProbe,
    bool EvidenceAvailable,
    string Notes);

public sealed record CrmSecretProviderRealNonProductionBlockedItemContract(
    string Item,
    string Reason,
    string RequiredGate);

public sealed record CrmSecretProviderRealNonProductionApprovalStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool SecretProviderRealNonProductionApprovalPackageExists,
    bool SecretProviderRealNonProductionApprovalGranted,
    bool SecretProviderRealRuntimeEnabled,
    bool SecretProviderRealRuntimeConnected,
    bool RealSecretReadAttempted,
    bool KeyVaultRuntimeClientEnabled,
    bool AzureSecretSdkRuntimeEnabled,
    bool EnvFileRequired,
    bool EnvSecretReadAllowed,
    bool SecretsLogged,
    bool SecretNamesApproved,
    bool SecretValuesApproved,
    bool NonProductionOnly,
    bool SecurityReviewRequired,
    bool ArchitectureReviewRequired,
    bool DevOpsReviewRequired,
    bool RollbackRequired,
    bool ObservabilityRequired,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSecretProviderRealNonProductionSecretNameContract> LogicalSecretNames,
    IReadOnlyCollection<CrmSecretProviderRealNonProductionApprovalGateContract> ApprovalGates,
    IReadOnlyCollection<CrmSecretProviderRealNonProductionEvidenceContract> EvidenceRequired,
    IReadOnlyCollection<CrmSecretProviderRealNonProductionBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
