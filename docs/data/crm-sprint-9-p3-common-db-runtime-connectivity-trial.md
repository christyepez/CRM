# CRM Sprint 9 P3 Common DB Runtime Connectivity Trial

Sprint 9 P3 adds a controlled Common DB runtime connectivity trial for NonProduction only. It is disabled by default behind `Crm:RuntimeTrials:CommonDbConnectivityEnabled=false`.

The trial can consume only sanitized metadata from the Sprint 9 P2 Secret Provider boundary. It must not materialize, return, log, persist or cache connection strings.

Default status:

- `CommonDbRuntimeConnectivityTrialExists=true`
- `CommonDbRuntimeConnectivityTrialApproved=true`
- `CommonDbRuntimeConnectivityTrialEnabled=false`
- `CommonDbConnectionAttempted=false`
- `CommonDbConnected=false`
- `CommonDbConnectionStringResolved=false`
- `SecretProviderMetadataDependencyValidated=true`
- `SchemaCreated=false`
- `MigrationExecuted=false`
- `EfRuntimeEnabled=false`
- `ProductivePersistenceEnabled=false`
- `NextGate=Sprint9P4PortalAuthRuntimeValidationTrial`
