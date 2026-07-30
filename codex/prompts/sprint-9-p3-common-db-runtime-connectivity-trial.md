# CRM Sprint 9 P3 - Common DB Runtime Connectivity Trial

Actúa como Codex Task Runner + Data Architect Agent + DevOps Agent + Security Agent + Architecture Governance Agent + Backend Agent + QA Lead Agent + Documentation Agent para el proyecto CRM.

Repository:
christyepez/CRM

GitHub Repository:
https://github.com/christyepez/CRM

Phase:
CRM Sprint 9 - P3 Common DB Runtime Connectivity Trial

Base obligatoria:
84e2496bc66f585890077ce143b6b1d25e0bf284

Objetivo:
Implementar un trial controlado de conectividad Common DB solo para NonProduction, disabled/fail-closed por defecto, sin exponer connection strings, sin crear schema, sin ejecutar migrations, sin activar CRUD productivo y sin activar producción.

Contexto:
Sprint 9 P2 agregó Secret Provider Runtime Enablement Trial:
- Secret Provider trial existe.
- Está disabled por defecto.
- Retorna metadata sanitizada.
- No retorna secretos.
- No loguea secretos.
- No persiste secretos.
- No cachea secretos.
- NextGate: Sprint9P3CommonDbRuntimeConnectivityTrial.

P3 puede consumir únicamente metadata sanitizada del boundary de Secret Provider. No puede materializar ni exponer valores secretos.

Comportamiento por defecto esperado:
- CommonDbRuntimeConnectivityTrialExists: true.
- CommonDbRuntimeConnectivityTrialApproved: true.
- CommonDbRuntimeConnectivityTrialEnabled: false.
- CommonDbConnectionAttempted: false.
- CommonDbConnected: false.
- CommonDbConnectionStringResolved: false.
- CommonDbConnectionStringReturnedToApi: false.
- CommonDbConnectionStringLogged: false.
- CommonDbConnectionStringPersisted: false.
- CommonDbConnectionStringCached: false.
- SecretProviderMetadataDependencyValidated: true.
- SchemaCreated: false.
- MigrationExecuted: false.
- EfRuntimeEnabled: false.
- ProductivePersistenceEnabled: false.
- NonProductionOnly: true.
- ProductionBlocked: true.
- FailClosedByDefault: true.
- RollbackAvailable: true.
- ObservabilityMetadataOnly: true.
- NextGate: Sprint9P4PortalAuthRuntimeValidationTrial.

Endpoints esperados:
GET /api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial

POST /api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial/probe

El probe debe:
- retornar 423 por defecto.
- requerir flag explícito NonProduction.
- no retornar connection string.
- no loguear connection string.
- no persistir/cachear connection string.
- no crear schema.
- no ejecutar migrations.
- no usar EF productivo.
- no activar CRUD productivo.
- devolver solo metadata sanitizada.

Flag esperado:
Crm:RuntimeTrials:CommonDbConnectivityEnabled=false

Reglas críticas:
- No subir secretos reales.
- No subir .env.
- No subir connection strings reales.
- No exponer connection strings por API.
- No loguear connection strings.
- No persistir/cachear connection strings.
- No activar DB runtime productivo.
- No ejecutar migrations.
- No crear schema.
- No usar EF productivo.
- No activar Portal Auth runtime.
- No activar rutas productivas por defecto.
- No CRUD productivo.
- No DELETE.
- No UI productiva.
- No SQL Server propio.
- No datos reales.
- No usar secretos fuera del metadata-only contract de Sprint 9 P2.

Archivos permitidos:
- docs/data/crm-sprint-9-p3-common-db-runtime-connectivity-trial.md
- docs/data/crm-common-db-runtime-connectivity-trial-policy.md
- docs/data/crm-common-db-runtime-connectivity-trial-contract.md
- docs/data/crm-common-db-runtime-connectivity-trial-redaction.md
- docs/operations/crm-common-db-runtime-connectivity-trial-runbook.md
- docs/operations/crm-common-db-runtime-connectivity-trial-rollback.md
- docs/architecture/crm-common-db-runtime-connectivity-trial-architecture.md
- src/CRM.Application/Foundation/CrmCommonDbRuntimeConnectivityTrialContracts.cs
- src/CRM.Application/Foundation/CrmCommonDbRuntimeConnectivityTrialStatusService.cs
- src/CRM.Infrastructure/Data/CommonDb/CommonDbRuntimeConnectivityTrialOptions.cs
- src/CRM.Infrastructure/Data/CommonDb/CommonDbRuntimeConnectivityTrialResult.cs
- src/CRM.Infrastructure/Data/CommonDb/CommonDbRuntimeConnectivityTrialService.cs
- src/CRM.Api/Program.cs
- tests/CRM.UnitTests/CrmCommonDbRuntimeConnectivityTrialStatusServiceTests.cs
- tests/CRM.UnitTests/CommonDbRuntimeConnectivityTrialServiceTests.cs
- tests/CRM.ArchitectureTests/CommonDbRuntimeConnectivityTrialArchitectureTests.cs
- tools/check-crm-e2e-foundation.ps1
- tools/check-crm-guardrails.ps1
- tools/check-crm-health.ps1
- tools/preflight-crm-local.ps1
- tools/verify-crm-foundation.ps1
- frontend/crm-web/src/main.ts
- frontend/crm-web/tools/verify-crm-foundation.mjs
- README.md
- codex/TASKS.md
- docs/api/crm-api-contracts.md
- docs/api/crm-api-index.md
- docs/api/crm-foundation-endpoint-inventory.md
- docs/roadmap/crm-sprint-9-gates.md
- docs/roadmap/crm-sprint-9-recommended-path.md

Validaciones obligatorias:
- git diff --check
- dotnet build CRM.sln
- DOTNET_ROLL_FORWARD=Major dotnet test CRM.sln --no-build
- tools/check-crm-guardrails.ps1
- docker compose config
- tools/preflight-crm-local.ps1
- tools/verify-crm-foundation.ps1
- pnpm run build
- pnpm test
- docker compose up -d --build
- health/API checks:
  - /health
  - /health/live
  - /health/ready
  - /api/crm/readiness
  - /api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial
  - /api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial
- probe check:
  - POST /api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial/probe debe retornar 423 por defecto.
- negative route checks:
  - /api/crm/leads 404 por defecto.
  - /api/crm/accounts 404 por defecto.
  - /api/crm/contacts 404 por defecto.

PR esperado:
Branch:
crm-sprint-9-p3-common-db-runtime-connectivity-trial

Title:
feat: add crm common db runtime connectivity trial

No hacer merge automático.

Cierre esperado:
Agent:
Repository:
GitHub Repository:
Task:
Phase:
GitHub Main Source Confirmed:
Base Main Commit:
Files Read:
Files Created:
Files Modified:
Common DB Runtime Connectivity Trial Documentation:
Application Contracts:
Application Service:
Infrastructure Runtime Trial Adapter:
API Foundation Endpoint:
Runtime Probe Endpoint:
Tooling / Preflight Updates:
Documentation Updates:
Tests:
Verification Tools:
Frontend Impact:
Docker/Build:
Architecture Guardrails:
Security/Sanitization:
Database Impact:
Portal Authorization Status:
Persistence Status:
Productization Status:
Sprint 9 Status:
Commands Executed:
Health Status:
Probe Checks:
Negative Route Checks:
Locked Route Checks:
Branch:
Commit:
Pull Request:
Risks:
Blocked Items:
Next Step:
