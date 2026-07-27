namespace CRM.Application.Foundation;

public sealed class CrmSecretProviderRuntimeContractStatusService
{
    public const string WarningText = "Secret Provider contract validation only; no secrets are read";
    public const string NextGate = "Sprint5P3CommonDbProbeOptionalActivationInNonProduction";

    public CrmSecretProviderRuntimeContractStatusResponse GetStatus() =>
        new(
            "CRM",
            "SecretProviderRuntimeContractValidation",
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            NextGate,
            WarningText,
            GetLogicalSecrets(),
            GetApprovalGates(),
            GetNoReadPolicies(),
            [
                "Logical secret names could be confused with configured secret values if approval gates are ignored.",
                "Future probe activation must prove masking, least privilege, rotation and rollback before any read.",
                "Common DB and Portal Auth probes remain blocked until secret provider runtime evidence exists."
            ],
            [
                "Secret provider runtime connection.",
                "Secret read attempts.",
                "Common DB probe activation.",
                "Portal Auth probe activation.",
                "Runtime probe activation."
            ]);

    public IReadOnlyCollection<CrmSecretProviderLogicalSecretContract> GetLogicalSecrets() =>
    [
        new("CRM_COMMON_DB_CONNECTION", "Future common database connection lookup.", false, false, "LogicalNameOnly"),
        new("CRM_PORTAL_AUTH_BASE_URL", "Future Portal Auth base URL lookup.", false, false, "LogicalNameOnly"),
        new("CRM_PORTAL_AUTH_CLIENT_ID", "Future Portal Auth client identifier lookup.", false, false, "LogicalNameOnly"),
        new("CRM_PORTAL_AUTH_CLIENT_SECRET", "Future Portal Auth client secret lookup.", false, false, "LogicalNameOnly"),
        new("CRM_OBSERVABILITY_ENDPOINT", "Future observability endpoint lookup.", false, false, "LogicalNameOnly")
    ];

    public IReadOnlyCollection<CrmSecretProviderApprovalGateContract> GetApprovalGates() =>
    [
        new("Provider approval", "Security", true, false, "Approved provider and least-privilege access model."),
        new("Logical name approval", "Architecture Governance", true, false, "Contract-only names approved without values."),
        new("Masking and logging approval", "Security", true, false, "No secret values in responses, logs or telemetry."),
        new("Rotation policy approval", "DevOps", true, false, "Rotation and rollback procedure documented."),
        new("Synthetic non-production approval", "Data Architect", true, false, "Synthetic data and non-production-only scope approved.")
    ];

    public IReadOnlyCollection<CrmSecretProviderNoReadPolicyContract> GetNoReadPolicies() =>
    [
        new("No environment file dependency", true, "envFileRequired=false"),
        new("No connection strings configured", true, "connectionStringsConfigured=false"),
        new("No Key Vault client configured", true, "keyVaultClientConfigured=false"),
        new("No runtime secret reads", true, "secretReadAttemptedByRuntime=false"),
        new("No secret values exposed", true, "secretValuesExposed=false")
    ];
}
