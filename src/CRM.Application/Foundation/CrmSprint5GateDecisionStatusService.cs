namespace CRM.Application.Foundation;

public sealed class CrmSprint5GateDecisionStatusService
{
    public const string WarningText = "Sprint 5 gate decision only; no real activation";
    public const string NextGate = "Sprint6P1NonProductionRuntimeApprovalPackage";

    public CrmSprint5GateDecisionStatusResponse GetStatus() =>
        new(
            "CRM",
            "Sprint5GateDecision",
            true,
            "GoForControlledNonProductionPreparation",
            "NoGo",
            "NotReady",
            "NoGoForRuntimeRead",
            "NoGoForConnectionAttempt",
            "NoGoForPortalHttpOrTokenRead",
            "NoGo",
            "NoGoForRuntimeRegistration",
            "NoGo",
            "NoGo",
            "NoGo",
            "Go",
            NextGate,
            WarningText,
            GetCapabilityDecisions(),
            GetEvidence(),
            GetSprint6Roadmap(),
            [
                "Sprint 6 must not interpret preparation approval as real activation approval.",
                "Secret provider, DB and Portal Auth probes require explicit non-production approvals before runtime trials.",
                "Negative route checks must remain mandatory while productive routes are NoGo."
            ],
            [
                "Real activation",
                "Secret reads",
                "Database connection attempts",
                "Portal HTTP or token/header reads",
                "Locked stub runtime registration",
                "Productive CRUD, DELETE and productive UI"
            ]);

    public IReadOnlyCollection<CrmSprint5CapabilityDecisionContract> GetCapabilityDecisions() =>
    [
        new("Runtime probe activation plan", "PreparedOnly", "P1 defines the plan but approvals remain false."),
        new("Secret Provider runtime", "NoGoForRuntimeRead", "P2 validates contracts only; no secret reads."),
        new("Common DB runtime", "NoGoForConnectionAttempt", "P3 keeps DB probe disabled and connection attempts false."),
        new("Portal Auth runtime", "NoGoForPortalHttpOrTokenRead", "P4 keeps Portal HTTP and token/header reads false."),
        new("Locked productive route stubs", "NoGoForRuntimeRegistration", "P5 documents stubs only; routes remain 404 by default."),
        new("Productive CRUD", "NoGo", "DB, Auth and route gates are not approved."),
        new("DELETE", "NoGo", "DELETE remains prohibited."),
        new("Sprint 6 planning", "Go", "Controlled non-production preparation can continue.")
    ];

    public IReadOnlyCollection<CrmSprint5EvidenceContract> GetEvidence() =>
    [
        new("Build", "dotnet build CRM.sln --no-restore", "Passed"),
        new("Tests", "Unit and architecture tests passed in P1-P5 progression.", "Passed"),
        new("Frontend", "pnpm build/test and foundation verifier passed.", "Passed"),
        new("Docker", "docker compose config/up passed with crm-api on 8093 and no SQL Server.", "Passed"),
        new("Health", "Health, readiness and Sprint 5 foundation endpoints returned 200.", "Passed"),
        new("Negative routes", "/api/crm/leads, /accounts and /contacts returned 404.", "Passed")
    ];

    public IReadOnlyCollection<CrmSprint6RoadmapRecommendationContract> GetSprint6Roadmap() =>
    [
        new("Sprint 6 P1", "NonProduction Runtime Approval Package", "Sprint6P1NonProductionRuntimeApprovalPackage"),
        new("Sprint 6 P2", "Secret Provider Safe Mock Activation", "Sprint6P2SecretProviderSafeMockActivation"),
        new("Sprint 6 P3", "Common DB Connectivity Dry-Run Contract", "Sprint6P3CommonDbConnectivityDryRunContract"),
        new("Sprint 6 P4", "Portal Auth Token Propagation Dry-Run Contract", "Sprint6P4PortalAuthTokenPropagationDryRunContract"),
        new("Sprint 6 P5", "Locked Stub Runtime Registration Trial", "Sprint6P5LockedStubRuntimeRegistrationTrial"),
        new("Sprint 6 P6", "Sprint 6 Gate Decision", "Sprint6P6GateDecision")
    ];
}
