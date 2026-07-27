namespace CRM.Application.Foundation;

public sealed class CrmSprint3ProductizationReviewStatusService
{
    public const string WarningText = "Sprint 3 productization review only; no real activation";
    public const string NextGate = "Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening";

    public CrmSprint3ProductizationReviewStatusResponse GetStatus() =>
        new(
            "CRM",
            "Sprint3ProductizationReview",
            true,
            "NoGoForRealActivation",
            "NotReady",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "GoFoundationOnly",
            "Go",
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            NextGate,
            WarningText,
            GetDecisions(),
            GetEvidence(),
            GetSprint4Roadmap(),
            [
                "Activating productive routes before Portal Auth runtime would bypass cross-cutting ownership.",
                "Activating EF or database runtime before secret/runtime probes would risk configuration drift.",
                "DELETE remains blocked until audit, retention, recovery and data governance are approved.",
                "Foundation CRUD is non-production and must not be promoted as productive CRM behavior."
            ]);

    public IReadOnlyCollection<CrmSprint3CapabilityDecisionContract> GetDecisions() =>
    [
        NoGo("Durable Persistence", "DesignOnly evidence exists; runtime remains disabled.", "Common DB runtime probe and rollback plan."),
        NoGo("Real DB", "Logical CrmDb contract exists; no real database configured.", "Common SQL runtime probe with secrets kept outside source control."),
        NoGo("EF Runtime", "EF prototype exists behind disabled flag.", "Provider packages, EF runtime gate and migration approval."),
        NoGo("Migrations", "No migrations were created in Sprint 3.", "Migration governance and rollback validation."),
        NoGo("Secret Provider Runtime", "Secret strategy is contract-only.", "Approved secret store runtime and rotation process."),
        NoGo("Portal Auth Runtime", "Portal Auth runtime contract is validated only.", "Portal runtime adapter probe behind disabled flag."),
        NoGo("Portal Permissions Runtime", "Foundation simulation remains active.", "Real Portal permissions runtime and denied-path tests."),
        NoGo("Productive API Routes", "P5 route draft exists; routes are not registered.", "Auth and persistence gates must pass first."),
        NoGo("DELETE Endpoints", "DELETE endpoints are disabled.", "Audit, retention, restore and legal policy approval."),
        NoGo("Productive CRM UI", "Readiness UI remains foundation-only.", "API activation and UX/product approval."),
        NoGo("Audit Runtime", "Audit is a Portal capability, not active in CRM runtime.", "Portal Audit contract/runtime approval."),
        NoGo("Notification Runtime", "Notification is a Portal capability, not active in CRM runtime.", "Portal Notification contract/runtime approval."),
        NoGo("Reporting Runtime", "Reporting contracts are foundation-only.", "Reporting runtime and data model approval."),
        NoGo("Financial Runtime", "Financial contracts are foundation-only.", "Financiero runtime contract probe."),
        new("Foundation Capabilities", "GoFoundationOnly", "P1-P5 foundation endpoints remain allowed.", false, "Build, tests, verifiers and Docker foundation pass.", "Keep foundation-only guardrails."),
        new("Docker Runtime", "GoFoundationOnly", "crm-api runs without SQL Server.", false, "Docker Compose exposes CRM on 8093 only.", "Keep no-DB/no-secret compose."),
        new("Observability", "NoGo", "Health endpoints exist; productive observability not complete.", false, "Health/live/ready pass.", "Correlation, logs and production telemetry plan."),
        new("Backup/Restore", "NoGo", "No durable store exists yet.", false, "No database runtime enabled.", "Backup/restore drill after DB probe."),
        new("Rollback", "NoGo", "Code rollback exists through Git; runtime rollback not proven.", false, "No productive runtime activated.", "Rollback plan for DB/Auth/API activation."),
        new("Test Data Governance", "NoGo", "No real data is used.", false, "Foundation sample data only.", "Synthetic data policy for runtime pilots.")
    ];

    public IReadOnlyCollection<CrmSprint3EvidenceContract> GetEvidence() =>
    [
        new("P1 Durable Persistence Setup Design", "DesignOnly", "Durable persistence setup endpoint and documents exist.", "No database, EF runtime, migrations or real configuration values."),
        new("P2 Common DB Connection Contract and Secret Strategy", "ContractOnly", "Common DB and secret strategy status endpoint exists.", "No real secret provider or connection value is read."),
        new("P3 EF Prototype Behind Disabled Flag", "RuntimeDisabled", "EF prototype status endpoint exists.", "EF runtime active remains false."),
        new("P4 Portal Auth Runtime Contract Validation", "ContractOnly", "Portal Auth runtime contract endpoint exists.", "No Auth middleware, login, token storage or Portal HTTP."),
        new("P5 Productive API Route Draft Behind Disabled Flag", "DraftOnly", "Productive API route draft endpoint exists.", "Productive routes registered remains false.")
    ];

    public IReadOnlyCollection<CrmSprint4RoadmapRecommendationContract> GetSprint4Roadmap() =>
    [
        new("Sprint 4 P1", "Runtime Environment Readiness and Local Tooling Hardening", "Go", "Local tooling and validation baseline."),
        new("Sprint 4 P2", "Controlled Common DB Runtime Probe Behind Disabled Flag", "Plan", "No secrets in source control and common SQL only."),
        new("Sprint 4 P3", "Portal Auth Runtime Probe Behind Disabled Flag", "Plan", "No CRM-owned identity."),
        new("Sprint 4 P4", "Productive Routes Locked Stub Validation", "Plan", "Routes remain locked until Auth and persistence are ready."),
        new("Sprint 4 P5", "NonProduction E2E Pilot Readiness", "Plan", "Synthetic data only."),
        new("Sprint 4 P6", "Sprint 4 Gate Decision", "Plan", "Formal GO/NO-GO before activation.")
    ];

    private static CrmSprint3CapabilityDecisionContract NoGo(string capability, string evidence, string requiredBeforeGo) =>
        new(capability, "NoGo", "NotReady", false, evidence, requiredBeforeGo);
}
