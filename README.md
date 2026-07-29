# CRM Corporativo

## Sprint 7 P1 - Secret Provider Real NonProduction Approval

Sprint 7 P1 creates the approval package for a future real Secret Provider NonProduction runtime probe. The package exists, but approval is not granted and no real secret read occurs.

Endpoint:

- `GET /api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval`

Current decision: `SecretProviderRealNonProductionApprovalPackageExists=true`, `SecretProviderRealNonProductionApprovalGranted=false`, `SecretProviderRealRuntimeEnabled=false`, `SecretProviderRealRuntimeConnected=false`, `RealSecretReadAttempted=false`, `KeyVaultRuntimeClientEnabled=false`, `AzureSecretSdkRuntimeEnabled=false`, `EnvFileRequired=false`, `EnvSecretReadAllowed=false`, `SecretsLogged=false`, `SecretNamesApproved=false`, `SecretValuesApproved=false`.

Warning: `Secret Provider real NonProduction approval package only; no real secrets are read`. Next Gate: `Sprint7P2SecretProviderRealNonProductionRuntimeProbe`.

## Sprint 6 P6 - Gate Decision

Sprint 6 is closed as a gate decision only. Overall decision: `GoForSprint7ControlledNonProductionActivationPlanning`; Real Activation Decision: `NoGo`; Productization Status: `NotReady`; Sprint 7 Planning: `Go`.

Endpoint:

- `GET /api/crm/foundation/sprint-6/gate-decision`

Warning: `Sprint 6 gate decision only; no real activation`. Next Gate: `Sprint7P1SecretProviderRealNonProductionApproval`.

No real secret provider, real database connection, Portal Auth runtime, locked stub runtime registration, productive route, productive CRUD, DELETE endpoint or productive UI is activated.

## Sprint 6 P3 - Common DB Connectivity Dry-Run Contract

Sprint 6 P3 creates a Common DB dry-run contract only. It uses safe mock metadata and the synthetic reference `mock://crm/common-db`, but does not resolve a real connection string and does not connect to a database.

Endpoint:

- `GET /api/crm/foundation/sprint-6/common-db-connectivity-dry-run`

Current decision: `CommonDbConnectivityDryRunContractExists=true`, `CommonDbDryRunApprovalGranted=false`, `CommonDbDryRunEnabled=false`, `CommonDbConnectionAttempted=false`, `UsesSecretProviderSafeMockMetadata=true`, `UsesSyntheticConnectionReference=true`, `RealConnectionStringUsed=false`, `ConnectionStringResolved=false`, `SqlConnectionCreated=false`, `DbConnectionCreated=false`, `EfRuntimeEnabled=false`, `MigrationsCreated=false`, `ApiRequiresDatabase=false`.

Warning: `Common DB connectivity dry-run contract only; no database connection is attempted`. Next Gate: `Sprint6P4PortalAuthTokenPropagationDryRunContract`.

## Sprint 6 P2 - Secret Provider Safe Mock Activation

Sprint 6 P2 enables only a safe deterministic Secret Provider mock for non-production contract validation.

Endpoint:

- `GET /api/crm/foundation/sprint-6/secret-provider-safe-mock-activation`

Current decision: `SecretProviderSafeMockExists=true`, `SecretProviderSafeMockEnabled=true`, `SecretProviderRuntimeConnected=false`, `SecretProviderReadsRealSecrets=false`, `SecretProviderReadsSyntheticValues=true`, `SecretProviderReadsEnabledForMockOnly=true`, `RealSecretsConfigured=false`, `EnvFileRequired=false`, `KeyVaultClientConfigured=false`, `AzureSdkForSecretsConfigured=false`, `SecretValuesExposedInLogs=false`.

Allowed synthetic values: `mock://crm/common-db`, `mock://crm/portal-auth-base-url`, `mock-client-id`, `mock-client-secret-not-real`, `mock://crm/observability`. Warning: `Secret Provider safe mock only; no real secrets are read`. Next Gate: `Sprint6P3CommonDbConnectivityDryRunContract`.

## Sprint 6 P1 - NonProduction Runtime Approval Package

Sprint 6 P1 creates the approval package for future non-production runtime trials. The package exists, but no runtime approval is granted.

Endpoint:

- `GET /api/crm/foundation/sprint-6/nonproduction-runtime-approval-package`

Current decision: `NonProductionRuntimeApprovalPackageExists=true`, `NonProductionRuntimeApprovalGranted=false`, `SecretProviderMockApprovalGranted=false`, `CommonDbDryRunApprovalGranted=false`, `PortalAuthDryRunApprovalGranted=false`, `LockedStubRuntimeTrialApprovalGranted=false`, `RealActivationApprovalGranted=false`, `ProductiveRoutesApprovalGranted=false`, `DeleteApprovalGranted=false`.

Required gates remain: synthetic data, rollback, observability, security review and architecture review. Warning: `NonProduction runtime approval package only; no runtime approval is granted`. Next Gate: `Sprint6P2SecretProviderSafeMockActivation`.

## Sprint 5 P3 - Common DB Probe Optional Activation

Sprint 5 P3 prepares Common DB probe optional activation for non-production only. Status: `CommonDbProbeOptionalActivation`; Common DB Probe Optional Activation Exists: `true`; Common DB Probe Activation Approved: `false`; Common DB Probe Enabled: `false`; Common DB Connection Attempted: `false`; Secret Provider Runtime Required: `true`; Secret Provider Runtime Connected: `false`; Secret Reads Required Before Activation: `true`; Secret Reads Enabled: `false`; Real Database Configured: `false`; Connection Strings Configured: `false`; EF Runtime Enabled: `false`; Migrations Created: `false`; API Requires Database: `false`.

Endpoint:

- `GET /api/crm/foundation/sprint-5/common-db-probe-optional-activation`

Warning: `Common DB probe optional activation only; no database connection is attempted`. Next Gate: `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction`.

## Sprint 5 P2 - Secret Provider Runtime Contract Validation

Sprint 5 P2 validates the Secret Provider runtime contract only. Status: `SecretProviderRuntimeContractValidation`; Secret Provider Contract Exists: `true`; Secret Provider Runtime Connected: `false`; Secret Provider Reads Enabled: `false`; Secret Read Attempted By Runtime: `false`; Real Secrets Configured: `false`; Env File Required: `false`; Connection Strings Configured: `false`; Key Vault Client Configured: `false`; Secret Values Exposed: `false`.

Endpoint:

- `GET /api/crm/foundation/sprint-5/secret-provider-runtime-contract`

Warning: `Secret Provider contract validation only; no secrets are read`. Next Gate: `Sprint5P3CommonDbProbeOptionalActivationInNonProduction`.

## Sprint 5 P1 - Controlled Runtime Probe Activation Plan

Sprint 5 P1 creates a controlled activation plan only. Status: `ControlledRuntimeProbeActivationPlan`; Runtime Probe Activation Plan Exists: `true`; Runtime Probe Activation Approved: `false`; Common DB Probe Activation Approved: `false`; Portal Auth Probe Activation Approved: `false`; Productive Routes Activation Approved: `false`; Real Activation Approved: `false`; Non-Production Only: `true`; Synthetic Data Required: `true`; Rollback Plan Required: `true`; Observability Required: `true`; Secret Provider Required: `true`; DELETE Still NoGo: `true`.

Endpoint:

- `GET /api/crm/foundation/sprint-5/runtime-probe-activation-plan`

Warning: `Runtime probe activation plan only; no runtime activation approved`. Next Gate: `Sprint5P2SecretProviderRuntimeContractValidation`.

## Sprint 4 P6 - Sprint 4 Gate Decision

Sprint 4 is closed with `OverallDecision=GoForNonProductionFoundationPilot`, `RealActivationDecision=NoGo`, `ProductizationStatus=NotReady`, `NonProductionE2EPilotDecision=GoFoundationOnly` and `Sprint5PlanningDecision=Go`.

Endpoint:

- `GET /api/crm/foundation/sprint-4/gate-decision`

Warning: `Sprint 4 gate decision only; no real activation`. Next Gate: `Sprint5P1ControlledRuntimeProbeActivationPlan`.

## Sprint 4 P5 - Non-Production E2E Pilot Readiness

P5 prepares a foundation-only E2E pilot without real activation. Status: `NonProductionE2EPilotReadiness`; E2E Pilot Can Run: `true`; E2E Pilot Scope: `FoundationOnly`; Productive Routes Used: `false`; Real Database Used: `false`; Portal Auth Runtime Used: `false`; Durable Persistence Used: `false`; DELETE Operations Used: `false`; Synthetic Data Only: `true`; Foundation Endpoints Only: `true`; Negative Route Validation Required: `true`.

Endpoint:

- `GET /api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness`

Warning: `Non-production E2E pilot readiness only; no real activation`. Next Gate: `Sprint4P6Sprint4GateDecision`.

## Sprint 4 P4 - Productive Routes Locked Stub Validation

P4 validates the future productive route strategy without registering productive routes or locked stubs. Status: `ProductiveRoutesLockedStubValidation`; Locked Stubs Strategy: `DocumentOnlyPreferred`; Productive Routes Registered: `false`; Locked Stubs Registered: `false`; Productive CRUD Enabled: `false`; Productive Authorization Enabled: `false`; DELETE Endpoints Enabled: `false`; DB Required: `false`; Auth Runtime Required: `false`; Foundation CRUD Still Separate: `true`.

Endpoint:

- `GET /api/crm/foundation/sprint-4/productive-routes-locked-stub`

Warning: `Productive routes locked stub validation only; no productive routes are active`. Next Gate: `Sprint4P5NonProductionE2EPilotReadiness`.

## Sprint 4 P3 - Portal Auth Runtime Probe Behind Disabled Flag

P3 adds a controlled Portal Auth runtime probe contract while keeping Auth runtime disabled. Status: `PortalAuthRuntimeProbe`; Portal Auth Runtime Probe Exists: `true`; Portal Auth Runtime Probe Enabled: `false`; Portal Runtime Connected: `false`; Auth Runtime Enabled: `false`; Productive Authorization Enabled: `false`; Token Read Attempted By Runtime: `false`; Portal HTTP Attempted By Runtime: `false`; Login Implemented By CRM: `false`; Identity Implemented By CRM: `false`; Permissions Persisted In CRM: `false`; Foundation Simulation Active: `true`.

Endpoint:

- `GET /api/crm/foundation/sprint-4/portal-auth-runtime-probe`

Warning: `Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted`. Next Gate: `Sprint4P4ProductiveRoutesLockedStubValidation`.

## Sprint 4 P2 - Controlled Common DB Runtime Probe Behind Disabled Flag

P2 adds a controlled common DB runtime probe contract while keeping runtime persistence disabled. Status: `CommonDbRuntimeProbe`; Common DB Runtime Probe Exists: `true`; Common DB Runtime Probe Enabled: `false`; Real Database Configured: `false`; Connection Strings Configured: `false`; Secret Provider Runtime Connected: `false`; DB Connection Attempted By Runtime: `false`; SQL Server Owned By CRM: `false`; EF Runtime Enabled: `false`; DbContext Runtime Active: `false`; Migrations Created: `false`; Durable Persistence Enabled: `false`; Productive CRUD Enabled: `false`; API Requires Database: `false`.

Endpoint:

- `GET /api/crm/foundation/sprint-4/common-db-runtime-probe`

Warning: `Common DB runtime probe exists but is disabled; no database connection is attempted`. Next Gate: `Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag`.

## Sprint 4 P1 - Runtime Environment Readiness and Local Tooling Hardening

P1 hardens local tooling before controlled runtime probes. Status: `RuntimeEnvironmentReadiness`; Docker Compose Expected: `true`; CRM API Port: `8093`; SQL Server Owned By CRM: `false`; Node PATH Required For Frontend Verifier: `false`; Productive Routes Active: `false`; DELETE Endpoints Enabled: `false`; Real Database Configured: `false`; Auth Runtime Enabled: `false`; Portal Runtime Connected: `false`; Productization Status: `NotReady`.

Endpoint:

- `GET /api/crm/foundation/sprint-4/runtime-readiness`

Warning: `Runtime readiness only; no real activation`. Next Gate: `Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag`.

## Sprint 3 P6 - Productization Review Before Any Real Activation

P6 closes Sprint 3 with a formal productization review. Overall Decision: `NoGoForRealActivation`; Productization Status: `NotReady`; Durable Persistence: `NoGo`; Real Database: `NoGo`; EF Runtime: `NoGo`; Portal Auth Runtime: `NoGo`; Productive API Routes: `NoGo`; Productive CRM UI: `NoGo`; Foundation Capabilities: `GoFoundationOnly`; Sprint 4 Planning: `Go`.

Endpoint:

- `GET /api/crm/foundation/sprint-3/productization-review`

Warning: `Sprint 3 productization review only; no real activation`. Next Gate: `Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening`.

## Sprint 3 P5 - Productive API Route Draft Behind Disabled Flag

P5 creates the productive API route draft for future Lead, Account and Contact APIs without registering active productive routes. Productive API Draft: `Exists`; Productive Routes Registered: `false`; Productive CRUD Enabled: `false`; Productive Authorization Enabled: `false`; Durable Persistence Enabled: `false`; Real Database Configured: `false`; EF Runtime Enabled: `false`; DELETE Endpoints Enabled: `false`; Foundation CRUD Still Separate: `true`.

Endpoint:

- `GET /api/crm/foundation/sprint-3/productive-api-route-draft`

Warning: `Productive API route draft only; routes are not active`. Next Gate: `Sprint3P6Sprint3ProductizationReview`.

## Sprint 3 P4 - Portal Auth Runtime Contract Validation

P4 validates the future Portal Auth runtime contract without activating real Auth. PortalCorporativo remains owner of Auth/SSO/user/tenant/permissions, while CRM keeps `Portal Runtime Connected: false`, `Auth Runtime Enabled: false`, `CRM Owns Auth: false`, `Token Storage Enabled: false`, `Login Implemented By CRM: false`, `Identity Implemented By CRM: false`, `Permissions Persisted In CRM: false`, `Foundation Simulation Active: true` and `Productive Authorization Enabled: false`.

Endpoint:

- `GET /api/crm/foundation/sprint-3/portal-auth-runtime-contract`

Warning: `Portal Auth runtime contract validation only; no real Auth runtime configured`. Next Gate: `Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag`.

## Sprint 3 P3 - EF/DbContext Prototype Behind Disabled Flag

P3 adds a review-only EF/DbContext prototype. It confirms `Sprint 3 P3 EF Prototype: Exists`, but keeps `EF Runtime Enabled: false`, `DbContext Runtime Active: false`, `Migrations Created: false`, `Real Database Configured: false`, `Connection Strings Configured: false`, `Provider Configured: false`, `UseSqlServer Configured: false`, `Foundation Stores Remain Active: true` and `Productive CRUD Enabled: false`.

Endpoint:

- `GET /api/crm/foundation/sprint-3/ef-prototype-status`

Warning: `EF/DbContext prototype only; runtime disabled and no database configured`. Next Gate: `Sprint3P4PortalAuthRuntimeContractValidation`.

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

### CRM Sprint 2 P6 - Productization gate closure

CRM Sprint 2 is closed with `ProductizationStatus=NotReady` and `OverallDecision=NoGoForProductiveActivation`.

Current P6 decision:

- Sprint 2: Closed
- Overall Decision: NoGoForProductiveActivation
- Foundation CRUD: GoFoundationOnly
- Durable Persistence: NoGo
- Real Database: NoGo
- Portal Auth Runtime: NoGo
- Productive CRUD API: NoGo
- Sprint 3 Planning: Go
- Next Gate: Sprint3P1DurablePersistenceSetupDesign
- Warning: Productization gate decision only; no productive activation

No DB real, Auth real, Portal runtime, productive route, DELETE endpoint or product UI activation occurs in P6.

### CRM Sprint 3 P1 - Durable persistence setup design

CRM Sprint 3 P1 starts durable persistence preparation as design-only work.

- Sprint 3 P1 Durable Persistence Setup: DesignOnly
- Real Database Configured: false
- EF Runtime Enabled: false
- DbContext Configured: false
- Migrations Created: false
- Connection Strings Configured: false
- SQL Server Owned By CRM: false
- Secret Strategy: PlannedOnly
- Migration Strategy: PlannedOnly
- Productive Activation: NoGo
- Next Gate: Sprint3P2CommonDbConnectionContractAndSecretStrategy
- Warning: Durable persistence setup design only; no database, EF runtime, migrations, or connection strings configured

No DB real, EF runtime, migrations, connection strings, SQL Server container, secrets, `.env`, productive routes or DELETE endpoint are added in P1.

### CRM Sprint 3 P2 - Common DB connection and secret strategy

CRM Sprint 3 P2 defines the common DB connection contract and secret strategy as contract-only work.

- Sprint 3 P2 Common DB Strategy: ContractOnly
- Logical Database Name: CrmDb
- Logical DB Placeholder: true
- Real Database Configured: false
- Connection Strings Configured: false
- Secret Provider Configured: false
- Secret Provider Runtime Connected: false
- SQL Server Owned By CRM: false
- EF Runtime Enabled: false
- Next Gate: Sprint3P3EfDbContextPrototypeBehindDisabledFlag
- Warning: Common DB connection contract only; no real database or secrets configured

No real DB, real secrets, connection string values, EF runtime, migrations, SQL Server container, Portal runtime or productive API activation occurs in P2.

## CRM Sprint 5 P4
Portal Auth probe optional activation is documented and exposed through a foundation endpoint. It is disabled by default and does not call Portal or read tokens/headers.
## CRM Sprint 5 P5

Locked productive route stub trial is documented and exposed through a foundation endpoint. Productive CRM routes remain unregistered by default and negative route checks stay 404.
## CRM Sprint 5 P6

Sprint 5 gate decision is complete: `GoForControlledNonProductionPreparation`. Real activation remains `NoGo`; Sprint 6 planning is `Go`.
## CRM Sprint 6 P4 - Portal Auth Token Propagation Dry-Run Contract

Sprint 6 P4 adds a foundation-only Portal Auth token propagation dry-run contract. CRM exposes `GET /api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run` with synthetic metadata only: `mock://crm/portal-auth-token` and `mock://crm/portal-user`.

CRM does not read real tokens, does not read headers, does not inspect Authorization values, does not call PortalCorporativo over HTTP, does not implement login/logout or Identity, and does not persist roles or permissions. Real activation remains No-Go. Next gate: `Sprint6P5LockedStubRuntimeRegistrationTrial`.
## CRM Sprint 6 P5 - Locked Stub Runtime Registration Trial

Sprint 6 P5 adds a foundation-only locked stub runtime registration trial at `GET /api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial`.

Runtime registration is not approved. Productive routes are not registered by default, so `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` must continue returning 404. Future explicit NonProduction enablement, if approved later, must return 423 Locked with no domain services, stores, DB, Portal Auth, token/header reads or DELETE. Next gate: `Sprint6P6Sprint6GateDecision`.

## CRM Sprint 7 P2 - Secret Provider Real NonProduction Runtime Probe

Sprint 7 P2 adds a controlled runtime probe contract for Secret Provider real NonProduction usage. The probe exists but is skipped by default because approval is not granted. It validates logical secret names only, does not read real values, does not call Key Vault, does not use Azure secret SDK runtime calls, does not require `.env`, and keeps real activation as NoGo. Next gate: `Sprint7P3CommonDbRealConnectivityNonProductionProbe`.

## CRM Sprint 7 P3 - Common DB Real Connectivity NonProduction Probe

Sprint 7 P3 adds a Common DB real connectivity NonProduction probe contract. The probe exists but is skipped because Secret Provider real approval is not granted. CRM does not resolve, materialize, log or return connection strings; does not open DB connections; does not enable EF runtime; does not create migrations; does not add SQL Server compose services; and keeps productive routes as NoGo. Next gate: `Sprint7P4PortalAuthRealRuntimeProbe`.

## CRM Sprint 7 P4 - Portal Auth Real Runtime Probe

Sprint 7 P4 adds a Portal Auth real runtime NonProduction probe contract at `GET /api/crm/foundation/sprint-7/portal-auth-real-runtime-probe`. The probe exists but is skipped because Portal Auth approval is not granted. CRM does not resolve, materialize, log or return a Portal Auth base URL; does not create a Portal HTTP client; does not call Portal; does not read tokens or headers; does not implement login/logout, Identity, roles or permissions; and keeps real activation as NoGo. Next gate: `Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423`.
## CRM Sprint 7 P5 - Locked productive route runtime registration

P5 adds `GET /api/crm/foundation/sprint-7/locked-productive-route-runtime-registration` and a disabled-by-default registrar for future productive CRM route shapes. `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` still return `404` by default. If `Crm:ProductiveRoutes:LockedRegistrationEnabled=true` is enabled in NonProduction, GET/POST/PUT/PATCH return `423 Locked` without CRUD, domain execution, DB, Portal Auth runtime, token/header reads, DELETE or product UI. Next gate: `Sprint7P6Sprint7GateDecision`.
## CRM Sprint 7 P6 - Gate decision

Sprint 7 is closed through `GET /api/crm/foundation/sprint-7/gate-decision`. The overall decision is `GoForSprint8ControlledRuntimeApprovalAndPilotPlanning`; real activation remains `NoGo`, productization remains `NotReady`, and Sprint 8 planning is `Go`. Next gate: `Sprint8P1SecretProviderApprovalDecision`.
