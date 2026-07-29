namespace CRM.Application.Foundation;

public sealed class CrmSecretProviderApprovalDecisionStatusService
{
    public const string StatusName = "SecretProviderApprovalDecision";
    public const string ApprovalDecision = "ApprovedForControlledNonProductionReadPlanning";
    public const string WarningText = "Secret Provider approval decision only; no real secret read in Sprint 8 P1";
    public const string NextGate = "Sprint8P2SecretProviderControlledRealNonProductionRead";

    public CrmSecretProviderApprovalDecisionStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            SecretProviderApprovalDecisionExists: true,
            SecretProviderApprovalDecision: ApprovalDecision,
            SecretProviderRealReadApprovedForNextSprint: true,
            SecretProviderRealReadEnabledNow: false,
            RealSecretReadAttempted: false,
            RealSecretValueMaterialized: false,
            RealSecretValueLogged: false,
            SecretValueReturnedToApi: false,
            KeyVaultRuntimeClientCreated: false,
            KeyVaultRuntimeCallAttempted: false,
            AzureSecretSdkRuntimeEnabled: false,
            EnvFileRequired: false,
            EnvSecretReadAllowed: false,
            ApprovedSecretNamesOnly: true,
            ApprovedSecretValues: false,
            ApprovedForNonProductionOnly: true,
            SecurityApprovalRecorded: true,
            ArchitectureApprovalRecorded: true,
            DevOpsApprovalRecorded: true,
            RollbackPlanApproved: true,
            ObservabilityPlanApproved: true,
            RedactionPlanApproved: true,
            NextGate: NextGate,
            Warning: WarningText,
            ApprovedSecrets: GetApprovedSecrets(),
            Gates: GetGates(),
            Evidence: GetEvidence(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "P1 approval is planning-only and must not be interpreted as real secret read execution.",
                "P2 must preserve NonProduction-only execution, redaction, rollback and no value exposure.",
                "Approved logical names must not introduce secret values into source, logs, API responses or persisted state."
            ]);

    public IReadOnlyCollection<CrmSecretProviderApprovalDecisionApprovedSecretContract> GetApprovedSecrets() =>
    [
        new("crm-common-db-connection", "Common DB controlled NonProduction connectivity.", false, false),
        new("crm-portal-auth-base-url", "Portal Auth controlled NonProduction endpoint reference.", false, false),
        new("crm-portal-auth-client-id", "Portal Auth controlled NonProduction client identifier.", false, false),
        new("crm-portal-auth-client-secret", "Portal Auth controlled NonProduction client credential reference.", false, false),
        new("crm-observability-endpoint", "Redacted observability endpoint reference.", false, false)
    ];

    public IReadOnlyCollection<CrmSecretProviderApprovalDecisionGateContract> GetGates() =>
    [
        new("Security approval", true, true, "Approval recorded for planning controlled NonProduction read in P2."),
        new("Architecture approval", true, true, "Approval recorded with Portal and shared capability ownership boundaries preserved."),
        new("DevOps approval", true, true, "Approval recorded for external provider configuration outside source control."),
        new("Rollback approval", true, true, "Rollback plan approved before P2 implementation."),
        new("Observability and redaction approval", true, true, "No values may be logged, persisted or returned.")
    ];

    public IReadOnlyCollection<CrmSecretProviderApprovalDecisionEvidenceContract> GetEvidence() =>
    [
        new("Sprint 7 gate", "Sprint 7 closed with Sprint 8 planning Go and real activation NoGo.", "Accepted"),
        new("Approved logical names", "Only approved logical secret names are in scope.", "Accepted"),
        new("No P1 runtime read", "RealSecretReadAttempted=false and SecretProviderRealReadEnabledNow=false.", "Accepted"),
        new("No value exposure", "Value materialization, logging and API return remain false.", "Accepted"),
        new("NonProduction only", "P2 scope is controlled NonProduction read only.", "Accepted")
    ];

    public IReadOnlyCollection<CrmSecretProviderApprovalDecisionBlockedItemContract> GetBlockedItems() =>
    [
        new("Real secret reads in P1", "P1 is approval decision only.", NextGate),
        new("Secret values in API/logs/repo", "Only logical names are approved.", NextGate),
        new("Production activation", "Productive activation remains NoGo.", NextGate),
        new("DB, Portal Auth or productive route activation", "Separate Sprint 8 gates are required.", NextGate)
    ];
}
