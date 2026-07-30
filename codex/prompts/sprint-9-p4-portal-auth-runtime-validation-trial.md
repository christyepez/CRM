# CRM Sprint 9 P4 - Portal Auth Runtime Validation Trial

Actúa como Codex Task Runner + Portal Integration Agent + Security Agent + Architecture Governance Agent + DevOps Agent + Backend Agent + QA Lead Agent + Documentation Agent para el proyecto CRM.

Repository:
christyepez/CRM

GitHub Repository:
https://github.com/christyepez/CRM

Phase:
CRM Sprint 9 - P4 Portal Auth Runtime Validation Trial

Base obligatoria:
25a0951c7bd1d342a7a83676619f4349d036d326

Objetivo:
Implementar un trial controlado de validación Portal Auth solo para NonProduction, disabled/fail-closed por defecto, sin activar Auth productivo, sin login/logout CRM, sin Identity propio, sin token storage, sin lectura de Authorization headers por defecto y sin Portal HTTP por defecto.

Contexto:
Sprint 9 P2 agregó Secret Provider Runtime Enablement Trial:
- Secret Provider trial existe.
- Está disabled por defecto.
- Retorna metadata sanitizada.
- No retorna secretos.
- No loguea secretos.
- No persiste secretos.
- No cachea secretos.

Sprint 9 P3 agregó Common DB Runtime Connectivity Trial:
- Common DB trial existe.
- Está disabled por defecto.
- Retorna metadata sanitizada.
- No expone connection strings.
- No activa schema, migrations, EF runtime ni persistencia productiva.

P4 puede consumir únicamente metadata sanitizada de P2/P3. No puede activar Auth productivo ni hacer enforcement real de permisos.

Comportamiento por defecto esperado:
- PortalAuthRuntimeValidationTrialExists: true.
- PortalAuthRuntimeValidationTrialApproved: true.
- PortalAuthRuntimeValidationTrialEnabled: false.
- PortalAuthValidationAttempted: false.
- PortalAuthValidated: false.
- PortalHttpAttempted: false.
- PortalHttpConfigured: false.
- PortalAuthUrlResolved: false.
- PortalAuthUrlReturnedToApi: false.
- PortalClientSecretResolved: false.
- PortalClientSecretReturnedToApi: false.
- AuthHeaderRead: false.
- TokenRead: false.
- TokenStored: false.
- ClaimsMapped: false.
- ProductiveAuthEnabled: false.
- LoginEndpointCreated: false.
- LogoutEndpointCreated: false.
- IdentityRuntimeEnabled: false.
- AuthAttributeEnabled: false.
- SecretProviderMetadataDependencyValidated: true.
- CommonDbMetadataDependencyValidated: true.
- NonProductionOnly: true.
- ProductionBlocked: true.
- FailClosedByDefault: true.
- RollbackAvailable: true.
- ObservabilityMetadataOnly: true.
- NextGate: Sprint9P5ProductiveRouteDryRunTrial.

Endpoints esperados:
GET /api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial

POST /api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial/probe

El probe debe:
- retornar 423 por defecto.
- requerir flag explícito NonProduction.
- no llamar Portal HTTP por defecto.
- no leer Authorization headers por defecto.
- no leer tokens.
- no almacenar tokens.
- no activar [Authorize].
- no crear login/logout.
- no activar Identity propio.
- no retornar URLs privadas, client secrets, tokens ni claims sensibles.
- devolver solo metadata sanitizada.

Flag esperado:
Crm:RuntimeTrials:PortalAuthValidationEnabled=false

Reglas críticas:
- No subir secretos reales.
- No subir .env.
- No subir tokens.
- No subir certificados reales.
- No subir URLs privadas reales de Portal/Auth.
- No exponer client secrets por API.
- No loguear client secrets.
- No persistir/cachear secretos/tokens.
- No activar Auth productivo.
- No crear login/logout.
- No crear Identity propio.
- No activar token storage.
- No leer Authorization headers por defecto.
- No leer tokens por defecto.
- No usar [Authorize] productivo.
- No usar UseAuthentication/UseAuthorization productivo.
- No llamar Portal HTTP por defecto.
- No activar DB runtime productivo.
- No activar rutas productivas por defecto.
- No CRUD productivo.
- No DELETE.
- No UI productiva.
- No SQL Server propio.
- No datos reales.
- No usar secretos fuera del metadata-only contract de Sprint 9 P2.
- No usar DB fuera del metadata-only contract de Sprint 9 P3.

Archivos permitidos:
- docs/security/crm-sprint-9-p4-portal-auth-runtime-validation-trial.md
- docs/security/crm-portal-auth-runtime-validation-trial-policy.md
- docs/security/crm-portal-auth-runtime-validation-trial-contract.md
- docs/security/crm-portal-auth-runtime-validation-trial-redaction.md
- docs/operations/crm-portal-auth-runtime-validation-trial-runbook.md
- docs/operations/crm-portal-auth-runtime-validation-trial-rollback.md
- docs/architecture/crm-portal-auth-runtime-validation-trial-architecture.md
- src/CRM.Application/Foundation/CrmPortalAuthRuntimeValidationTrialContracts.cs
- src/CRM.Application/Foundation/CrmPortalAuthRuntimeValidationTrialStatusService.cs
- src/CRM.Infrastructure/Portal/Auth/PortalAuthRuntimeValidationTrialOptions.cs
- src/CRM.Infrastructure/Portal/Auth/PortalAuthRuntimeValidationTrialResult.cs
- src/CRM.Infrastructure/Portal/Auth/PortalAuthRuntimeValidationTrialService.cs
- src/CRM.Api/Program.cs
- tests/CRM.UnitTests/CrmPortalAuthRuntimeValidationTrialStatusServiceTests.cs
- tests/CRM.UnitTests/PortalAuthRuntimeValidationTrialServiceTests.cs
- tests/CRM.ArchitectureTests/PortalAuthRuntimeValidationTrialArchitectureTests.cs
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
  - /api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial
- probe check:
  - POST /api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial/probe debe retornar 423 por defecto.
- negative route checks:
  - /api/crm/leads 404 por defecto.
  - /api/crm/accounts 404 por defecto.
  - /api/crm/contacts 404 por defecto.

PR esperado:
Branch:
crm-sprint-9-p4-portal-auth-runtime-validation-trial

Title:
feat: add crm portal auth runtime validation trial

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
Portal Auth Runtime Validation Trial Documentation:
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
