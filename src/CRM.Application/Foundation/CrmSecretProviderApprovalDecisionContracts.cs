namespace CRM.Application.Foundation;

public sealed record CrmSecretProviderApprovalDecisionGateContract(
    string Gate,
    bool Required,
    bool Approved,
    string Reason);

public sealed record CrmSecretProviderApprovalDecisionEvidenceContract(
    string Area,
    string Evidence,
    string Status);

public sealed record CrmSecretProviderApprovalDecisionApprovedSecretContract(
    string LogicalName,
    string Purpose,
    bool ValueApproved,
    bool ValueReturnedToApi);

public sealed record CrmSecretProviderApprovalDecisionBlockedItemContract(
    string Item,
    string Reason,
    string NextGate);

public sealed record CrmSecretProviderApprovalDecisionStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool SecretProviderApprovalDecisionExists,
    string SecretProviderApprovalDecision,
    bool SecretProviderRealReadApprovedForNextSprint,
    bool SecretProviderRealReadEnabledNow,
    bool RealSecretReadAttempted,
    bool RealSecretValueMaterialized,
    bool RealSecretValueLogged,
    bool SecretValueReturnedToApi,
    bool KeyVaultRuntimeClientCreated,
    bool KeyVaultRuntimeCallAttempted,
    bool AzureSecretSdkRuntimeEnabled,
    bool EnvFileRequired,
    bool EnvSecretReadAllowed,
    bool ApprovedSecretNamesOnly,
    bool ApprovedSecretValues,
    bool ApprovedForNonProductionOnly,
    bool SecurityApprovalRecorded,
    bool ArchitectureApprovalRecorded,
    bool DevOpsApprovalRecorded,
    bool RollbackPlanApproved,
    bool ObservabilityPlanApproved,
    bool RedactionPlanApproved,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSecretProviderApprovalDecisionApprovedSecretContract> ApprovedSecrets,
    IReadOnlyCollection<CrmSecretProviderApprovalDecisionGateContract> Gates,
    IReadOnlyCollection<CrmSecretProviderApprovalDecisionEvidenceContract> Evidence,
    IReadOnlyCollection<CrmSecretProviderApprovalDecisionBlockedItemContract> BlockedItems,
    IReadOnlyCollection<string> Risks);
