namespace CRM.Application.Foundation;

public sealed class CrmCommonDbControlledRealConnectivityStatusService
{
    public const string StatusName = "CommonDbControlledRealConnectivity";
    public const string ApprovedSecretName = "crm-common-db-connection";
    public const string WarningText = "Common DB controlled real connectivity is disabled by default and never exposes connection strings";
    public const string NextGate = "Sprint8P4PortalAuthControlledRealRuntimeValidation";

    public CrmCommonDbControlledRealConnectivityStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            CommonDbControlledRealConnectivityExists: true,
            CommonDbControlledRealConnectivityApproved: true,
            CommonDbControlledRealConnectivityEnabled: false,
            CommonDbConnectivityAttempted: false,
            CommonDbConnected: false,
            SecretProviderAvailabilityMetadataUsed: true,
            SecretValueReturnedToApi: false,
            ConnectionStringResolved: false,
            ConnectionStringMaterializedInPublicContract: false,
            ConnectionStringLogged: false,
            ConnectionStringReturnedToApi: false,
            SqlConnectionCreated: false,
            DbConnectionCreated: false,
            DbConnectionOpened: false,
            EfRuntimeEnabled: false,
            AddDbContextRuntimeEnabled: false,
            UseSqlServerEnabled: false,
            MigrationsCreated: false,
            DatabaseSchemaChanged: false,
            ProductivePersistenceEnabled: false,
            ProductiveCrudEnabled: false,
            ApiRequiresDatabase: false,
            NonProductionOnly: true,
            FailClosedByDefault: true,
            NextGate: NextGate,
            Warning: WarningText,
            Probe: GetProbe(),
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "A future explicit NonProduction probe must keep connection values inside the infrastructure boundary.",
                "Schema changes, migrations and CRUD remain separate NoGo gates.",
                "P4 Portal Auth validation must not infer DB readiness from connection metadata alone."
            ]);

    public CrmCommonDbControlledRealConnectivityProbeContract GetProbe() =>
        new(
            SecretName: ApprovedSecretName,
            ProbeAttempted: false,
            ProviderConfigured: false,
            ConnectionAttempted: false,
            Connected: false,
            TimeoutApplied: true,
            TimeoutSeconds: 3,
            ConnectionStringReturned: false,
            ConnectionStringLogged: false);

    public IReadOnlyCollection<CrmCommonDbControlledRealConnectivityGateContract> GetGates() =>
    [
        new("Secret Provider P2 metadata", true, true, "P3 depends on approved sanitized Secret Provider availability metadata."),
        new("Approved logical secret name", true, true, $"Only {ApprovedSecretName} is valid."),
        new("Explicit NonProduction enable flag", true, false, "Default remains disabled and fail-closed."),
        new("Connection string redaction", true, true, "Public contracts contain only booleans and sanitized metadata."),
        new("No schema or CRUD", true, true, "No migrations, schema changes, EF runtime or productive CRUD are enabled.")
    ];

    public IReadOnlyCollection<CrmCommonDbControlledRealConnectivityObservationContract> GetObservations() =>
    [
        new("Default disabled", true, "CommonDbControlledRealConnectivityEnabled=false."),
        new("No connection attempted", true, "CommonDbConnectivityAttempted=false."),
        new("No connection string exposure", true, "ConnectionStringReturnedToApi=false and ConnectionStringLogged=false."),
        new("No EF runtime", true, "EfRuntimeEnabled=false and AddDbContextRuntimeEnabled=false."),
        new("No schema changes", true, "MigrationsCreated=false and DatabaseSchemaChanged=false."),
        new("No product routes", true, "ProductiveCrudEnabled=false and ApiRequiresDatabase=false.")
    ];

    public IReadOnlyCollection<CrmCommonDbControlledRealConnectivityBlockedItemContract> GetBlockedItems() =>
    [
        new("Default DB connectivity", "The explicit NonProduction flag is off.", NextGate),
        new("Production connectivity", "Production remains NoGo.", NextGate),
        new("Connection string exposure", "Values cannot be returned, logged, cached or persisted.", NextGate),
        new("EF, migrations and CRUD", "Separate gates are required before any durable persistence activation.", NextGate)
    ];
}
