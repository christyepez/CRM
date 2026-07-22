# CRM Sprint 1 P1 Release Notes

Date: 2026-07-20
Phase: Repository Foundation and Architecture Baseline

## P2 - Core Domain Discovery and API Contract Baseline

- Added pure CRM domain concepts, value objects, statuses and conceptual domain events.
- Added draft application contract catalog.
- Added read-only API contract endpoints for domain catalog, contracts and integration boundaries.
- Added docs for domain model, business rules, API contracts and Portal/Financiero boundaries.
- Added unit and architecture tests for domain behavior and guardrails.
- Still no DB, migrations, CRUD, Identity, token storage or real integrations.

## P3 - Leads, Accounts and Contacts Foundation

- Strengthened Lead, Account and Contact domain rules.
- Added foundation-only Application preview services.
- Added preview endpoints under `/api/crm/foundation/.../preview`.
- Added domain and Application tests for foundation behavior.
- Added documentation for Leads, Accounts, Contacts and preview API.
- Still no DB, migrations, productive CRUD, DELETE, Identity, token storage or real integrations.

## P4 - Controlled Persistence and Read Model Design

- Added future persistence strategy and read model documentation.
- Added Application persistence ports as conceptual interfaces only.
- Added read model contracts and foundation mock read model services.
- Added GET read-model preview endpoints under `/api/crm/foundation/...`.
- Added Infrastructure placeholder guardrail exception.
- Still no DB, migrations, productive CRUD, DELETE, Identity, token storage or real integrations.

## Summary

CRM repository foundation starts with .NET 8, Angular, Docker Compose and architecture guardrails.

## Added

- `CRM.sln`.
- Clean Architecture projects.
- Health/readiness API endpoints.
- Angular foundation shell.
- Docker Compose without SQL Server.
- Verification tools.
- Architecture, security and roadmap documentation.

## Not added

- CRM CRUD.
- CRM database/migrations.
- Login, Identity, roles, token storage.
- Gateway or Portal Shell.
- Real Financiero integration.
- Production activation.
# Sprint 1 P5 - Portal Adapter Contracts

- Added CRM Application ports for future PortalCorporativo adapters.
- Added safe DTO contracts for Portal user, tenant, permission, menu, audit, notification, configuration and correlation context.
- Added NonProduction placeholder in Infrastructure without HTTP clients or URLs.
- Added foundation-only Portal integration readiness endpoints.
- Added guardrail tests and verification checks to prevent Portal capability duplication.

# Sprint 1 P6 - Financial Adapter Contracts

- Added CRM Application ports for future Financiero adapters.
- Added safe DTO contracts for financial customer reference, account status, invoice awareness, payment status, collections signal and conceptual financial events.
- Added NonProduction placeholder in Infrastructure without HTTP clients, URLs, connection strings or DB access.
- Added foundation-only Financial integration readiness endpoints.
- Added guardrail tests and verification checks to prevent direct coupling or shared database usage.

# Sprint 1 P7 - Reporting and BI Contract Foundation

- Added CRM Application ports for future reporting and BI adapters.
- Added KPI, dashboard, analytics read model and metric definition contracts.
- Added NonProduction reporting placeholder without Power BI runtime, IDs, URLs or tokens.
- Added foundation-only Reporting readiness endpoints.
- Added documentation and guardrail tests to prevent productive analytics activation.

# Sprint 1 P8 - Foundation Closure

- Added formal Sprint 1 closure documentation and integrated evidence.
- Added integrated capability matrix, ownership boundaries and dependency map.
- Added endpoint inventory, guardrail register and sensitive data policy.
- Added Sprint 2 options, recommended path and productization gates.
- Added foundation-only closure status endpoint.

# Sprint 2 P1 - Controlled Persistence Design Review

- Added design-only persistence readiness contracts and endpoint.
- Added persistence design review documentation and activation gates.
- Kept database, migrations, DbContext, DbSet and productive CRUD disabled.
## Sprint 2 P2 note

Sprint 1 remains closed. P2 adds only a non-production seam for preview persistence and does not reopen Sprint 1 scope.
## Sprint 2 P3 note

Sprint 1 remains closed. P3 adds only Portal authorization simulation and does not reopen Sprint 1 scope or activate productive security.

P4 adds controlled foundation CRUD previews and does not reopen Sprint 1 scope or activate productive CRM CRUD.

P5 adds integration readiness review and does not activate DB, Auth or productive CRUD.

P6 closes Sprint 2 with `OverallDecision=NoGoForProductiveActivation`, keeps `ProductizationStatus=NotReady`, and approves Sprint 3 planning only.
