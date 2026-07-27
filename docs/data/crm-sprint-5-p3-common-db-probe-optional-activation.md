# CRM Sprint 5 P3 - Common DB Probe Optional Activation

Status: `CommonDbProbeOptionalActivation`.

Sprint 5 P3 prepares optional Common DB probe activation for future non-production use only. The probe exists, but activation is not approved and no database connection is attempted.

Default decision:

- `commonDbProbeOptionalActivationExists=true`.
- `commonDbProbeActivationApproved=false`.
- `commonDbProbeEnabled=false`.
- `commonDbConnectionAttempted=false`.
- `secretProviderRuntimeRequired=true`.
- `secretProviderRuntimeConnected=false`.
- `secretReadsRequiredBeforeActivation=true`.
- `secretReadsEnabled=false`.
- `realDatabaseConfigured=false`.
- `connectionStringsConfigured=false`.
- `efRuntimeEnabled=false`.
- `migrationsCreated=false`.
- `durablePersistenceEnabled=false`.
- `apiRequiresDatabase=false`.
- `nonProductionOnly=true`.
- `syntheticDataRequired=true`.
- `rollbackRequired=true`.

Next gate: `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction`.

Warning: `Common DB probe optional activation only; no database connection is attempted`.
