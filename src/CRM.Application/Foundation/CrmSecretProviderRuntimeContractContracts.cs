namespace CRM.Application.Foundation;

public sealed record CrmSecretProviderLogicalSecretContract(
    string LogicalName,
    string Purpose,
    bool ValueConfigured,
    bool ValueExposed,
    string Scope);

public sealed record CrmSecretProviderApprovalGateContract(
    string Gate,
    string Owner,
    bool Required,
    bool Approved,
    string RequiredEvidence);

public sealed record CrmSecretProviderNoReadPolicyContract(
    string Rule,
    bool Enforced,
    string Evidence);

public sealed record CrmSecretProviderRuntimeContractStatusResponse(
    string Module,
    string Status,
    bool FoundationMode,
    bool SecretProviderContractExists,
    bool SecretProviderRuntimeConnected,
    bool SecretProviderReadsEnabled,
    bool SecretReadAttemptedByRuntime,
    bool RealSecretsConfigured,
    bool EnvFileRequired,
    bool ConnectionStringsConfigured,
    bool KeyVaultClientConfigured,
    bool SecretValuesExposed,
    bool CommonDbProbeActivationApproved,
    bool PortalAuthProbeActivationApproved,
    bool RuntimeProbeActivationApproved,
    string NextGate,
    string Warning,
    IReadOnlyCollection<CrmSecretProviderLogicalSecretContract> LogicalSecrets,
    IReadOnlyCollection<CrmSecretProviderApprovalGateContract> ApprovalGates,
    IReadOnlyCollection<CrmSecretProviderNoReadPolicyContract> NoReadPolicies,
    IReadOnlyCollection<string> Risks,
    IReadOnlyCollection<string> BlockedItems);
