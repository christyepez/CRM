# TASKS.md

## CRM Sprint 7 P1 - Secret Provider Real NonProduction Approval

- [x] Validate GitHub main contains Sprint 6 P6 commit.
- [x] Create Secret Provider real NonProduction approval docs, policy, boundary, approved logical names, runbook, rollback and architecture review.
- [x] Add `CrmSecretProviderRealNonProductionApprovalStatusService`.
- [x] Add contract-only Infrastructure placeholder.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval`.
- [x] Keep approval granted false.
- [x] Keep runtime enabled/connected false.
- [x] Keep real secret read attempted false.
- [x] Keep secret store runtime client, secret SDK runtime, `.env`, environment secret reads and secret logging disabled.
- [x] Keep DB/Auth/Portal runtime, productive routes, locked stubs runtime, DELETE, login, Identity and productive UI disabled.
- [x] Next Gate: `Sprint7P2SecretProviderRealNonProductionRuntimeProbe`.

## CRM Sprint 6 P6 - Sprint 6 Gate Decision

- [x] Validate GitHub main contains Sprint 6 P5 commit.
- [x] Create Sprint 6 closure and gate decision documentation.
- [x] Create Sprint 6 gate matrix and security/data/API/E2E reviews.
- [x] Create Sprint 7 options, recommended path and gates.
- [x] Add `CrmSprint6GateDecisionStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-6/gate-decision`.
- [x] Keep `OverallDecision=GoForSprint7ControlledNonProductionActivationPlanning`.
- [x] Keep `RealActivationDecision=NoGo`, `ProductizationStatus=NotReady`, productive routes, productive CRUD, DELETE and productive UI as NoGo.
- [x] Next Gate: `Sprint7P1SecretProviderRealNonProductionApproval`.
- [x] Do not activate real secrets, DB, Portal Auth runtime, token/header reads, productive routes, locked stubs runtime, DELETE, login, Identity or productive UI.

## CRM Sprint 6 P3 - Common DB Connectivity Dry-Run Contract

- [x] Validate GitHub main contains Sprint 6 P2 commit.
- [x] Create Common DB dry-run documentation, policy, contract, observability, runbook and secret boundary.
- [x] Add `CrmCommonDbConnectivityDryRunStatusService`.
- [x] Add contract-only `CommonDbConnectivityDryRun` placeholder.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-6/common-db-connectivity-dry-run`.
- [x] Use only `mock://crm/common-db` synthetic metadata.
- [x] Keep `commonDbDryRunApprovalGranted=false`, `commonDbDryRunEnabled=false`, `commonDbConnectionAttempted=false`.
- [x] Keep real connection string resolution, DB connection, EF runtime, migrations, SQL Server compose, secrets/env reads, Portal Auth runtime, productive routes and DELETE disabled.
- [x] Next Gate: `Sprint6P4PortalAuthTokenPropagationDryRunContract`.

## CRM Sprint 6 P2 - Secret Provider Safe Mock Activation

- [x] Validate GitHub main contains Sprint 6 P1 commit.
- [x] Create safe mock documentation, policy, contract, synthetic values and runbook.
- [x] Add `CrmSecretProviderSafeMockActivationStatusService`.
- [x] Add deterministic `SecretProviderSafeMock` in Infrastructure.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation`.
- [x] Keep safe mock enabled only for synthetic values.
- [x] Keep `secretProviderRuntimeConnected=false`, `secretProviderReadsRealSecrets=false`, `realSecretsConfigured=false`, `envFileRequired=false`, `keyVaultClientConfigured=false`, `azureSdkForSecretsConfigured=false`.
- [x] Keep DB/Auth/Portal runtime, productive routes, locked stubs runtime, DELETE, login, Identity and productive UI disabled.
- [x] Next Gate: `Sprint6P3CommonDbConnectivityDryRunContract`.

## CRM Sprint 6 P1 - NonProduction Runtime Approval Package

- [x] Validate GitHub main contains Sprint 5 P6 commit.
- [x] Create non-production runtime approval package documentation.
- [x] Create approval matrix, entry/exit criteria, rollback, security and architecture approval docs.
- [x] Add `CrmNonProductionRuntimeApprovalPackageStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package`.
- [x] Keep `nonProductionRuntimeApprovalPackageExists=true`.
- [x] Keep all approvals false: non-production runtime, secret provider mock, common DB dry-run, Portal Auth dry-run, locked stub runtime trial, real activation, productive routes and DELETE.
- [x] Require synthetic data, rollback, observability, security review and architecture review.
- [x] Next Gate: `Sprint6P2SecretProviderSafeMockActivation`.
- [x] Do not activate secrets, DB, Portal Auth runtime, token/header reads, productive routes, locked stubs runtime, DELETE, login, Identity or productive UI.

## CRM Sprint 5 P3 - Common DB Probe Optional Activation

- [x] Create Common DB probe optional activation documentation.
- [x] Add `CrmCommonDbProbeOptionalActivationStatusService`.
- [x] Add `CommonDbProbeOptionalActivationPlaceholder`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-5/common-db-probe-optional-activation`.
- [x] Keep `commonDbProbeActivationApproved=false`.
- [x] Keep `commonDbProbeEnabled=false`.
- [x] Keep `commonDbConnectionAttempted=false`.
- [x] Keep DB runtime, EF runtime, migrations, connection strings, SQL Server compose and secret reads disabled.
- [x] Next Gate: `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction`.

## CRM Sprint 5 P2 - Secret Provider Runtime Contract Validation

- [x] Create Secret Provider runtime contract documentation.
- [x] Add `CrmSecretProviderRuntimeContractStatusService`.
- [x] Add `SecretProviderRuntimeContractPlaceholder`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-5/secret-provider-runtime-contract`.
- [x] Keep `secretProviderRuntimeConnected=false`.
- [x] Keep `secretProviderReadsEnabled=false`.
- [x] Keep `secretReadAttemptedByRuntime=false`.
- [x] Keep `.env`, real secrets, Key Vault client, DB runtime and Portal Auth runtime disabled.
- [x] Next Gate: `Sprint5P3CommonDbProbeOptionalActivationInNonProduction`.

## CRM Sprint 5 P1 - Controlled Runtime Probe Activation Plan

- [x] Validate GitHub main contains Sprint 4 P6 commit.
- [x] Create controlled runtime probe activation plan documentation.
- [x] Create approval matrix, checklist, rollback plan, observability plan and security policies.
- [x] Add `CrmControlledRuntimeProbeActivationPlanStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-5/runtime-probe-activation-plan`.
- [x] Keep all activation flags false: runtime probe, common DB, Portal Auth, productive routes and real activation.
- [x] Require non-production only, synthetic data, rollback, observability and secret provider validation.
- [x] Next Gate: `Sprint5P2SecretProviderRuntimeContractValidation`.
- [x] Do not activate DB/Auth/Portal runtime, productive routes, locked stubs, DELETE, secrets or productive UI.

## CRM Sprint 4 P6 - Sprint 4 Gate Decision

- [x] Validate GitHub main contains Sprint 4 P5 commit.
- [x] Create Sprint 4 closure and gate decision documentation.
- [x] Create Sprint 4 gate matrix and security/data/API/E2E reviews.
- [x] Create Sprint 5 options, recommended path and gates.
- [x] Add `CrmSprint4GateDecisionStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-4/gate-decision`.
- [x] Keep `OverallDecision=GoForNonProductionFoundationPilot`, `RealActivationDecision=NoGo`, `ProductizationStatus=NotReady`, `NonProductionE2EPilotDecision=GoFoundationOnly` and `Sprint5PlanningDecision=Go`.
- [x] Next Gate: `Sprint5P1ControlledRuntimeProbeActivationPlan`.
- [x] Do not activate DB/Auth/Portal runtime, productive routes, locked stubs, DELETE, secrets or productive UI.

## CRM Sprint 4 P5 - Non-Production E2E Pilot Readiness

- [x] Validate GitHub main contains Sprint 4 P4 commit.
- [x] Add non-production E2E pilot readiness documentation and scenario matrix.
- [x] Add `CrmNonProductionE2EPilotReadinessStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness`.
- [x] Add foundation-only E2E check script.
- [x] Keep `e2ePilotCanRun=true`, `e2ePilotScope=FoundationOnly`, `productiveRoutesUsed=false`, `realDatabaseUsed=false`, `portalAuthRuntimeUsed=false`, `durablePersistenceUsed=false`, `deleteOperationsUsed=false`, `syntheticDataOnly=true`, `foundationEndpointsOnly=true` and `negativeRouteValidationRequired=true`.
- [x] Next Gate: `Sprint4P6Sprint4GateDecision`.
- [x] Do not activate productive routes, DELETE, DB runtime, Auth runtime, Portal runtime, token reads, login/Identity, secrets or productive UI.

## CRM Sprint 4 P4 - Productive Routes Locked Stub Validation

- [x] Validate GitHub main contains Sprint 4 P3 commit.
- [x] Add productive routes locked stub validation documentation.
- [x] Select `DocumentOnlyPreferred` strategy.
- [x] Add `CrmProductiveRoutesLockedStubStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-4/productive-routes-locked-stub`.
- [x] Keep `productiveRoutesRegistered=false`, `lockedStubsRegistered=false`, `productiveCrudEnabled=false`, `productiveAuthorizationEnabled=false`, `deleteEndpointsEnabled=false`, `dbRequired=false`, `authRuntimeRequired=false` and `foundationCrudStillSeparate=true`.
- [x] Next Gate: `Sprint4P5NonProductionE2EPilotReadiness`.
- [x] Do not register productive route stubs, productive CRUD, DELETE, DB runtime, Auth runtime, Portal runtime, token reads, login/Identity or UI productiva.

## CRM Sprint 4 P3 - Portal Auth Runtime Probe Behind Disabled Flag

- [x] Validate GitHub main contains Sprint 4 P2 commit.
- [x] Add controlled Portal Auth runtime probe documentation.
- [x] Add `CrmPortalAuthRuntimeProbeStatusService`.
- [x] Add disabled Infrastructure placeholder without Portal HTTP, token reads, DB, file or network access.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-4/portal-auth-runtime-probe`.
- [x] Keep `portalAuthRuntimeProbeEnabled=false`, `portalRuntimeConnected=false`, `authRuntimeEnabled=false`, `productiveAuthorizationEnabled=false`, `tokenReadAttemptedByRuntime=false`, `portalHttpAttemptedByRuntime=false`, `loginImplementedByCrm=false`, `identityImplementedByCrm=false`, `permissionsPersistedInCrm=false` and `foundationSimulationActive=true`.
- [x] Next Gate: `Sprint4P4ProductiveRoutesLockedStubValidation`.
- [x] Do not activate login, Identity, JWT/cookie Auth, token storage, Portal runtime calls, Auth middleware, productive authorization, DB, EF runtime, migrations, productive routes or DELETE.

## CRM Sprint 4 P2 - Controlled Common DB Runtime Probe Behind Disabled Flag

- [x] Validate GitHub main contains Sprint 4 P1 commit.
- [x] Add controlled common DB runtime probe documentation.
- [x] Add `CrmCommonDbRuntimeProbeStatusService`.
- [x] Add disabled Infrastructure placeholder without DB, secret, file or network access.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-4/common-db-runtime-probe`.
- [x] Keep `commonDbRuntimeProbeEnabled=false`, `realDatabaseConfigured=false`, `connectionStringsConfigured=false`, `secretProviderRuntimeConnected=false`, `dbConnectionAttemptedByRuntime=false`, `sqlServerOwnedByCrm=false`, `efRuntimeEnabled=false`, `dbContextRuntimeActive=false`, `migrationsCreated=false`, `durablePersistenceEnabled=false`, `productiveCrudEnabled=false` and `apiRequiresDatabase=false`.
- [x] Next Gate: `Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag`.
- [x] Do not activate DB, EF runtime, migrations, secrets, Auth runtime, Portal runtime, productive routes, DELETE or productive UI.

## CRM Sprint 4 P1 - Runtime Environment Readiness and Local Tooling Hardening

- [x] Validate GitHub main contains Sprint 3 P6 commit.
- [x] Add local runtime readiness documentation and Windows runbooks.
- [x] Add preflight, guardrail and health scripts.
- [x] Add `CrmRuntimeEnvironmentReadinessStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-4/runtime-readiness`.
- [x] Keep `crmApiPort=8093`, `sqlServerOwnedByCrm=false`, `productiveRoutesActive=false`, `deleteEndpointsEnabled=false`, `realDatabaseConfigured=false`, `authRuntimeEnabled=false`, `portalRuntimeConnected=false`.
- [x] Next Gate: `Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag`.
- [x] Do not activate DB, EF runtime, migrations, Auth runtime, Portal runtime, productive routes, DELETE or productive UI.

## CRM Sprint 3 P6 - Productization Review Before Any Real Activation

- [x] Validate GitHub main contains Sprint 3 P5 commit.
- [x] Create Sprint 3 closure documentation and integrated evidence.
- [x] Create GO/NO-GO and decision records.
- [x] Create productization review matrix and no-go reviews for Security, Persistence and API.
- [x] Create Sprint 4 options, recommended path and gates.
- [x] Add `CrmSprint3ProductizationReviewStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-3/productization-review`.
- [x] Keep `OverallDecision=NoGoForRealActivation`, `ProductizationStatus=NotReady`, `FoundationCapabilitiesDecision=GoFoundationOnly` and `Sprint4PlanningDecision=Go`.
- [x] Next Gate: `Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening`.
- [x] Do not activate DB, EF runtime, migrations, Auth runtime, Portal runtime, productive routes, DELETE or productive UI.

## CRM Sprint 3 P5 - Productive API Route Draft Behind Disabled Flag

- [x] Validate GitHub main contains Sprint 3 P4 commit.
- [x] Document future productive routes for Lead, Account and Contact.
- [x] Keep productive routes unregistered.
- [x] Add `CrmProductiveApiRouteDraftStatusService`.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-3/productive-api-route-draft`.
- [x] Keep `productiveRoutesRegistered=false`, `productiveCrudEnabled=false`, `productiveAuthorizationEnabled=false`, `durablePersistenceEnabled=false`, `realDatabaseConfigured=false`, `efRuntimeEnabled=false`, `deleteEndpointsEnabled=false` and `foundationCrudStillSeparate=true`.
- [x] Next Gate: `Sprint3P6Sprint3ProductizationReview`.
- [x] Do not add DELETE, Auth real, Portal runtime, EF runtime, DB, migrations, connection strings, product UI or active productive routes.

## CRM Sprint 3 P4 - Portal Auth Runtime Contract Validation

- [x] Validate GitHub main contains Sprint 3 P3 commit.
- [x] Document future Portal Auth runtime contract without Auth activation.
- [x] Add Application contracts and `CrmPortalAuthRuntimeContractStatusService`.
- [x] Add Infrastructure contract-only placeholders without HTTP, URLs, credential storage or external I/O.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-3/portal-auth-runtime-contract`.
- [x] Keep FoundationSimulation active.
- [x] Keep `portalRuntimeConnected=false`, `authRuntimeEnabled=false`, `crmOwnsAuth=false`, `authOwnedBy=PortalCorporativo`, `tokenStorageEnabled=false`, `loginImplementedByCrm=false`, `identityImplementedByCrm=false`, `permissionsPersistedInCrm=false` and `productiveAuthorizationEnabled=false`.
- [x] Next Gate: `Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag`.
- [x] Do not add login, Identity, JWT/cookie auth, Portal HTTP, Portal URLs, persisted roles/permissions, DB, migrations, connection strings, productive routes or DELETE.

## CRM Sprint 3 P3 - EF/DbContext Prototype Behind Disabled Flag

- [x] Validate GitHub main contains Sprint 3 P2 commit.
- [x] Add disabled EF prototype contracts and status service.
- [x] Add `CrmDbContextPrototype` placeholder without EF runtime inheritance.
- [x] Add foundation-only GET endpoint `/api/crm/foundation/sprint-3/ef-prototype-status`.
- [x] Document disabled flag policy, prototype design, migrations no-go policy and runtime activation gates.
- [x] Keep `EF Runtime Enabled: false`, `DbContext Runtime Active: false`, `Migrations Created: false`, `Real Database Configured: false`, `Connection Strings Configured: false`, `Provider Configured: false`, `UseSqlServer Configured: false`, `Foundation Stores Remain Active: true` and `Productive CRUD Enabled: false`.
- [x] Next Gate: `Sprint3P4PortalAuthRuntimeContractValidation`.
- [x] Do not add EF provider packages, real database, connection values, migrations, SQL Server container, Auth runtime or productive endpoints.

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

## CRM Sprint 2 P6 - Productization gate decision and closure

Status: implemented for PR review.

- Added Sprint 2 closure and productization gate documentation.
- Added productization decision matrix and Sprint 3 roadmap.
- Added productization gate contracts and service.
- Added `GET /api/crm/foundation/sprint-2/productization-gate`.
- ProductizationStatus: NotReady.
- OverallDecision: NoGoForProductiveActivation.
- FoundationCrudDecision: GoFoundationOnly.
- DurablePersistenceDecision: NoGo.
- RealDatabaseDecision: NoGo.
- PortalAuthRuntimeDecision: NoGo.
- ProductiveCrudApiDecision: NoGo.
- Sprint3PlanningDecision: Go.
- NextGate: Sprint3P1DurablePersistenceSetupDesign.
- Guardrail: Productization gate decision only; no productive activation.

## CRM Sprint 3 P1 - Durable persistence setup design

Status: implemented for PR review.

- Added durable persistence setup design docs.
- Added common DB usage, migration/rollback and secrets strategy docs.
- Added durable persistence setup contracts and service.
- Added `GET /api/crm/foundation/sprint-3/durable-persistence-setup`.
- DurablePersistenceMode: DesignOnly.
- RealDatabaseConfigured: false.
- EfRuntimeEnabled: false.
- DbContextConfigured: false.
- MigrationsCreated: false.
- ConnectionStringsConfigured: false.
- SqlServerOwnedByCrm: false.
- ProductiveActivation: NoGo.
- NextGate: Sprint3P2CommonDbConnectionContractAndSecretStrategy.
- Guardrail: Durable persistence setup design only; no database, EF runtime, migrations, or connection strings configured.

## CRM Sprint 3 P2 - Common DB connection and secret strategy

Status: implemented for PR review.

- Added common DB connection and secret strategy documentation.
- Added logical DB naming and connection string policy docs.
- Added common DB connection strategy contracts and service.
- Added safe Infrastructure placeholders for secret provider and database configuration.
- Added `GET /api/crm/foundation/sprint-3/common-db-connection-strategy`.
- RealDatabaseConfigured: false.
- ConnectionStringsConfigured: false.
- SecretProviderConfigured: false.
- SecretProviderRuntimeConnected: false.
- LogicalDatabaseName: CrmDb.
- LogicalDatabaseNameIsPlaceholder: true.
- SecretStrategy: ContractOnly.
- ConnectionStringPolicy: NoRealValuesInRepository.
- NextGate: Sprint3P3EfDbContextPrototypeBehindDisabledFlag.
- Guardrail: Common DB connection contract only; no real database or secrets configured.

## Sprint 5 P4 - Portal Auth Probe Optional Activation
Status: implemented as contract-only, disabled by default. Next gate: Sprint5P5LockedProductiveRouteStubTrialInNonProduction.
## Sprint 5 P5 - Locked Productive Route Stub Trial

Status: implemented as document-only preferred, disabled by default. Next gate: Sprint5P6Sprint5GateDecision.
## Sprint 5 P6 - Sprint 5 Gate Decision

Status: implemented as closure/gate decision only. Next gate: Sprint6P1NonProductionRuntimeApprovalPackage.
## CRM Sprint 6 P4 - Portal Auth Token Propagation Dry-Run Contract

- Status: Implemented as contract-only dry-run.
- Endpoint: `GET /api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run`.
- Synthetic metadata: `mock://crm/portal-auth-token`, `mock://crm/portal-user`.
- Safety: tokenReadAttempted=false, headerReadAttempted=false, portalHttpAttempted=false, realTokenUsed=false, realHeadersRead=false.
- Boundaries: no Auth middleware, no `[Authorize]`, no login/logout, no CRM Identity, no Portal HTTP, no DB runtime, no productive routes.
- Next Gate: `Sprint6P5LockedStubRuntimeRegistrationTrial`.
## CRM Sprint 6 P5 - Locked Stub Runtime Registration Trial

- Status: Implemented as foundation-only trial contract.
- Endpoint: `GET /api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial`.
- Runtime registration decision: `DocumentOnlyPreferredWithNoRuntimeRegistration`.
- Defaults: lockedStubRuntimeRegistrationEnabled=false, lockedStubsRegisteredAtRuntime=false, productiveRoutesRegistered=false.
- Negative routes remain 404 for `/api/crm/leads`, `/api/crm/accounts`, `/api/crm/contacts`.
- Future explicit locked response: 423 Locked, NonProduction only, no DELETE.
- Next Gate: `Sprint6P6Sprint6GateDecision`.
