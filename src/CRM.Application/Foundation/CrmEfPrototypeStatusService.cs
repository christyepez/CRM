namespace CRM.Application.Foundation;

public sealed class CrmEfPrototypeStatusService
{
    public const string WarningText = "EF/DbContext prototype only; runtime disabled and no database configured";

    public CrmEfPrototypeStatusResponse GetStatus() =>
        new(
            "CRM",
            "EfDbContextPrototypeDisabled",
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            false,
            "Sprint3P4PortalAuthRuntimeContractValidation",
            WarningText,
            true,
            [
                "CRM_EF_PROTOTYPE_EXISTS=true",
                "CRM_EF_RUNTIME_ENABLED=false",
                "CRM_DBCONTEXT_RUNTIME_ACTIVE=false",
                "CRM_MIGRATIONS_ENABLED=false",
                "CRM_DURABLE_PERSISTENCE_ENABLED=false",
                "CRM_PRODUCTIVE_CRUD_ENABLED=false"
            ],
            new("CrmDbContextPrototype", true, false, false, "Placeholder class only; no EF package or provider runtime."),
            [
                new("EF Prototype", "ExistsDisabled", true, "Prototype exists only for technical review."),
                new("EF Runtime", "Disabled", false, "Do not register EF runtime context or provider runtime."),
                new("Migrations", "NotCreated", false, "No migration files or commands in this sprint."),
                new("Foundation Stores", "RemainActive", true, "In-memory foundation stores continue to serve foundation endpoints."),
                new("Productive CRUD", "Disabled", false, "No productive /api/crm routes are exposed.")
            ],
            [
                new("DatabaseApproval", "Blocked", false, "Approve common DB, secret provider and runtime injection."),
                new("ProviderApproval", "Blocked", false, "Approve provider package and disabled registration strategy."),
                new("MigrationApproval", "Blocked", false, "Approve migration, rollback, backup/restore and synthetic data policy."),
                new("PortalAuthRuntime", "Blocked", false, "Validate Portal authorization runtime before productive API."),
                new("ProductiveActivation", "NoGo", false, "Require DB, Auth, audit and operational gates.")
            ],
            [
                "Prototype class could be mistaken for active persistence if registered in DI.",
                "Future provider activation must not bypass common DB and secret gates.",
                "Foundation in-memory stores must remain active until durable persistence is approved."
            ]);
}
