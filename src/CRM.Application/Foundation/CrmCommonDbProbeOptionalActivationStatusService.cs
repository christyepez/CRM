namespace CRM.Application.Foundation;

public sealed class CrmCommonDbProbeOptionalActivationStatusService
{
    public const string WarningText = "Common DB probe optional activation only; no database connection is attempted";
    public const string NextGate = "Sprint5P4PortalAuthProbeOptionalActivationInNonProduction";

    public CrmCommonDbProbeOptionalActivationStatusResponse GetStatus() =>
        new(
            "CRM",
            "CommonDbProbeOptionalActivation",
            true,
            true,
            false,
            false,
            false,
            true,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            NextGate,
            WarningText,
            GetActivationGates(),
            GetDependencies(),
            GetRollbackRequirements(),
            GetBlockedItems(),
            [
                "Optional activation wording could be confused with approval if disabled flags are ignored.",
                "Future non-production activation must validate secret provider approvals before any database probe.",
                "Negative route checks must remain mandatory while CRM has no productive persistence runtime."
            ]);

    public IReadOnlyCollection<CrmCommonDbProbeActivationGateContract> GetActivationGates() =>
    [
        new("Secret provider runtime approval", "Security", true, false, "P2 approvals closed, no real values in files and secret reads explicitly approved."),
        new("Non-production synthetic data approval", "Data Architect", true, false, "Synthetic-only data set approved for probe execution."),
        new("Rollback approval", "DevOps", true, false, "Probe flag rollback and health regression procedure approved."),
        new("Architecture boundary approval", "Architecture Governance", true, false, "Common SQL container reuse and logical CRM database boundary approved.")
    ];

    public IReadOnlyCollection<CrmCommonDbProbeDependencyContract> GetDependencies() =>
    [
        new("Secret Provider Runtime", true, false, "Required before activation; not connected in P3."),
        new("Secret reads", true, false, "Required before activation; disabled in P3."),
        new("Common SQL container", true, false, "Must be reused from shared environment; CRM does not define SQL Server."),
        new("Synthetic data", true, false, "Required before any future non-production probe.")
    ];

    public IReadOnlyCollection<CrmCommonDbProbeRollbackContract> GetRollbackRequirements() =>
    [
        new("Keep probe disabled by default", true, "Any unexpected database access attempt."),
        new("Return to foundation-only endpoints", true, "Health/readiness regression."),
        new("Preserve negative route checks", true, "Any productive CRM route returns success.")
    ];

    public IReadOnlyCollection<CrmCommonDbProbeBlockedItemContract> GetBlockedItems() =>
    [
        new("Common DB probe activation", "Approval gates remain false."),
        new("Database connection attempts", "Secret provider runtime is not connected and reads are disabled."),
        new("EF runtime or migrations", "P3 is contract-only and does not activate persistence."),
        new("Productive CRM routes and DELETE", "Productization remains NoGo.")
    ];
}
