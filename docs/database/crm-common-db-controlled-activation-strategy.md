# CRM Common DB Controlled Activation Strategy

CRM will use the shared local SQL Server container owned by the platform environment, but CRM owns its own logical database and schema boundary.

## Target model

- One SQL Server container per environment.
- CRM logical database: `CrmDb`.
- Portal logical database: separate and not queried directly by CRM.
- No shared tables across Portal and CRM.
- No cross-domain migrations.

## Activation stages

1. Preparation only: document boundary, prerequisites, rollback and validation.
2. Contract alignment: wait for Portal Sprint 21 consumer contract.
3. Explicit NonProduction approval: resolve logical secret metadata without exposing values.
4. Connectivity probe: metadata-only, fail-closed, no schema changes.
5. Persistence pilot: separate future gate.

## Required markers

- CommonDbStrategyPrepared: true.
- CommonDbRuntimeEnabled: false.
- RealCommonDbConnectionConfigured: false.
- RealConnectionStringsPresent: false.
- SharedPortalTablesAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
