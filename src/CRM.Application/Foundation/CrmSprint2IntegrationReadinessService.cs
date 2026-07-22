namespace CRM.Application.Foundation;

public sealed class CrmSprint2IntegrationReadinessService
{
    public const string WarningText = "Integration readiness review only; no productive activation";

    public CrmSprint2IntegrationReadinessStatusResponse GetStatus() =>
        new(
            "CRM",
            "IntegrationReadinessReview",
            "NotReady",
            false,
            false,
            false,
            true,
            false,
            false,
            "ContinueReview",
            "Sprint2P6ProductizationGateDecision",
            WarningText,
            true,
            [
                "P1 persistence design review completed with DesignOnly and no database configured.",
                "P2 NonProductionSeam completed with in-memory FoundationStore adapters.",
                "P3 Portal authorization simulation completed without real Portal runtime.",
                "P4 controlled foundation CRUD completed under /api/crm/foundation only.",
                "Docker build remains environment-dependent on MCR: BLOCKED_EXTERNAL_REGISTRY when metadata times out."
            ],
            [
                new("DurableDatabase", "Blocked", false, "Approve shared SQL strategy, migrations, rollback, backup/restore, retention and secrets management."),
                new("PortalAuthRuntime", "Blocked", false, "Approve Portal runtime adapter, permission contracts, tenant context and audited authorization flow."),
                new("ProductiveCrudApi", "Blocked", false, "Approve real Auth, durable persistence, audit/observability and production data policy."),
                new("AuditObservability", "ReviewRequired", false, "Define audit event ownership, correlation, alerting and operational dashboards."),
                new("FeatureFlags", "ReviewRequired", false, "Define explicit activation flags, rollout plan and rollback plan.")
            ],
            [
                new("Foundation CRUD mistaken for productization", "High", "Keep warnings, foundation routes and guardrail tests active.", true),
                new("Durable persistence without migration governance", "High", "Require migration, rollback, backup and retention approvals before DB activation.", true),
                new("Auth runtime bypassing Portal ownership", "High", "Require Portal-owned runtime authorization before productive endpoints.", true),
                new("Docker registry availability", "Medium", "Document BLOCKED_EXTERNAL_REGISTRY separately from application correctness.", false)
            ],
            [
                new("Database", "DoNotActivate", "DatabaseReady=false; no migrations, secrets or rollback plan approved."),
                new("Auth", "DoNotActivate", "AuthReady=false; Portal runtime is still simulated."),
                new("ProductiveCrud", "DoNotActivate", "ProductiveCrudReady=false; only foundation preview routes are approved."),
                new("Sprint2P6", "ContinueReview", "Prepare productization gate decision with explicit GO/NO-GO criteria.")
            ]);
}
