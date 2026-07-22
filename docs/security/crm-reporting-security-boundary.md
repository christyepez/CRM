# CRM Reporting Security Boundary

Reporting authorization must be delegated to Portal-owned security context. CRM does not create login, identity, token storage or report authorization catalogs.

CRM P7 exposes only foundation readiness endpoints under `/api/crm/foundation/reporting/...`.

## Not implemented

- Power BI SDK runtime
- Embed tokens
- Real datasets
- Workspace IDs
- Report IDs
- Dataset IDs
- Embedded dashboards
- Productive analytics APIs
