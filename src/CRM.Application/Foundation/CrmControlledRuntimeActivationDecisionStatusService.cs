namespace CRM.Application.Foundation;

public sealed class CrmControlledRuntimeActivationDecisionStatusService
{
    public const string StatusName = "ControlledRuntimeActivationDecision";
    public const string Decision = "ApprovedForNonProductionTrialsOnly";
    public const string ProductionDecision = "NoGo";
    public const string NextGate = "Sprint9P2SecretProviderRuntimeEnablementTrial";
    public const string WarningText = "Sprint 9 P1 is an approval decision only; no runtime trial is enabled now";

    public CrmControlledRuntimeActivationDecisionStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            ControlledRuntimeActivationDecisionExists: true,
            ControlledRuntimeActivationDecision: Decision,
            ProductionActivationDecision: ProductionDecision,
            SecretProviderRuntimeEnablementTrialApproved: true,
            CommonDbRuntimeConnectivityTrialApproved: true,
            PortalAuthRuntimeValidationTrialApproved: true,
            ProductiveRouteDryRunTrialApproved: true,
            RuntimeTrialsEnabledNow: false,
            ProductionRuntimeEnabledNow: false,
            SecretProviderRuntimeEnabledNow: false,
            CommonDbRuntimeEnabledNow: false,
            PortalAuthRuntimeEnabledNow: false,
            ProductiveRoutesEnabledNow: false,
            ProductiveCrudEnabledNow: false,
            DeleteEnabledNow: false,
            ProductiveUiEnabledNow: false,
            DefaultFailClosedRequired: true,
            ExplicitNonProductionFlagsRequired: true,
            RollbackRequired: true,
            ObservabilityRequired: true,
            SecurityApprovalRequiredForEachTrial: true,
            ArchitectureApprovalRequiredForEachTrial: true,
            DevOpsApprovalRequiredForEachTrial: true,
            QaApprovalRequiredForEachTrial: true,
            NextGate: NextGate,
            Warning: WarningText,
            CapabilityDecisions: GetCapabilityDecisions(),
            Gates: GetGates(),
            Evidence: GetEvidence(),
            BlockedItems: GetBlockedItems(),
            Sprint9Roadmap: GetSprint9Roadmap(),
            Risks:
            [
                "P1 approval must not be interpreted as runtime enablement.",
                "Every trial still requires explicit NonProduction flags, fail-closed defaults, observability and rollback evidence.",
                "Production activation, productive CRUD, DELETE and Productive UI remain blocked."
            ]);

    public IReadOnlyCollection<CrmControlledRuntimeActivationCapabilityDecisionContract> GetCapabilityDecisions() =>
    [
        new("Secret Provider Runtime Enablement Trial", true, false, Decision, "Approved only for Sprint 9 P2 NonProduction trial planning."),
        new("Common DB Runtime Connectivity Trial", true, false, Decision, "Approved only for Sprint 9 P3 NonProduction trial planning."),
        new("Portal Auth Runtime Validation Trial", true, false, Decision, "Approved only for Sprint 9 P4 NonProduction trial planning."),
        new("Productive Route Dry-Run Trial", true, false, Decision, "Approved only for Sprint 9 P5 dry-run under explicit NonProduction flags."),
        new("Production Runtime", false, false, ProductionDecision, "Production activation remains blocked."),
        new("Productive CRUD", false, false, ProductionDecision, "Domain execution remains blocked."),
        new("DELETE", false, false, ProductionDecision, "DELETE endpoints remain prohibited."),
        new("Productive UI", false, false, ProductionDecision, "No productive Angular UI is enabled.")
    ];

    public IReadOnlyCollection<CrmControlledRuntimeActivationGateContract> GetGates() =>
    [
        new("DefaultFailClosedRequired", "All runtime trials must remain disabled by default.", "Required"),
        new("ExplicitNonProductionFlagsRequired", "Each trial needs explicit NonProduction opt-in.", "Required"),
        new("RollbackRequired", "Each trial needs rollback before execution.", "Required"),
        new("ObservabilityRequired", "Each trial needs health/log/evidence capture.", "Required"),
        new("SecurityApprovalRequiredForEachTrial", "Security must approve each trial.", "Required"),
        new("ArchitectureApprovalRequiredForEachTrial", "Architecture must approve each trial.", "Required"),
        new("DevOpsApprovalRequiredForEachTrial", "DevOps must approve each trial.", "Required"),
        new("QaApprovalRequiredForEachTrial", "QA must approve each trial.", "Required")
    ];

    public IReadOnlyCollection<CrmControlledRuntimeActivationEvidenceContract> GetEvidence() =>
    [
        new("Sprint 8 P6", "Main contains Sprint 8 gate decision and next gate is Sprint9P1ControlledRuntimeActivationDecision.", "Passed"),
        new("Runtime", "RuntimeTrialsEnabledNow=false and ProductionRuntimeEnabledNow=false.", "Passed"),
        new("Security", "No secret, token, header, login, Identity or productive Auth activation is introduced.", "Passed"),
        new("Database", "No DB connection, EF runtime, migrations or SQL Server service is introduced.", "Passed"),
        new("API", "Only a read-only foundation endpoint is added.", "Passed"),
        new("Frontend", "Readiness page shows P1 decision only; no productive UI is added.", "Passed")
    ];

    public IReadOnlyCollection<CrmControlledRuntimeActivationBlockedItemContract> GetBlockedItems() =>
    [
        new("Production activation", "P1 approves NonProduction trials only."),
        new("Runtime trial execution", "P2-P5 must implement separate gated trials."),
        new("Productive routes by default", "Default behavior remains 404."),
        new("Productive CRUD", "No domain execution is enabled."),
        new("DELETE endpoints", "DELETE remains prohibited."),
        new("Productive UI", "No productive UI is enabled.")
    ];

    public IReadOnlyCollection<CrmSprint9RoadmapGateContract> GetSprint9Roadmap() =>
    [
        new("Sprint 9 P1", "Controlled Runtime Activation Decision", "CompletedByThisPackage"),
        new("Sprint 9 P2", "Secret Provider Runtime Enablement Trial", "Sprint9P2SecretProviderRuntimeEnablementTrial"),
        new("Sprint 9 P3", "Common DB Runtime Connectivity Trial", "Sprint9P3CommonDbRuntimeConnectivityTrial"),
        new("Sprint 9 P4", "Portal Auth Runtime Validation Trial", "Sprint9P4PortalAuthRuntimeValidationTrial"),
        new("Sprint 9 P5", "Productive Route Dry-Run Trial", "Sprint9P5ProductiveRouteDryRunTrial"),
        new("Sprint 9 P6", "Sprint 9 Closure Gate", "Sprint9P6Sprint9ClosureGate")
    ];
}
