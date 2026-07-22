namespace CRM.Application.Foundation;

public sealed class CrmDurablePersistenceSetupStatusService
{
    public const string WarningText = "Durable persistence setup design only; no database, EF runtime, migrations, or connection strings configured";

    public CrmDurablePersistenceSetupStatusResponse GetStatus() =>
        new(
            "CRM",
            "DurablePersistenceSetupDesign",
            "DesignOnly",
            false,
            false,
            false,
            false,
            false,
            false,
            "PlannedOnly",
            "PlannedOnly",
            "NoGo",
            "Sprint3P2CommonDbConnectionContractAndSecretStrategy",
            WarningText,
            true,
            new(
                "Common environment database owned outside CRM",
                "CRM must not define a SQL Server container",
                "CRM logical database only after gate approval",
                "No shared database with Financiero and no Portal user or permission data duplication"),
            new("PlannedOnly", true, true, false),
            new("PlannedOnly", false, "Portal Configuration or external secure environment provider", "No hardcoded values, no .env and no secrets in repo"),
            [
                new("Durable Persistence", "DesignOnly", "NoGo", "Approve common DB contract, migration plan and rollback gates."),
                new("Real Database", "NotConfigured", "NoGo", "Approve logical CRM DB naming and secure connection source."),
                new("EF Runtime", "NotEnabled", "NoGo", "Approve disabled prototype in Sprint 3 P3 after connection strategy."),
                new("Migrations", "PlannedOnly", "NoGo", "Approve idempotent scripts, rollback and backup/restore evidence."),
                new("Connection Strings", "NotConfigured", "NoGo", "Approve secret provider and secure runtime injection path."),
                new("Productive Activation", "NoGo", "NoGo", "Require DB, Auth, audit and operational readiness gates.")
            ],
            [
                "Approve common DB connection contract without CRM-owned SQL Server.",
                "Approve secret provider strategy without committed secrets or .env.",
                "Approve migration, rollback, backup and restore process.",
                "Approve synthetic non-production test data policy.",
                "Keep Portal Auth runtime and productive CRUD blocked."
            ],
            [
                "Design documents could be mistaken for activation if guards are bypassed.",
                "Connection values must not be committed in any future sprint.",
                "CRM data must not share tables or ownership with Financiero.",
                "Portal user and permission data must remain Portal-owned."
            ]);
}
