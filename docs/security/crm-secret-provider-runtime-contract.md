# CRM Secret Provider Runtime Contract

The Secret Provider is required before any future non-production runtime probe can read configuration needed by CRM. In P2 the contract exists, but no runtime provider is connected and no secret value is read.

Allowed logical names only:

| Logical name | Purpose | Value in repo |
| --- | --- | --- |
| `CRM_COMMON_DB_CONNECTION` | Future common database lookup. | Never |
| `CRM_PORTAL_AUTH_BASE_URL` | Future Portal Auth endpoint lookup. | Never |
| `CRM_PORTAL_AUTH_CLIENT_ID` | Future Portal Auth client identifier lookup. | Never |
| `CRM_PORTAL_AUTH_CLIENT_SECRET` | Future Portal Auth client secret lookup. | Never |
| `CRM_OBSERVABILITY_ENDPOINT` | Future observability endpoint lookup. | Never |

Before any future read, CRM requires approved provider ownership, least privilege, masking, no secret logging, rotation policy, rollback, synthetic data and non-production-only scope.
