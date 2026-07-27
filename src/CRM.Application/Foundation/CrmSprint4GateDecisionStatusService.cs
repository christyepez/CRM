namespace CRM.Application.Foundation;

public sealed class CrmSprint4GateDecisionStatusService
{
    public const string WarningText = "Sprint 4 gate decision only; no real activation";
    public const string NextGate = "Sprint5P1ControlledRuntimeProbeActivationPlan";

    public CrmSprint4GateDecisionStatusResponse GetStatus() =>
        new(
            "CRM",
            "Sprint4GateDecision",
            true,
            "GoForNonProductionFoundationPilot",
            "NoGo",
            "NotReady",
            "NoGo",
            "NoGoForRuntimeActivation",
            "NoGoForRuntimeActivation",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "GoFoundationOnly",
            "Go",
            NextGate,
            WarningText,
            GetCapabilityDecisions(),
            GetEvidence(),
            GetSprint5Roadmap(),
            [
                "Foundation pilot readiness can be mistaken for productive readiness if Sprint 5 gates are skipped.",
                "Runtime probes exist but remain disabled until explicit non-production activation approval.",
                "Productive route stubs are document-only; runtime registration still requires a later gate."
            ],
            [
                "Real DB runtime activation.",
                "Portal Auth runtime activation.",
                "Productive CRM routes and DELETE endpoints.",
                "Productive CRM UI and login flows."
            ]);

    public IReadOnlyCollection<CrmSprint4GateCapabilityDecisionContract> GetCapabilityDecisions() =>
    [
        new("Runtime readiness", "Prepared", "P1 tooling, Docker and health checks passed.", false),
        new("Common DB runtime probe", "NoGoForRuntimeActivation", "P2 probe exists but is disabled.", false),
        new("Portal Auth runtime probe", "NoGoForRuntimeActivation", "P3 probe exists but does not read tokens or call Portal.", false),
        new("Productive routes", "NoGo", "P4 strategy is DocumentOnlyPreferred and routes are not registered.", false),
        new("Non-production E2E pilot", "GoFoundationOnly", "P5 health and negative route checks passed.", false)
    ];

    public IReadOnlyCollection<CrmSprint4EvidenceContract> GetEvidence() =>
    [
        new("Build", "Passed", "dotnet build CRM.sln --no-restore", true),
        new("Tests", "Passed", "DOTNET_ROLL_FORWARD=Major dotnet test CRM.sln --no-build", true),
        new("Frontend", "Passed", "pnpm run build; pnpm run test", true),
        new("Docker", "Passed", "docker compose config; docker compose up -d --build; docker compose ps", true),
        new("Health", "Passed", "/health, /health/live, /health/ready and foundation endpoints returned 200", true),
        new("Negative routes", "Passed", "/api/crm/leads, /api/crm/accounts and /api/crm/contacts returned 404", true)
    ];

    public IReadOnlyCollection<CrmSprint5RoadmapRecommendationContract> GetSprint5Roadmap() =>
    [
        new("Sprint 5 P1", "Controlled Runtime Probe Activation Plan", "Sprint5P1ControlledRuntimeProbeActivationPlan", false),
        new("Sprint 5 P2", "Secret Provider Runtime Contract Validation", "Sprint5P2SecretProviderRuntimeContractValidation", false),
        new("Sprint 5 P3", "Common DB Probe Optional Activation in NonProduction", "Sprint5P3CommonDbProbeOptionalActivation", false),
        new("Sprint 5 P4", "Portal Auth Probe Optional Activation in NonProduction", "Sprint5P4PortalAuthProbeOptionalActivation", false),
        new("Sprint 5 P5", "Locked Productive Route Stub Trial in NonProduction", "Sprint5P5LockedProductiveRouteStubTrial", false),
        new("Sprint 5 P6", "Sprint 5 Gate Decision", "Sprint5P6GateDecision", false)
    ];
}
