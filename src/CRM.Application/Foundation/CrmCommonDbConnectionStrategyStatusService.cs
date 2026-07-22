namespace CRM.Application.Foundation;

public sealed class CrmCommonDbConnectionStrategyStatusService
{
    public const string WarningText = "Common DB connection contract only; no real database or secrets configured";

    public CrmCommonDbConnectionStrategyStatusResponse GetStatus() =>
        new(
            "CRM",
            "CommonDbConnectionStrategy",
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            "CrmDb",
            true,
            "ContractOnly",
            "NoRealValuesInRepository",
            "Sprint3P3EfDbContextPrototypeBehindDisabledFlag",
            WarningText,
            true,
            new(
                "CrmDb",
                true,
                "CRM logical database placeholder only",
                "Common database infrastructure is external to this repo and must be approved by a later gate.",
                "No shared database with Financiero and no duplicated Portal user or permission data."),
            new(
                "ContractOnly",
                false,
                false,
                "Portal Configuration, external secret provider, environment vault, or CI/CD secure variables.",
                "Committed files, .env files, appsettings with real values, passwords, tokens, or private URLs."),
            new(
                "NoRealValuesInRepository",
                false,
                false,
                "Only logical placeholders such as CrmDb may be documented before runtime approval."),
            [
                new("CommonDatabaseContract", "Planned", false, "Approve common SQL host ownership and CRM logical database naming."),
                new("SecretProviderContract", "ContractOnly", false, "Approve external provider, access policy and rotation process."),
                new("ConnectionPolicy", "NoRealValuesInRepository", false, "Keep real values outside committed files and outside .env."),
                new("EfPrototypeGate", "Blocked", false, "Approve Sprint 3 P3 disabled EF prototype after P2 review."),
                new("ProductiveActivation", "NoGo", false, "Require DB, Auth, audit, backup/restore and rollback gates.")
            ],
            [
                "Logical database placeholder could be mistaken for a live database if copied into runtime configuration.",
                "Future secrets must come from approved secure providers only.",
                "CRM must not create SQL Server containers or share tables with Financiero.",
                "Portal user and permission data must remain Portal-owned."
            ]);
}
