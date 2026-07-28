namespace CRM.Application.Foundation;

public sealed class CrmCommonDbConnectivityDryRunStatusService
{
    public const string SyntheticReference = "mock://crm/common-db";
    public const string WarningText = "Common DB connectivity dry-run contract only; no database connection is attempted";
    public const string NextGate = "Sprint6P4PortalAuthTokenPropagationDryRunContract";

    public CrmCommonDbConnectivityDryRunStatusResponse GetStatus() =>
        new(
            "CRM",
            "CommonDbConnectivityDryRunContract",
            true,
            true,
            false,
            false,
            false,
            true,
            true,
            SyntheticReference,
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
            GetDependencies(),
            GetSafetyGates(),
            GetObservability(),
            GetBlockedItems(),
            [
                "The synthetic reference must not be transformed into a real connection string.",
                "P3 must remain contract-only until a future explicit connection approval.",
                "Future persistence activation still requires security, architecture, rollback and observability approval."
            ]);

    public IReadOnlyCollection<CrmCommonDbConnectivityDryRunDependencyContract> GetDependencies() =>
    [
        new("Secret Provider Safe Mock metadata", true, true, "Uses only the synthetic reference mock://crm/common-db."),
        new("Common SQL Server", true, false, "Not contacted in P3; ownership remains external to CRM."),
        new("Synthetic data approval", true, false, "Required before any future connection attempt."),
        new("Rollback approval", true, false, "Required before any future DB runtime trial.")
    ];

    public IReadOnlyCollection<CrmCommonDbConnectivityDryRunSafetyGateContract> GetSafetyGates() =>
    [
        new("No database connection", true, true, "ConnectionAttempted remains false."),
        new("No real connection string", true, true, "Only mock metadata is exposed."),
        new("No EF runtime or migrations", true, true, "No provider activation or migration scripts are created."),
        new("No secret or environment reads", true, true, "P3 does not read files, environment values or real secret stores."),
        new("No API database dependency", true, true, "Foundation endpoints continue to run without DB.")
    ];

    public IReadOnlyCollection<CrmCommonDbConnectivityDryRunObservabilityContract> GetObservability() =>
    [
        new("Dry-run status endpoint", true, true, "Reports contract state through foundation API."),
        new("Connection attempted flag", true, true, "Always false in P3."),
        new("Synthetic reference marker", true, true, "Reports mock://crm/common-db only."),
        new("Negative route checks", true, true, "Productive CRM routes must remain inactive.")
    ];

    public IReadOnlyCollection<CrmCommonDbConnectivityDryRunBlockedItemContract> GetBlockedItems() =>
    [
        new("Real DB connection", "Common DB dry-run approval is not granted.", "Future explicit DB connection gate"),
        new("Connection string resolution", "Only synthetic metadata is allowed in P3.", "Future secret/runtime gate"),
        new("EF runtime", "Provider activation remains blocked.", "Future persistence activation gate"),
        new("Migrations", "No schema changes are approved.", "Future migration approval gate"),
        new("Portal Auth runtime", "Portal Auth dry-run is the next separate gate.", NextGate)
    ];
}
