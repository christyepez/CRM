namespace CRM.Application.Persistence;

public sealed class CrmPersistenceReadinessService
{
    public const string WarningText = "Persistence design review only; no database configured";

    public CrmPersistenceReadinessResponse GetReadiness() =>
        new(
            "CRM",
            "PersistenceDesignReview",
            "DesignOnly",
            false,
            false,
            false,
            false,
            "NonProduction",
            "Sprint2P2PersistenceSeam",
            WarningText,
            true,
            ["Lead", "Account", "Contact"],
            ["Opportunity"],
            [
                new("DatabaseApproval", "Pending", "Confirm approved shared local SQL Server strategy without CRM-owned SQL Server."),
                new("PortalAuthorization", "Pending", "Confirm Portal authorization before productive CRUD."),
                new("MigrationPlan", "DesignOnly", "Approve migration naming, rollback and seed policy before creating migrations.")
            ]);
}
