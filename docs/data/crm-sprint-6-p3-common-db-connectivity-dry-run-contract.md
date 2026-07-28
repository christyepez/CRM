# CRM Sprint 6 P3 - Common DB Connectivity Dry-Run Contract

Status: contract exists, dry-run disabled, no database connection attempted.

This sprint defines the shape of a future Common DB connectivity dry-run using only Secret Provider safe mock metadata. It uses the synthetic reference `mock://crm/common-db` and does not resolve a real connection string.

Default decisions:

- CommonDbConnectivityDryRunContractExists: true
- CommonDbDryRunApprovalGranted: false
- CommonDbDryRunEnabled: false
- CommonDbConnectionAttempted: false
- UsesSecretProviderSafeMockMetadata: true
- UsesSyntheticConnectionReference: true
- SyntheticConnectionReference: `mock://crm/common-db`
- RealConnectionStringUsed: false
- ConnectionStringResolved: false
- SqlConnectionCreated: false
- DbConnectionCreated: false
- EfRuntimeEnabled: false
- MigrationsCreated: false
- ApiRequiresDatabase: false
- NonProductionOnly: true
- RollbackRequired: true
- ObservabilityRequired: true
- NextGate: Sprint6P4PortalAuthTokenPropagationDryRunContract

Warning: `Common DB connectivity dry-run contract only; no database connection is attempted`.
