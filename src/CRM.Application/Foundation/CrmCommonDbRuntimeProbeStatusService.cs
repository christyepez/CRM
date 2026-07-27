namespace CRM.Application.Foundation;

public sealed class CrmCommonDbRuntimeProbeStatusService
{
    public const string WarningText = "Common DB runtime probe exists but is disabled; no database connection is attempted";
    public const string NextGate = "Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag";

    public CrmCommonDbRuntimeProbeStatusResponse GetStatus() =>
        new(
            "CRM",
            "CommonDbRuntimeProbe",
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            NextGate,
            WarningText,
            GetCapabilities(),
            GetSafetyGates(),
            GetBlockedItems(),
            [
                "A disabled probe can be mistaken for active persistence if flags are changed without approval.",
                "Future values must come only from approved secret tooling outside committed files.",
                "CRM must keep using common SQL infrastructure and must not define its own SQL Server service.",
                "API startup must remain independent from database availability."
            ]);

    public IReadOnlyCollection<CrmCommonDbRuntimeProbeCapabilityContract> GetCapabilities() =>
    [
        new("Common DB Runtime Probe", "Exists", true, "Contract and disabled placeholder exist."),
        new("Common DB Runtime Probe Enabled", "Disabled", false, "commonDbRuntimeProbeEnabled=false."),
        new("API Requires Database", "False", false, "API startup and health do not depend on DB."),
        new("SQL Server Owned By CRM", "False", false, "CRM Compose must not define SQL Server."),
        new("EF Runtime", "Disabled", false, "No runtime EF provider or activation is configured.")
    ];

    public IReadOnlyCollection<CrmCommonDbRuntimeProbeSafetyGateContract> GetSafetyGates() =>
    [
        new("Secret provider approved", "NoGo", false, "Approve external secret provider and rotation policy."),
        new("Common DB approved", "NoGo", false, "Confirm shared local SQL infrastructure and CRM logical database."),
        new("Rollback and backup documented", "NoGo", false, "Document restore and rollback drill before enabling."),
        new("Synthetic data defined", "NoGo", false, "Define non-production synthetic data only."),
        new("Portal Auth gate clear", "NoGo", false, "Coordinate with Sprint 4 P3 Portal Auth probe.")
    ];

    public IReadOnlyCollection<CrmCommonDbRuntimeProbeBlockedItemContract> GetBlockedItems() =>
    [
        new("Connection values", "Blocked", "No real values may be committed or required by default."),
        new("Secret reads", "Blocked", "No runtime secret access in this package."),
        new("Database connection", "Blocked", "No connection is attempted by runtime."),
        new("Migrations", "Blocked", "No migration files or baseline in this package."),
        new("Productive CRUD", "Blocked", "Foundation CRUD remains separate and non-production.")
    ];
}
