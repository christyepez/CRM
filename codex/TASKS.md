# TASKS.md

## CRM Sprint 1 P2 - Core Domain Discovery and API Contract Baseline

- [x] Validate GitHub main contains P1 commit.
- [x] Create pure CRM domain baseline.
- [x] Create application contract catalog.
- [x] Add non-mutating API contract endpoints.
- [x] Document domain, API and integration boundaries.
- [x] Add unit and architecture tests.
- [x] Keep DB/migrations/auth/token storage out of scope.

## CRM Sprint 1 P3 - Leads, Accounts and Contacts Foundation

- [x] Validate GitHub main contains P2 commit.
- [x] Strengthen Lead, Account and Contact domain rules.
- [x] Add foundation-only Application preview services.
- [x] Add preview endpoints under `/api/crm/foundation/.../preview`.
- [x] Document foundation rules and preview API.
- [x] Add unit and architecture tests.
- [x] Keep persistence, productive CRUD, DELETE, Auth, token storage and integrations out of scope.

## CRM Sprint 1 P4 - Controlled Persistence and Read Model Design

- [x] Validate GitHub main contains P3 commit.
- [x] Document persistence strategy and data ownership.
- [x] Add conceptual Application persistence ports.
- [x] Add read model contracts and foundation mock services.
- [x] Add GET read-model preview endpoints under `/api/crm/foundation/...`.
- [x] Keep DB, migrations, productive CRUD, DELETE, Auth, token storage and integrations out of scope.

## CRM Sprint 1 P1 - Repository Foundation and Architecture Baseline

- [x] Confirmar GitHub `main` como fuente principal.
- [x] Crear rama `crm-sprint-1-p1-foundation-architecture-baseline`.
- [x] Crear `CRM.sln`.
- [x] Crear proyectos `CRM.Api`, `CRM.Application`, `CRM.Domain`, `CRM.Infrastructure`.
- [x] Crear tests `CRM.UnitTests` y `CRM.ArchitectureTests`.
- [x] Crear API foundation con `/health`, `/health/live`, `/health/ready`, `/api/crm/readiness`.
- [x] Crear frontend Angular foundation en `frontend/crm-web`.
- [x] Crear Docker foundation sin SQL Server propio.
- [x] Crear verificadores foundation.
- [x] Crear documentación architecture/roadmap/release.
- [x] Mantener runtime `NonProduction` y `ReadyForFoundationOnly`.
- [x] No crear CRUD CRM, Identity propio, token storage, Gateway propio, Shell propio, DB/migrations CRM ni integración real con Financiero.

## Propósito

Backlog inicial para que Codex implemente CRM por fases, agentes y estrategia Portal-First.

## Regla de ejecución

Antes de iniciar cualquier fase técnica, Codex debe clasificar cada componente como:

```text
REUSE
EXTEND
ADAPT
CREATE
BLOCKED
```

## Fase 0 - Coordinación y bajo consumo de tokens

- Validar `AGENTS.md`.
- Validar `codex/COORDINADOR_SOLUCION.md`.
- Validar `codex/PORTAL_INTEGRATION_CONTRACTS.md`.
- Revisar `PortalCorporativo/codex/REUSABLE_CAPABILITIES.md` si existe.
- Revisar `CodexCommonAgents/registry/reusable-portal-apis.md` cuando esté disponible.
- Actualizar `docs/coordination/dependencies.md`.
- No leer todo el repo si la tarea no lo requiere.

## Fase 1 - Contratos con PortalCorporativo

- Validar APIs reales del portal.
- Crear clientes/adapters hacia servicios del portal.
- Crear contratos para seguridad, permisos, menú, configuración, catálogos, auditoría, notificaciones, documentos, reporting e integración.
- Registrar pendientes en `docs/coordination/open-issues.md`.

## Fase 2 - Backend CRM Core

- Crear estructura backend CRM.
- Implementar Customers.
- Implementar Contacts.
- Implementar Leads.
- Implementar Lead Conversion.
- Implementar Opportunities.
- Implementar Activities.
- Implementar Cases.
- Implementar Campaigns.
- Integrar permisos del portal.
- Integrar auditoría del portal.
- Integrar notificaciones del portal.

## Fase 3 - Base de datos CRM

- Crear modelo CRM.
- Crear scripts SQL.
- Crear seed inicial CRM.
- Crear modelo Integration Hub.
- Evitar dependencia directa con bases de datos del portal.

## Fase 4 - Frontend CRM

- Crear módulo Angular CRM integrado al shell del portal.
- Crear rutas CRM.
- Crear pantallas de clientes, leads, oportunidades y casos.
- Usar menú, tema, permisos, grids y formularios del portal.
- No quemar colores, logos, menús, botones ni layouts.

## Fase 5 - CRM Integration Hub

- Crear ExternalSystem.
- Crear IntegrationTransaction.
- Crear EntityMapping.
- Crear FieldMapping.
- Crear ExternalEntityReference.
- Crear Outbox e Inbox CRM.
- Crear GenericRestConnector.
- Crear stubs Salesforce y Dynamics.
- No acoplar CRM Core directamente a Salesforce/Dynamics.

## Fase 6 - Workers CRM

- Procesar Outbox.
- Procesar Inbox.
- Procesar reintentos.
- Procesar integraciones.
- Publicar eventos auditables y notificables.

## Fase 7 - Docker y ejecución conjunta

- Crear compose CRM.
- Documentar ejecución junto al portal.
- Validar variables de entorno.
- No guardar secretos en código.

## Fase 8 - QA

- Crear pruebas de dominio.
- Crear pruebas API.
- Crear pruebas de contrato contra portal.
- Crear pruebas Integration Hub.
- Validar clasificación REUSE/EXTEND/ADAPT/CREATE/BLOCKED.

## Salida obligatoria por tarea

```text
Agent:
Task:
Portal Capability Checked:
Reuse Classification:
Files Created:
Files Modified:
Tests Added:
Risks:
Next Step:
```
# CRM Sprint 1 P5

Status: implemented in branch `crm-sprint-1-p5-portal-adapter-contracts`.

Scope:

- Portal adapter ports in Application.
- Portal integration contracts in Application.
- NonProduction placeholder in Infrastructure.
- Foundation-only Portal integration status endpoints.
- Documentation and guardrail tests.

Out of scope:

- Runtime Portal calls.
- CRM-owned login, identity, menu, permissions, audit, notification, configuration, gateway.
- Database, migrations, DbContext, SQL Server.

# CRM Sprint 1 P8

Status: implemented in branch `crm-sprint-1-p8-foundation-closure-readiness-roadmap`.

Scope:

- Sprint 1 closure documentation.
- Integrated evidence and capability matrix.
- Endpoint inventory and guardrail register.
- Sprint 2 roadmap and productization gates.
- Foundation-only closure status endpoint.

Out of scope:

- Productive activation.
- Runtime Portal/Financiero/BI integrations.
- DB, migrations, DbContext, SQL Server.
- Login, token storage, DELETE or productive CRM APIs.

# CRM Sprint 2 P1

Status: implemented in branch `crm-sprint-2-p1-controlled-persistence-design-review`.

Scope:

- Persistence design review documentation.
- Logical data model contracts.
- Foundation-only persistence readiness endpoint.
- GO/NO-GO gates for Sprint 2 P2.

Out of scope:

- Database activation.
- EF migrations, DbContext, DbSet.
- SQL Server owned by CRM.
- Productive CRUD or DELETE endpoints.

# CRM Sprint 1 P6

Status: implemented in branch `crm-sprint-1-p6-financial-adapter-contracts`.

Scope:

- Financial adapter ports in Application.
- Financial integration contracts in Application.
- Conceptual CRM/Financiero events.
- NonProduction placeholder in Infrastructure.
- Foundation-only Financial integration status endpoints.
- Documentation and guardrail tests.

Out of scope:

- Runtime Financiero calls.
- Direct references to Financiero projects or assemblies.
- Shared database, FinancieroDb queries, migrations, DbContext, SQL Server.
- Invoices, collections, SRI, ATS, RIDE, XAdES.

# CRM Sprint 1 P7

Status: implemented in branch `crm-sprint-1-p7-reporting-bi-contract-foundation`.

Scope:

- Reporting/BI adapter ports in Application.
- KPI, dashboard, analytics read model and metric definition contracts.
- NonProduction reporting placeholder in Infrastructure.
- Foundation-only Reporting status endpoints.
- Documentation and guardrail tests.

Out of scope:

- Power BI runtime, embed tokens, report IDs, workspace IDs, dataset IDs.
- Real dashboards, real datasets, ETL, SQL queries.
- Database, migrations, DbContext, SQL Server.
## CRM Sprint 2 P2 - Non-production persistence seam

Status: implemented for PR review.

- Added Application foundation store ports.
- Added Infrastructure in-memory foundation stores.
- Added seam status, feature flags, stores status and clear-preview foundation endpoints.
- Kept DB, EF, migrations, SQL Server and productive CRUD disabled.
- Next: Sprint2P3PortalAuthorizationAdapterSimulation.
## CRM Sprint 2 P3 - Portal authorization simulation

Status: implemented for PR review.

- Added Portal authorization simulation contracts and service.
- Added Infrastructure simulated Portal user, permission and scenario providers.
- Added foundation permission guard.
- Added foundation-only Portal authorization simulation endpoints.
- Clear-preview now returns simulated permission result for `crm.foundation.preview.clear`.
- Kept login, productive Auth, Portal runtime, token storage, DB and productive CRUD disabled.
- Next: Sprint2P4ControlledCrudBehindFoundationFlag.

## CRM Sprint 2 P4 - Controlled foundation CRUD

Status: implemented for PR review.

- Added foundation CRUD contracts for Lead, Account and Contact.
- Added foundation CRUD use cases and status service.
- Added GET/POST/PUT endpoints under `/api/crm/foundation/...`.
- Extended in-memory foundation stores with lookup by preview id.
- Kept productive CRUD, DELETE, DB, EF, migrations, real Auth and Portal runtime disabled.
- Next: Sprint2P5IntegrationReadinessReview.

## CRM Sprint 2 P5 - Integration readiness review

Status: implemented for PR review.

- Added P5 readiness review documents, evidence, GO/NO-GO, risk register and decision record.
- Added activation gate matrix and DB/Auth/CRUD readiness map.
- Added productization readiness contracts and service.
- Added `GET /api/crm/foundation/sprint-2/integration-readiness`.
- Recommendation: ContinueReview; DB/Auth/productive CRUD remain NO-GO.
- Next: Sprint2P6ProductizationGateDecision.
