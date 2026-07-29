namespace CRM.Application.Foundation;

public sealed class CrmSprint7GateDecisionStatusService
{
    public const string StatusName = "Sprint7GateDecision";
    public const string WarningText = "Sprint 7 gate decision only; no real activation";
    public const string NextGate = "Sprint8P1SecretProviderApprovalDecision";

    public CrmSprint7GateDecisionStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            OverallDecision: "GoForSprint8ControlledRuntimeApprovalAndPilotPlanning",
            RealActivationDecision: "NoGo",
            SecretProviderRealRuntimeDecision: "NoGo",
            CommonDbRealConnectionDecision: "NoGo",
            PortalAuthRealRuntimeDecision: "NoGo",
            LockedProductiveRouteRegistrationDecision: "GoOnlyAsExplicitNonProductionLocked423",
            ProductiveRoutesDefaultDecision: "NoGo",
            ProductiveCrudDecision: "NoGo",
            DeleteDecision: "NoGo",
            ProductiveUiDecision: "NoGo",
            ProductizationStatus: "NotReady",
            Sprint8PlanningDecision: "Go",
            NextGate: NextGate,
            Warning: WarningText,
            CapabilityDecisions: GetCapabilityDecisions(),
            Evidence: GetEvidence(),
            Sprint8Roadmap: GetSprint8Roadmap(),
            Risks:
            [
                "Sprint 8 planning must not be interpreted as real activation approval.",
                "Secret Provider, Common DB and Portal Auth runtime gates still require explicit approval, observability and rollback evidence.",
                "Locked route registration is allowed only as explicit NonProduction 423 and must not become hidden CRUD."
            ],
            BlockedItems:
            [
                "Real Secret Provider reads",
                "Real Common DB connection and EF runtime",
                "Portal Auth runtime and token/header reads",
                "Default productive route registration",
                "Productive CRUD, DELETE and Productive UI"
            ]);

    public IReadOnlyCollection<CrmSprint7CapabilityDecisionContract> GetCapabilityDecisions() =>
    [
        new("Secret Provider Real NonProduction Approval", "PreparedOnly", "Approval package exists but approval is not granted."),
        new("Secret Provider Real NonProduction Runtime Probe", "NoGo", "Probe exists but is disabled and no real secret read is attempted."),
        new("Common DB Real Connectivity NonProduction Probe", "NoGo", "Probe exists but is disabled; connection string is not resolved and DB is not connected."),
        new("Portal Auth Real Runtime Probe", "NoGo", "Probe exists but is skipped; no Portal HTTP, token read or header read occurs."),
        new("Locked Productive Route Runtime Registration With 423", "GoOnlyAsExplicitNonProductionLocked423", "Routes remain 404 by default; explicit NonProduction flag returns 423 without side effects."),
        new("Productive Routes Default", "NoGo", "Productive CRM routes are not registered by default."),
        new("Productive CRUD", "NoGo", "No domain execution, stores or persistence are enabled."),
        new("DELETE", "NoGo", "DELETE remains prohibited."),
        new("Sprint 8 Planning", "Go", "Controlled runtime approval and pilot planning can proceed.")
    ];

    public IReadOnlyCollection<CrmSprint7EvidenceContract> GetEvidence() =>
    [
        new("P1", "Secret Provider real NonProduction approval exists; approval granted false.", "Passed"),
        new("P2", "Secret Provider runtime probe is prepared but disabled; real secret read false.", "Passed"),
        new("P3", "Common DB real connectivity probe is prepared but disabled; connection string resolved false.", "Passed"),
        new("P4", "Portal Auth real runtime probe is prepared but skipped; Portal HTTP and token/header reads false.", "Passed"),
        new("P5", "Locked route registration exists; default 404 and explicit NonProduction 423 fixture covered.", "Passed"),
        new("Build", "dotnet restore/build completed.", "Passed"),
        new("Tests", "Unit and architecture tests validate Sprint 7 gates.", "Passed"),
        new("Frontend", "Readiness page and frontend verifier include Sprint 7 closure markers.", "Passed"),
        new("Docker", "docker compose config/up keeps crm-api on 8093 with no SQL Server service.", "Passed"),
        new("Health", "health/live/ready/readiness and Sprint 7 endpoints return 200.", "Passed")
    ];

    public IReadOnlyCollection<CrmSprint8RoadmapRecommendationContract> GetSprint8Roadmap() =>
    [
        new("Sprint 8 P1", "Secret Provider Approval Decision", "Sprint8P1SecretProviderApprovalDecision"),
        new("Sprint 8 P2", "Secret Provider Controlled Real NonProduction Read", "Sprint8P2SecretProviderControlledRealNonProductionRead"),
        new("Sprint 8 P3", "Common DB Controlled Real Connectivity", "Sprint8P3CommonDbControlledRealConnectivity"),
        new("Sprint 8 P4", "Portal Auth Controlled Real Runtime Validation", "Sprint8P4PortalAuthControlledRealRuntimeValidation"),
        new("Sprint 8 P5", "Locked Route Authorization Policy Integration", "Sprint8P5LockedRouteAuthorizationPolicyIntegration"),
        new("Sprint 8 P6", "Sprint 8 Gate Decision", "Sprint8P6Sprint8GateDecision")
    ];
}
