# CRM Financial Security Boundary

Financiero owns financial records, invoice facts, payment facts, tax processes and SRI-related workflows. CRM may consume future summaries only through approved API/event contracts.

CRM must not store financial credentials, query FinancieroDb, share tables, hold SRI artifacts, issue financial documents or create collection processes.

P6 exposes only foundation status endpoints under `/api/crm/foundation/financial-integration/...`. They are NonProduction readiness endpoints and return `connected=false`.

## Risks

- Shared database access would break domain ownership.
- Hardcoded Financiero URLs would make environments unsafe.
- Runtime calls before contracts are stable could create brittle coupling.
