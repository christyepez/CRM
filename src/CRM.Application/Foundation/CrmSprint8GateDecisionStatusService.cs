namespace CRM.Application.Foundation;

public sealed class CrmSprint8GateDecisionStatusService
{
    public const string StatusName = "Sprint8GateDecision";
    public const string WarningText = "Sprint 8 gate decision only; no production activation";
    public const string NextGate = "Sprint9P1ControlledRuntimeActivationDecision";

    public CrmSprint8GateDecisionStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            OverallDecision: "GoForSprint9ControlledRuntimeActivationPlanning",
            RealProductionActivationDecision: "NoGo",
            SecretProviderControlledReadDecision: "GoOnlyAsExplicitNonProductionFlag",
            CommonDbControlledConnectivityDecision: "GoOnlyAsExplicitNonProductionFlag",
            PortalAuthControlledValidationDecision: "GoOnlyAsExplicitNonProductionFlag",
            LockedRouteAuthorizationPolicyDecision: "GoOnlyAsExplicitNonProductionLocked423",
            ProductiveRoutesDefaultDecision: "NoGo",
            ProductiveCrudDecision: "NoGo",
            DeleteDecision: "NoGo",
            ProductiveUiDecision: "NoGo",
            ProductizationStatus: "NotReady",
            Sprint9PlanningDecision: "Go",
            NextGate: NextGate,
            Warning: WarningText,
            CapabilityDecisions: GetCapabilityDecisions(),
            Evidence: GetEvidence(),
            Sprint9Roadmap: GetSprint9Roadmap(),
            Risks:
            [
                "Sprint 9 planning must not be interpreted as production activation approval.",
                "Secret Provider, Common DB, Portal Auth and locked route gates still require explicit NonProduction flags, observability and rollback evidence.",
                "Productive CRUD, DELETE and Productive UI remain blocked until a future productization gate."
            ],
            BlockedItems:
            [
                "Production activation",
                "Default productive route registration",
                "Productive CRUD and domain execution",
                "DELETE endpoints",
                "Real DB/EF runtime and migrations by default",
                "Portal Auth runtime, token/header reads and Portal HTTP by default",
                "Productive UI"
            ]);

    public IReadOnlyCollection<CrmSprint8CapabilityDecisionContract> GetCapabilityDecisions() =>
    [
        new("Secret Provider Controlled Read", "GoOnlyAsExplicitNonProductionFlag", "P2 abstraction is fail-closed and probe is 423 by default."),
        new("Common DB Controlled Connectivity", "GoOnlyAsExplicitNonProductionFlag", "P3 abstraction is fail-closed and no connection string is exposed by default."),
        new("Portal Auth Controlled Validation", "GoOnlyAsExplicitNonProductionFlag", "P4 validation is fail-closed with no Portal HTTP or token/header reads by default."),
        new("Locked Route Authorization Policy", "GoOnlyAsExplicitNonProductionLocked423", "P5 policy metadata is sanitized and locked routes remain 423 only under explicit NonProduction flags."),
        new("Productive Routes Default", "NoGo", "Productive routes remain 404 by default."),
        new("Productive CRUD", "NoGo", "Domain execution, stores and persistence are not enabled for productive routes."),
        new("DELETE", "NoGo", "DELETE remains prohibited."),
        new("Sprint 9 Planning", "Go", "Controlled runtime activation decision planning can proceed.")
    ];

    public IReadOnlyCollection<CrmSprint8EvidenceContract> GetEvidence() =>
    [
        new("P1 Secret Provider Approval Decision", "Controlled read planning approved; real secret read disabled.", "Passed"),
        new("P2 Secret Provider Controlled Real NonProduction Read", "Runtime abstraction exists; default probe returns 423 and no secret value is returned.", "Passed"),
        new("P3 Common DB Controlled Real Connectivity", "Connectivity abstraction exists; default probe returns 423 and no connection string is resolved.", "Passed"),
        new("P4 Portal Auth Controlled Real Runtime Validation", "Validation abstraction exists; default probe returns 423 and no Portal HTTP/token/header read occurs.", "Passed"),
        new("P5 Locked Route Authorization Policy Integration", "Policy evaluator exists; productive routes 404 by default and locked route fixtures return 423.", "Passed"),
        new("Build", "dotnet restore/build completed.", "Passed"),
        new("Tests", "Unit and architecture tests validate Sprint 8 gates.", "Passed"),
        new("Frontend", "Readiness page and frontend verifier include Sprint 8 closure markers.", "Passed"),
        new("Docker", "docker compose config/up keeps crm-api on 8093 with no SQL Server service.", "Passed"),
        new("Health", "health/live/ready/readiness and Sprint 8 endpoints return 200.", "Passed")
    ];

    public IReadOnlyCollection<CrmSprint9RoadmapRecommendationContract> GetSprint9Roadmap() =>
    [
        new("Sprint 9 P1", "Controlled Runtime Activation Decision", "Sprint9P1ControlledRuntimeActivationDecision"),
        new("Sprint 9 P2", "Secret Provider Runtime Enablement Trial", "Sprint9P2SecretProviderRuntimeEnablementTrial"),
        new("Sprint 9 P3", "Common DB Runtime Connectivity Trial", "Sprint9P3CommonDbRuntimeConnectivityTrial"),
        new("Sprint 9 P4", "Portal Auth Runtime Validation Trial", "Sprint9P4PortalAuthRuntimeValidationTrial"),
        new("Sprint 9 P5", "Productive Route Dry-Run Trial", "Sprint9P5ProductiveRouteDryRunTrial"),
        new("Sprint 9 P6", "Sprint 9 Gate Decision", "Sprint9P6Sprint9GateDecision")
    ];
}
