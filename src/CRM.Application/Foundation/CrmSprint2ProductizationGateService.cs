namespace CRM.Application.Foundation;

public sealed class CrmSprint2ProductizationGateService
{
    public const string WarningText = "Productization gate decision only; no productive activation";

    public CrmSprint2ProductizationGateStatusResponse GetStatus() =>
        new(
            "CRM",
            "Sprint2Closed",
            "NotReady",
            "NoGoForProductiveActivation",
            "GoFoundationOnly",
            "NoGo",
            "NoGo",
            "NoGo",
            "NoGo",
            "Go",
            "Sprint3P1DurablePersistenceSetupDesign",
            WarningText,
            true,
            false,
            false,
            false,
            false,
            [
                "P1 persistence activation design review completed without database activation.",
                "P2 NonProductionSeam active with in-memory foundation stores.",
                "P3 Portal authorization simulation active without Portal runtime HTTP.",
                "P4 foundation CRUD enabled only under /api/crm/foundation.",
                "P5 integration readiness recommended ContinueReview and NotReady.",
                "Docker build can be BLOCKED_EXTERNAL_REGISTRY when MCR metadata times out."
            ],
            [
                new("DurablePersistence", "NoGo", "DurablePersistence=false; no schema, migrations, rollback or backup/restore approved.", "Approve Sprint 3 durable persistence setup design."),
                new("RealDatabase", "NoGo", "DatabaseConfigured=false; no shared DB connection contract or secret strategy approved.", "Approve common DB connection contract and sanitized secret strategy."),
                new("PortalAuthRuntime", "NoGo", "PortalRuntimeConnected=false; authorization remains simulated.", "Validate Portal runtime permission contract and audited user context."),
                new("ProductiveCrudApi", "NoGo", "ProductiveCrudEnabled=false; routes remain foundation-only.", "Approve DB, Auth, audit and route gates together."),
                new("FoundationCrud", "GoFoundationOnly", "Foundation CRUD is explicitly non-production and in-memory.", "Keep warning labels, guards and route prefix."),
                new("Sprint3Planning", "Go", "Sprint 2 evidence is sufficient to plan controlled non-production setup.", "Start Sprint 3 P1 without productization activation.")
            ],
            [
                new("Durable Persistence", "CRM Data Architect", "NonProductionSeam", "P1-P5 evidence; no DB configured.", "NoGo", "Design schema, rollback, retention and backup/restore.", "Sprint3P1", "High"),
                new("Real DB / Common DB", "Portal/CRM DevOps", "NotConfigured", "No SQL Server compose and no connection strings.", "NoGo", "Approve common DB host, logical CRM DB and secret handling.", "Sprint3P2", "High"),
                new("EF/Migrations", "CRM Backend", "NotCreated", "No ORM runtime, entity sets, migration builder or EF provider.", "NoGo", "Prototype behind disabled flag only after DB contract.", "Sprint3P3", "High"),
                new("Portal Auth Runtime", "PortalCorporativo", "Simulated", "FoundationSimulation only.", "NoGo", "Validate Portal permission runtime and correlation contract.", "Sprint3P4", "High"),
                new("Productive CRUD API", "CRM Backend", "Disabled", "No productive /api/crm/leads/accounts/contacts routes.", "NoGo", "Require DB, Auth, audit and rollback approvals.", "Sprint3P5", "High"),
                new("Foundation CRUD", "CRM Backend", "EnabledFoundationOnly", "Foundation CRUD under /api/crm/foundation with warnings.", "GoFoundationOnly", "Do not promote without P6+ approval.", "Current", "Medium"),
                new("Docker Runtime", "DevOps", "ConfigValid", "Compose exposes crm-api on 8093 and no SQL Server.", "GoForConfigOnly", "MCR availability required for image build.", "Sprint3", "Medium"),
                new("Observability", "PortalCorporativo", "Planned", "No runtime dashboard configured.", "NoGo", "Define audit/log metrics before production.", "Sprint3", "Medium")
            ],
            [
                new("Sprint3P1", "Durable Persistence Setup Design for NonProduction", "No real DB activation", "Design only; no migrations."),
                new("Sprint3P2", "Common DB Connection Contract and Secret Strategy", "No secrets in repo", "Use placeholders and Portal/common DB ownership."),
                new("Sprint3P3", "EF Prototype Behind Disabled Flag", "Disabled flag required", "Prototype only after P2 approval."),
                new("Sprint3P4", "Portal Auth Runtime Contract Validation", "No CRM-owned login", "Validate Portal-owned auth and permissions."),
                new("Sprint3P5", "Productive API Route Draft Behind Disabled Flag", "No public productive routes", "Draft only behind disabled gate."),
                new("Sprint3P6", "Sprint 3 Productization Review", "Formal GO/NO-GO", "Close evidence before activation.")
            ],
            [
                "Foundation CRUD could be mistaken for productization if warnings are removed.",
                "Durable persistence without rollback and backup/restore would create data risk.",
                "Auth runtime outside Portal ownership would violate platform boundaries.",
                "Docker validation depends on external Microsoft Container Registry availability."
            ]);
}
