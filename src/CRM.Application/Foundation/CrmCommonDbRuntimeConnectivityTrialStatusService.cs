namespace CRM.Application.Foundation;

public sealed class CrmCommonDbRuntimeConnectivityTrialStatusService
{
    public const string StatusName = "CommonDbRuntimeConnectivityTrial";
    public const string ApprovedSecretName = "crm-common-db-connection";
    public const string NextGate = "Sprint9P4PortalAuthRuntimeValidationTrial";
    public const string WarningText = "Common DB runtime connectivity trial is disabled by default and never exposes connection strings";

    public CrmCommonDbRuntimeConnectivityTrialStatusResponse GetStatus() =>
        new(
            Module: "CRM",
            Status: StatusName,
            FoundationMode: true,
            CommonDbRuntimeConnectivityTrialExists: true,
            CommonDbRuntimeConnectivityTrialApproved: true,
            CommonDbRuntimeConnectivityTrialEnabled: false,
            CommonDbConnectionAttempted: false,
            CommonDbConnected: false,
            CommonDbConnectionStringResolved: false,
            CommonDbConnectionStringReturnedToApi: false,
            CommonDbConnectionStringLogged: false,
            CommonDbConnectionStringPersisted: false,
            CommonDbConnectionStringCached: false,
            SecretProviderMetadataDependencyValidated: true,
            SchemaCreated: false,
            MigrationExecuted: false,
            EfRuntimeEnabled: false,
            ProductivePersistenceEnabled: false,
            NonProductionOnly: true,
            ProductionBlocked: true,
            FailClosedByDefault: true,
            RollbackAvailable: true,
            ObservabilityMetadataOnly: true,
            NextGate: NextGate,
            Warning: WarningText,
            ApprovedSecretName: ApprovedSecretName,
            Gates: GetGates(),
            Observations: GetObservations(),
            BlockedItems: GetBlockedItems(),
            Risks:
            [
                "P3 can use only sanitized Secret Provider metadata from Sprint 9 P2.",
                "Any future real connectivity must keep connection strings inside infrastructure boundaries.",
                "Schema, migrations, EF runtime and productive CRUD remain separate NoGo gates."
            ]);

    public IReadOnlyCollection<CrmCommonDbRuntimeConnectivityTrialGateContract> GetGates() =>
    [
        new("Secret Provider P2 metadata", true, true, "P2 metadata-only boundary exists and is the only allowed dependency."),
        new("Crm:RuntimeTrials:CommonDbConnectivityEnabled", true, false, "Flag is false by default."),
        new("NonProductionOnly", true, true, "Production is blocked."),
        new("Connection string redaction", true, true, "No public contract contains connection string values."),
        new("No schema or migrations", true, true, "P3 does not create schema and does not execute migrations.")
    ];

    public IReadOnlyCollection<CrmCommonDbRuntimeConnectivityTrialObservationContract> GetObservations() =>
    [
        new("Default disabled", true, "CommonDbRuntimeConnectivityTrialEnabled=false."),
        new("No connection attempted", true, "CommonDbConnectionAttempted=false."),
        new("No connection string exposure", true, "Returned/logged/persisted/cached flags are false."),
        new("Secret Provider dependency", true, "SecretProviderMetadataDependencyValidated=true."),
        new("No productive persistence", true, "EfRuntimeEnabled=false and ProductivePersistenceEnabled=false.")
    ];

    public IReadOnlyCollection<CrmCommonDbRuntimeConnectivityTrialBlockedItemContract> GetBlockedItems() =>
    [
        new("Default Common DB connectivity", "Explicit NonProduction flag is disabled.", NextGate),
        new("Production DB connectivity", "Production remains blocked.", NextGate),
        new("Connection string materialization", "Values cannot be exposed, logged, persisted or cached.", NextGate),
        new("Schema, migrations and EF", "Separate approval is required before any productive persistence activation.", NextGate)
    ];
}
