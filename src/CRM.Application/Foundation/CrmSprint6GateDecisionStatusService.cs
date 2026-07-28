namespace CRM.Application.Foundation;

public sealed class CrmSprint6GateDecisionStatusService
{
    public const string WarningText = "Sprint 6 gate decision only; no real activation";
    public const string NextGate = "Sprint7P1SecretProviderRealNonProductionApproval";

    public CrmSprint6GateDecisionStatusResponse GetStatus() =>
        new(
            "CRM",
            "Sprint6GateDecision",
            true,
            "GoForSprint7ControlledNonProductionActivationPlanning",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "NotReady",
            "Go",
            NextGate,
            WarningText,
            GetCapabilityDecisions(),
            GetEvidence(),
            GetSprint7Roadmap(),
            [
                "Sprint 7 planning approval must not be treated as real runtime activation approval.",
                "Real secret provider, DB connectivity and Portal Auth probes require explicit NonProduction approvals and rollback evidence.",
                "Productive routes must remain inactive until locked 423 stubs and later productive gates are approved."
            ],
            [
                "Real secret provider runtime",
                "Real common DB connection",
                "Portal Auth runtime and token/header propagation",
                "Locked stub runtime registration",
                "Productive routes, CRUD, DELETE and productive UI"
            ]);

    public IReadOnlyCollection<CrmSprint6CapabilityDecisionContract> GetCapabilityDecisions() =>
    [
        new("NonProduction Runtime Approval Package", "PreparedOnly", "P1 created approval evidence but RealActivationApprovalGranted remains false."),
        new("Secret Provider Safe Mock", "GoForMockOnly", "P2 validates safe synthetic metadata; real secret provider runtime remains NoGo."),
        new("Common DB Connectivity Dry-Run", "NoGoForRealConnection", "P3 keeps database connectivity attempts, provider runtime, EF runtime and migrations false."),
        new("Portal Auth Token Propagation Dry-Run", "NoGoForRealRuntime", "P4 keeps token reads, header reads and Portal HTTP attempts false."),
        new("Locked Stub Runtime Registration Trial", "NoGoForRuntimeRegistration", "P5 keeps productive routes unregistered and negative route checks at 404."),
        new("Productive Routes", "NoGo", "Productive `/api/crm/leads`, `/accounts` and `/contacts` remain inactive."),
        new("DELETE", "NoGo", "DELETE remains prohibited."),
        new("Sprint 7 Planning", "Go", "Controlled NonProduction activation planning can proceed with explicit gates.")
    ];

    public IReadOnlyCollection<CrmSprint6EvidenceContract> GetEvidence() =>
    [
        new("Build", "dotnet build CRM.sln --no-restore", "Passed"),
        new("Tests", "Unit and architecture tests passed through P1-P5 progression.", "Passed"),
        new("Frontend", "pnpm build/test and foundation verifier passed.", "Passed"),
        new("Docker", "docker compose config/up passed with crm-api on 8093 and no SQL Server.", "Passed"),
        new("Health", "Health, readiness and Sprint 6 foundation endpoints returned 200.", "Passed"),
        new("Negative routes", "/api/crm/leads, /accounts and /contacts returned 404.", "Passed"),
        new("Security", "No real secrets, token/header reads, Auth middleware or Portal HTTP activated.", "Passed"),
        new("Persistence", "No DB runtime, EF runtime, migrations or connection strings activated.", "Passed")
    ];

    public IReadOnlyCollection<CrmSprint7RoadmapRecommendationContract> GetSprint7Roadmap() =>
    [
        new("Sprint 7 P1", "Secret Provider Real NonProduction Approval", "Sprint7P1SecretProviderRealNonProductionApproval"),
        new("Sprint 7 P2", "Secret Provider Real NonProduction Runtime Probe", "Sprint7P2SecretProviderRealNonProductionRuntimeProbe"),
        new("Sprint 7 P3", "Common DB Real Connectivity NonProduction Probe", "Sprint7P3CommonDbRealConnectivityNonProductionProbe"),
        new("Sprint 7 P4", "Portal Auth Real Runtime Probe", "Sprint7P4PortalAuthRealRuntimeProbe"),
        new("Sprint 7 P5", "Locked Productive Route Runtime Registration With 423", "Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423"),
        new("Sprint 7 P6", "Sprint 7 Gate Decision", "Sprint7P6Sprint7GateDecision")
    ];
}
