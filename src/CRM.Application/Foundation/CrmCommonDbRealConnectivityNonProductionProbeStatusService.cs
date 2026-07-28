namespace CRM.Application.Foundation;

public sealed class CrmCommonDbRealConnectivityNonProductionProbeStatusService
{
    public const string SyntheticConnectionReference = "mock://crm/common-db";
    public const string WarningText = "Common DB real connectivity NonProduction probe is prepared but skipped because Secret Provider approval is not granted";
    public const string NextGate = "Sprint7P4PortalAuthRealRuntimeProbe";

    public CrmCommonDbRealConnectivityNonProductionProbeStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: "CommonDbRealConnectivityNonProductionProbe",
            FoundationMode: true,
            CommonDbRealConnectivityNonProductionProbeExists: true,
            CommonDbRealConnectivityApprovalGranted: false,
            SecretProviderRealNonProductionApprovalGranted: false,
            SecretProviderRealRuntimeProbeEnabled: false,
            ConnectionStringResolved: false,
            ConnectionStringValueMaterialized: false,
            ConnectionStringLogged: false,
            ConnectionStringReturnedToApi: false,
            CommonDbProbeEnabled: false,
            CommonDbProbeAttempted: false,
            CommonDbConnected: false,
            SqlConnectionCreated: false,
            DbConnectionCreated: false,
            UseSqlServerEnabled: false,
            EfRuntimeEnabled: false,
            AddDbContextRuntimeEnabled: false,
            MigrationsCreated: false,
            DatabaseSchemaChanged: false,
            ProductivePersistenceEnabled: false,
            ApiRequiresDatabase: false,
            UsesSecretProviderRuntime: false,
            UsesSyntheticFallback: true,
            SyntheticConnectionReference: SyntheticConnectionReference,
            ConnectionProbeSkippedBecauseSecretProviderApprovalNotGranted: true,
            NonProductionOnly: true,
            RollbackRequired: true,
            ObservabilityRequired: true,
            NextGate: NextGate,
            Warning: WarningText,
            Dependencies: GetDependencies(),
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "Common DB real connectivity must not run until Secret Provider real approval is granted.",
                "Synthetic reference is metadata only and must not be treated as a real connection value.",
                "Productive persistence remains blocked until DB, security and architecture gates pass."
            ]);

    public IReadOnlyCollection<CrmCommonDbRealConnectivityNonProductionProbeDependencyContract> GetDependencies() =>
    [
        new("Secret Provider real NonProduction approval", true, false, "Approval not granted."),
        new("Logical secret name crm-common-db-connection", true, true, "Name only; value not resolved."),
        new("Common SQL Server environment", true, false, "Not contacted in P3."),
        new("Redacted observability", true, false, "Required before any future connection attempt."),
        new("Rollback plan", true, true, "Documented; runtime still disabled.")
    ];

    public IReadOnlyCollection<CrmCommonDbRealConnectivityNonProductionProbeGateContract> GetGates() =>
    [
        new("Security approval", true, false, "Secret Provider approval remains false."),
        new("Architecture approval", true, false, "DB runtime must stay outside CRM productization until gates pass."),
        new("DevOps approval", true, false, "Common SQL access and timeout policy remain pending."),
        new("Rollback validation", true, false, "Connection probe rollback must be validated before activation."),
        new("Observability validation", true, false, "Logs and metrics must prove no connection values are exposed.")
    ];

    public IReadOnlyCollection<CrmCommonDbRealConnectivityNonProductionProbeObservationContract> GetObservations() =>
    [
        new("Connection probe skipped", true, "Secret Provider approval is not granted."),
        new("Synthetic fallback used", true, SyntheticConnectionReference),
        new("No connection value resolved", true, "Only safe metadata is returned."),
        new("No DB runtime enabled", true, "No EF runtime, schema change or connection attempt exists.")
    ];

    public IReadOnlyCollection<CrmCommonDbRealConnectivityNonProductionProbeBlockedItemContract> GetBlockedItems() =>
    [
        new("Real connection value resolution", "Secret Provider real approval is not granted.", NextGate),
        new("Common DB connection attempt", "Probe enabled flag remains false.", NextGate),
        new("EF runtime and schema changes", "Productive persistence remains disabled.", "Future persistence gate"),
        new("Portal Auth runtime", "Portal runtime is outside P3 and remains NoGo.", NextGate),
        new("Productive CRM routes", "Productization remains NotReady.", "Future productization gate")
    ];
}
