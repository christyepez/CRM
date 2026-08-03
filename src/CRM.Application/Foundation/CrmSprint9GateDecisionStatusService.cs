namespace CRM.Application.Foundation;

public sealed class CrmSprint9GateDecisionStatusService
{
    public const string StatusName = "Sprint9GateDecision";
    public const string WarningText = "Sprint 9 gate decision only; production activation remains NoGo";
    public const string NextGate = "Sprint10P1ProductizationReadinessDecision";

    public CrmSprint9GateDecisionStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            Sprint9GateDecisionExists: true,
            Sprint9GateDecisionApproved: true,
            Sprint9Closed: true,
            Sprint9EvidenceComplete: true,
            Sprint9P1Complete: true,
            Sprint9P2Complete: true,
            Sprint9P3Complete: true,
            Sprint9P4Complete: true,
            Sprint9P5Complete: true,
            OverallSprint9Decision: "GoForSprint10ControlledProductizationReadinessPlanning",
            ProductionActivationDecision: "NoGo",
            SecretProviderRuntimeTrialDecision: "GoOnlyAsExplicitNonProductionTrial",
            CommonDbRuntimeConnectivityTrialDecision: "GoOnlyAsExplicitNonProductionTrial",
            PortalAuthRuntimeValidationTrialDecision: "GoOnlyAsExplicitNonProductionTrial",
            ProductiveRouteDryRunTrialDecision: "GoOnlyAsExplicitNonProductionDryRun",
            ProductiveRouteRegistrationDecision: "NoGoByDefault",
            ProductiveCrudDecision: "NoGo",
            DeleteDecision: "NoGo",
            DbRuntimeDecision: "NoGoForProduction",
            PortalAuthEnforcementDecision: "NoGoForProduction",
            ProductionActivationApproved: false,
            RuntimeActivationApprovedForProduction: false,
            ProductiveRoutesApprovedByDefault: false,
            ProductiveCrudApproved: false,
            DeleteApproved: false,
            DatabaseWritesApproved: false,
            EfRuntimeApproved: false,
            MigrationsApproved: false,
            SchemaChangesApproved: false,
            PortalAuthEnforcementApproved: false,
            TokenHeaderReadsApproved: false,
            LoginLogoutApproved: false,
            IdentityRuntimeApproved: false,
            ProductiveUiApproved: false,
            NonProductionTrialsRemainAllowedOnlyWithExplicitFlags: true,
            AllTrialsFailClosedByDefault: true,
            AllObservabilityMetadataOnly: true,
            RollbackAvailable: true,
            ProductizationStatus: "NotReady",
            NextGate: NextGate,
            Warning: WarningText,
            Decisions: GetDecisions(),
            Evidence: GetEvidence(),
            Sprint10Roadmap: GetSprint10Roadmap(),
            Risks:
            [
                "Sprint 10 planning must not be interpreted as approval for production runtime activation.",
                "P2/P3/P4/P5 trials remain explicit NonProduction-only and fail-closed by default.",
                "Productive routes, CRUD, DELETE, DB runtime and Portal Auth enforcement remain blocked until a future gate."
            ],
            BlockedItems:
            [
                "Production activation",
                "Default productive route registration",
                "Productive CRUD and domain execution",
                "DELETE endpoints",
                "DB writes, EF runtime, migrations and schema changes",
                "Portal Auth enforcement, token/header reads, login/logout and CRM Identity",
                "Productive UI"
            ]);

    public IReadOnlyCollection<CrmSprint9GateDecisionContract> GetDecisions() =>
    [
        new("Overall Sprint 9", "GoForSprint10ControlledProductizationReadinessPlanning", "P1-P5 evidence is complete and all trials remain fail-closed."),
        new("Production Activation", "NoGo", "No production runtime path is approved."),
        new("Secret Provider Runtime Trial", "GoOnlyAsExplicitNonProductionTrial", "P2 remains disabled by default and metadata-only."),
        new("Common DB Runtime Connectivity Trial", "GoOnlyAsExplicitNonProductionTrial", "P3 remains disabled by default and does not expose connection strings."),
        new("Portal Auth Runtime Validation Trial", "GoOnlyAsExplicitNonProductionTrial", "P4 remains disabled by default and does not read tokens or headers."),
        new("Productive Route Dry Run Trial", "GoOnlyAsExplicitNonProductionDryRun", "P5 returns 423 by default and keeps productive routes 404 by default."),
        new("Productive Route Registration", "NoGoByDefault", "No productive CRM routes are registered by default."),
        new("Productive CRUD", "NoGo", "No productive domain execution or persistence is approved."),
        new("DELETE", "NoGo", "DELETE remains prohibited."),
        new("DB Runtime", "NoGoForProduction", "No DB writes, EF runtime, migrations or schema changes are approved."),
        new("Portal Auth Enforcement", "NoGoForProduction", "No production Auth middleware, token/header reads or Portal HTTP calls are approved.")
    ];

    public IReadOnlyCollection<CrmSprint9EvidenceContract> GetEvidence() =>
    [
        new("Sprint 9 P1", "Controlled runtime activation decision recorded NonProduction trials only and production NoGo.", "Passed"),
        new("Sprint 9 P2", "Secret Provider runtime trial exists, is disabled/fail-closed by default and returns only sanitized metadata.", "Passed"),
        new("Sprint 9 P3", "Common DB runtime connectivity trial exists, is disabled/fail-closed by default and does not expose connection strings.", "Passed"),
        new("Sprint 9 P4", "Portal Auth runtime validation trial exists, is disabled/fail-closed by default and does not read headers or tokens.", "Passed"),
        new("Sprint 9 P5", "Productive route dry-run trial exists, returns 423 by default and keeps productive routes 404 by default.", "Passed"),
        new("Build", "dotnet build validates the solution.", "Required"),
        new("Tests", "Unit and architecture tests validate Sprint 9 P6 gate boundaries.", "Required"),
        new("Docker", "docker compose uses crm-api only and no CRM-owned SQL Server.", "Required")
    ];

    public IReadOnlyCollection<CrmSprint10GateContract> GetSprint10Roadmap() =>
    [
        new("Sprint 10 P1", "Productization readiness decision", NextGate),
        new("Sprint 10 P2", "Explicit NonProduction runtime activation criteria", "Sprint10P2ExplicitNonProductionRuntimeActivationCriteria"),
        new("Sprint 10 P3", "Production no-go reassessment after evidence", "Sprint10P3ProductionNoGoReassessment")
    ];
}
