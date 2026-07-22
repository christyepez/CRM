# CRM Persistence Feature Flags

P2 hardcodes safe non-production flags through `ICrmPersistenceFeatureFlagProvider`:

| Flag | Value | Meaning |
| --- | --- | --- |
| `CRM_FOUNDATION_MODE` | `true` | Foundation endpoints may run. |
| `CRM_PERSISTENCE_SEAM_ENABLED` | `true` | In-memory seam is active. |
| `CRM_PERSISTENCE_ENABLED` | `false` | Productive persistence is disabled. |
| `CRM_PRODUCTIVE_CRUD_ENABLED` | `false` | Productive CRM CRUD is disabled. |
| `CRM_DURABLE_PERSISTENCE_ENABLED` | `false` | Durable persistence is disabled. |

Warning returned by the API: `Non-production persistence seam only; no database configured`.

Plain markers for automated verification: `CRM_PERSISTENCE_SEAM_ENABLED=true`, `CRM_PRODUCTIVE_CRUD_ENABLED=false`, `CRM_DURABLE_PERSISTENCE_ENABLED=false`.

P4 allows foundation CRUD previews while keeping productive CRUD disabled.
