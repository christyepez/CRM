# CRM Portal Security Boundary

PortalCorporativo owns authentication, authorization, permission metadata, menu visibility, audit, notification and shared configuration.

CRM must not store credentials, implement login, issue JWTs, persist roles or manage permission catalogs. CRM may declare future ports that consume Portal context once approved adapters exist.

P5 exposes only foundation status endpoints under `/api/crm/foundation/portal-integration/...`. They are NonProduction readiness endpoints and return `connected=false`.

## Risks

- Duplicating Portal permissions in CRM would split ownership.
- Hardcoded Portal URLs would make environments unsafe.
- Runtime calls before Portal contracts are stable could create brittle coupling.
