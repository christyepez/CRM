# CRM Corporativo

## Sprint 1 P4 - Controlled Persistence and Read Model Design

P4 defines future persistence/read-model architecture without productive activation. It adds Application ports, read model contracts and foundation mock read-model endpoints. It still does not add DB, migrations, productive CRUD, DELETE, Auth, token storage or runtime Portal/Financiero integration.

Read model preview endpoints:

- `GET /api/crm/foundation/leads/read-model-preview`
- `GET /api/crm/foundation/accounts/read-model-preview`
- `GET /api/crm/foundation/contacts/read-model-preview`
- `GET /api/crm/foundation/read-model-status`

Every read-model preview response includes `source=FoundationMock`, `persistence=None` and `warning=Read model preview only, not persisted`.

## Sprint 1 P3 - Leads, Accounts and Contacts Foundation

P3 adds foundation-only rules and preview endpoints for Leads, Accounts and Contacts. These endpoints validate contracts and business rules, but do not persist, do not integrate, and are not productive CRM APIs.

Preview endpoints:

- `POST /api/crm/foundation/leads/preview`
- `POST /api/crm/foundation/accounts/preview`
- `POST /api/crm/foundation/contacts/preview`

Every preview response includes `foundationMode=true`, `persistence=None`, `runtimeMode=NonProduction` and `warning=Preview only, not persisted`.

## Sprint 1 P2 - Core Domain Discovery and API Contract Baseline

P2 adds a draft CRM domain model and read-only contract endpoints. It remains non-production and does not add persistence, migrations, CRM CRUD, login, Identity, token storage or real Portal/Financiero integration.

Additional endpoints:

- `GET /api/crm/domain-catalog`
- `GET /api/crm/contracts`
- `GET /api/crm/integration-boundaries`

Domain concepts now documented and represented in pure domain code: Lead, Account, Contact, Opportunity, Pipeline, PipelineStage, Activity, Note, Campaign and Segment.

## Sprint 1 P1 - Foundation Architecture Baseline

CRM starts as an independent repository foundation using .NET 8, Angular and Docker Compose. The current runtime is non-production and foundation-only.

### Local execution

```powershell
dotnet restore CRM.sln
dotnet build CRM.sln
dotnet test CRM.sln
docker compose config
docker compose up -d --build
Invoke-WebRequest -UseBasicParsing http://localhost:8093/health
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/readiness
```

Frontend:

```powershell
cd frontend/crm-web
pnpm install --frozen-lockfile
pnpm run build
pnpm test
```

### Foundation endpoints

- `GET /health`
- `GET /health/live`
- `GET /health/ready`
- `GET /api/crm/readiness`

Readiness returns `module=CRM`, `status=ReadyForFoundationOnly`, `portalIntegration=Planned`, `financialIntegration=Planned` and `runtimeMode=NonProduction`.

### Guardrails

- No login, Identity, token storage or roles propios.
- No SQL Server propio in Docker Compose.
- No CRM CRUD endpoints yet.
- No hardcoded integration with Financiero.
- Portal Auth/Menu/permissions/Audit/Notification/Configuration remain planned reuse points.

Módulo CRM corporativo integrado a `PortalCorporativo`.

## Objetivo

Implementar capacidades de gestión comercial y relación con clientes reutilizando las APIs transversales del portal y evitando duplicación de seguridad, auditoría, notificaciones, menús, configuración visual, catálogos y reporting.

## Repos relacionados

```text
PortalCorporativo: https://github.com/christyepez/PortalCorporativo
CRM: https://github.com/christyepez/CRM
CodexCommonAgents: https://github.com/christyepez/CodexCommonAgents
```

## Capacidades propias CRM

Este repositorio puede crear componentes propios para:

- Customers.
- Contacts.
- Leads.
- Opportunities.
- Activities.
- Cases.
- Campaigns.
- Interactions.
- CRM Integration Hub.
- Salesforce/Dynamics/Generic REST connectors.
- Mapeos y transacciones de integración.

## Capacidades reutilizadas del portal

CRM debe reutilizar o extender:

- Security API.
- Menu API.
- Configuration API.
- Catalog API.
- Audit API.
- Notification API.
- Content/File API.
- Reporting API.
- Integration API base.
- Portal Angular Shell.
- API Gateway.

## Reglas Codex

Codex debe leer primero:

1. `AGENTS.md`.
2. `codex/COORDINADOR_SOLUCION.md`.
3. `codex/INSTRUCTIONS.md`.
4. `codex/ARCHITECTURE_RULES.md`.
5. `codex/PORTAL_INTEGRATION_CONTRACTS.md`.
6. `codex/TASKS.md`.

## Clasificación obligatoria

Toda tarea debe clasificar sus componentes como:

```text
REUSE   = usar componente del portal.
EXTEND  = extender configuración, permisos, catálogos, menús o contratos del portal.
ADAPT   = crear adaptador hacia API/servicio del portal.
CREATE  = crear componente propio del dominio CRM.
BLOCKED = no implementar hasta revisar portal.
```

## Principio de integración

CRM Core no debe acoplarse directamente a Salesforce, Dynamics u otro CRM externo. Toda integración externa debe pasar por `CRM Integration Hub`.

## Modo bajo consumo de tokens

No leer todo el repositorio si la tarea no lo requiere. Usar `AGENTS.md`, `codex/PORTAL_INTEGRATION_CONTRACTS.md`, `codex/TASKS.md` y el playbook aplicable de `CodexCommonAgents`.
# CRM Sprint 1 P5 Portal Adapter Contracts

CRM exposes foundation-only Portal integration readiness endpoints under `/api/crm/foundation/portal-integration/...`.

Current status:

- Integration mode: Planned
- Runtime mode: NonProduction
- Connected: false
- Capability owner: PortalCorporativo

CRM does not duplicate Portal security, menu, permissions, audit, notification, configuration or gateway capabilities.

# CRM Sprint 1 P6 Financial Adapter Contracts

CRM exposes foundation-only Financial integration readiness endpoints under `/api/crm/foundation/financial-integration/...`.

Current status:

- Integration mode: Planned
- Runtime mode: NonProduction
- Connected: false
- Capability owner: Financiero
- Integration patterns: API + Events + NoSharedDatabase

CRM does not create invoices, collections, SRI, ATS, RIDE, XAdES, shared databases, FinancieroDb queries or runtime Financiero calls.

# CRM Sprint 1 P7 Reporting/BI Contract Foundation

CRM exposes foundation-only Reporting readiness endpoints under `/api/crm/foundation/reporting/...`.

Current status:

- Analytics mode: Planned
- Runtime mode: NonProduction
- Connected: false
- Source: FoundationMock
- Power BI Embed: NotConfigured

CRM does not implement real dashboards, Power BI embedding, datasets, ETL, workspace IDs, report IDs, embed tokens, SQL queries or productive analytics endpoints.

# CRM Sprint 1 P8 Foundation Closure

Sprint 1 is closed as foundation only:

- Sprint 1 Foundation: Closed
- Runtime: NonProduction
- Persistence: None
- Portal/Financial/Reporting: Planned
- Productization: NotReady
- Next Gate: Sprint2Planning

The closure endpoint is `/api/crm/foundation/sprint-1/closure-status`.

# CRM Sprint 2 P1 Controlled Persistence Design Review

CRM exposes foundation-only persistence readiness at `/api/crm/foundation/persistence/readiness`.

Current persistence status:

- Persistence Design Review: Active
- Persistence Mode: DesignOnly
- Database Configured: false
- Migration Ready: false
- Next Gate: Sprint2P2PersistenceSeam

No database, migration, DbContext, DbSet, SQL Server or productive CRUD is active.
### CRM Sprint 2 P2 - Non-production persistence seam

CRM now exposes a foundation-only `NonProductionSeam` for Lead, Account and Contact previews. It uses in-memory adapters and keeps `DatabaseConfigured=false`, `DbContextConfigured=false`, `MigrationReady=false`, `DurablePersistence=false` and `ProductiveCrudEnabled=false`.

Useful local checks:

- `GET /api/crm/foundation/persistence/seam-status`
- `GET /api/crm/foundation/persistence/feature-flags`
- `GET /api/crm/foundation/persistence/stores/status`
- `POST /api/crm/foundation/persistence/stores/clear-preview`

No productive CRM CRUD endpoints are active in this sprint.
### CRM Sprint 2 P3 - Portal authorization simulation

CRM now exposes a foundation-only Portal authorization simulation. It keeps `PortalRuntimeConnected=false`, `AuthOwnedBy=PortalCorporativo`, `CrmOwnsAuth=false`, `TokenStorage=false` and `ProductiveAuthorization=false`.

Useful local checks:

- `GET /api/crm/foundation/portal-authorization/simulation-status`
- `GET /api/crm/foundation/portal-authorization/scenarios`
- `GET /api/crm/foundation/portal-authorization/permissions`
- `GET /api/crm/foundation/portal-authorization/sample-user-context`
- `POST /api/crm/foundation/portal-authorization/check-permission`

No login, productive Auth, Portal runtime call, token storage, menu runtime or CRUD UI is active.

### CRM Sprint 2 P4 - Controlled foundation CRUD

CRM now exposes foundation-only GET/POST/PUT preview endpoints for Lead, Account and Contact under `/api/crm/foundation/...`.

Current state:

- Foundation CRUD: Enabled
- Productive CRUD: false
- Durable Persistence: false
- Database Configured: false
- Authorization Mode: FoundationSimulation
- Next Gate: Sprint2P5IntegrationReadinessReview

No productive CRM routes, DELETE endpoints, DB, EF, migrations, real Auth or Portal runtime are active.

### CRM Sprint 2 P5 - Integration readiness review

CRM now exposes `GET /api/crm/foundation/sprint-2/integration-readiness`.

Current P5 decision:

- Sprint 2 P5 Readiness Review: Active
- Database Ready: false
- Auth Ready: false
- Productive CRUD Ready: false
- Productization Status: NotReady
- Recommended Decision: ContinueReview
- Next Gate: Sprint2P6ProductizationGateDecision

No DB real, Auth real, Portal runtime or productive endpoint activation occurs in P5.
