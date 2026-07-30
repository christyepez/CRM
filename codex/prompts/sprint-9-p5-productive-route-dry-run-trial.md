# CRM Sprint 9 P5 - Productive Route Dry Run Trial

Actúa como Codex Task Runner + API Governance Agent + Security Agent + Architecture Governance Agent + DevOps Agent + Backend Agent + QA Lead Agent + Documentation Agent para el proyecto CRM.

Repository:
christyepez/CRM

GitHub Repository:
https://github.com/christyepez/CRM

Phase:
CRM Sprint 9 - P5 Productive Route Dry Run Trial

Base obligatoria:
3da901f1d00fae351af1f4df60e80ad906cc9cf6

Objetivo:
Implementar un dry-run controlado de rutas productivas CRM solo para NonProduction, disabled/fail-closed por defecto, sin activar CRUD productivo real, sin DELETE, sin side effects, sin escritura en base, sin DB runtime productivo, sin Auth enforcement real y sin UI productiva.

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

Sprint 9 P4 agregó Portal Auth Runtime Validation Trial:
- Portal Auth trial existe.
- Está disabled por defecto.
- Retorna metadata sanitizada.
- No activa Auth productivo.
- No crea login/logout.
- No crea Identity propio.
- No lee Authorization headers por defecto.
- No lee tokens por defecto.
- No usa Portal HTTP por defecto.

P5 puede consumir únicamente metadata sanitizada de P2/P3/P4. No puede ejecutar dominio productivo real.

Comportamiento por defecto esperado:
- ProductiveRouteDryRunTrialExists: true.
- ProductiveRouteDryRunTrialApproved: true.
- ProductiveRouteDryRunTrialEnabled: false.
- ProductiveRoutesRegisteredByDefault: false.
- ProductiveRoutesDryRunRegistered: false.
- ProductiveRouteDryRunAttempted: false.
- ProductiveRouteDryRunAllowed: false.
- ProductiveRouteDryRunDecisionReturned: false.
- ProductiveRouteDryRunStatusCode: 423.
- ProductiveCrudEnabled: false.
- ProductiveDomainExecutionEnabled: false.
- ProductivePersistenceEnabled: false.
- DatabaseWriteAttempted: false.
- SideEffectsAllowed: false.
- DeleteEndpointsEnabled: false.
- DbRuntimeEnabled: false.
- EfRuntimeEnabled: false.
- MigrationsEnabled: false.
- SchemaChangeAllowed: false.
- PortalAuthMetadataDependencyValidated: true.
- CommonDbMetadataDependencyValidated: true.
- SecretProviderMetadataDependencyValidated: true.
- AuthHeaderRead: false.
- TokenRead: false.
- TokenStored: false.
- AuthAttributeEnabled: false.
- LoginEndpointCreated: false.
- LogoutEndpointCreated: false.
- IdentityRuntimeEnabled: false.
- NonProductionOnly: true.
- ProductionBlocked: true.
- FailClosedByDefault: true.
- RollbackAvailable: true.
- ObservabilityMetadataOnly: true.
- NextGate: Sprint9P6Sprint9GateDecision.

Endpoints esperados:
GET /api/crm/foundation/sprint-9/productive-route-dry-run-trial

POST /api/crm/foundation/sprint-9/productive-route-dry-run-trial/probe

El probe debe:
- retornar 423 por defecto.
- requerir flag explícito NonProduction.
- no registrar rutas productivas por defecto.
- no ejecutar CRUD real.
- no ejecutar dominio real.
- no escribir en base.
- no permitir side effects.
- no habilitar DELETE.
- no leer Authorization headers por defecto.
- no leer tokens.
- no usar [Authorize] productivo.
- no activar Portal Auth enforcement real.
- devolver solo metadata sanitizada.

Flag esperado:
Crm:RuntimeTrials:ProductiveRouteDryRunEnabled=false

Rutas productivas negativas esperadas por defecto:
- GET /api/crm/leads => 404
- GET /api/crm/accounts => 404
- GET /api/crm/contacts => 404
- POST /api/crm/leads => 404
- POST /api/crm/accounts => 404
- POST /api/crm/contacts => 404
- DELETE /api/crm/leads/{id} => 404
- DELETE /api/crm/accounts/{id} => 404
- DELETE /api/crm/contacts/{id} => 404

Reglas críticas:
- No subir secretos reales.
- No subir .env.
- No subir tokens.
- No subir certificados reales.
- No subir URLs privadas reales.
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
- No activar EF productivo.
- No ejecutar migrations.
- No crear schema.
- No activar rutas productivas por defecto.
- No CRUD productivo real.
- No DELETE.
- No side effects.
- No escritura en base.
- No UI productiva.
- No SQL Server propio.
- No datos reales.
- No usar secretos fuera del metadata-only contract de Sprint 9 P2.
- No usar DB fuera del metadata-only contract de Sprint 9 P3.
- No usar Auth fuera del metadata-only contract de Sprint 9 P4.

Archivos permitidos:
- docs/api/crm-sprint-9-p5-productive-route-dry-run-trial.md
- docs/api/crm-productive-route-dry-run-trial-policy.md
- docs/api/crm-productive-route-dry-run-trial-contract.md
- docs/api/crm-productive-route-dry-run-trial-redaction.md
- docs/operations/crm-productive-route-dry-run-trial-runbook.md
- docs/operations/crm-productive-route-dry-run-trial-rollback.md
- docs/architecture/crm-productive-route-dry-run-trial-architecture.md
- src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialContracts.cs
- src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialStatusService.cs
- src/CRM.Application/Foundation/CrmProductiveRouteDryRunTrialEvaluator.cs
- src/CRM.Api/ProductiveRoutes/ProductiveRouteDryRunTrialOptions.cs
- src/CRM.Api/ProductiveRoutes/ProductiveRouteDryRunTrialResult.cs
- src/CRM.Api/ProductiveRoutes/ProductiveRouteDryRunTrialService.cs
- src/CRM.Api/Program.cs
- tests/CRM.UnitTests/CrmProductiveRouteDryRunTrialStatusServiceTests.cs
- tests/CRM.UnitTests/ProductiveRouteDryRunTrialServiceTests.cs
- tests/CRM.ArchitectureTests/ProductiveRouteDryRunTrialArchitectureTests.cs
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
  - /api/crm/foundation/sprint-9/productive-route-dry-run-trial
- probe check:
  - POST /api/crm/foundation/sprint-9/productive-route-dry-run-trial/probe debe retornar 423 por defecto.
- negative route checks:
  - GET /api/crm/leads debe retornar 404 por defecto.
  - GET /api/crm/accounts debe retornar 404 por defecto.
  - GET /api/crm/contacts debe retornar 404 por defecto.
  - POST /api/crm/leads debe retornar 404 por defecto.
  - POST /api/crm/accounts debe retornar 404 por defecto.
  - POST /api/crm/contacts debe retornar 404 por defecto.
  - DELETE /api/crm/leads/{id} debe retornar 404 por defecto.
  - DELETE /api/crm/accounts/{id} debe retornar 404 por defecto.
  - DELETE /api/crm/contacts/{id} debe retornar 404 por defecto.

PR esperado:
Branch:
crm-sprint-9-p5-productive-route-dry-run-trial

Title:
feat: add crm productive route dry run trial

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
Productive Route Dry Run Trial Documentation:
Application Contracts:
Application Service:
Route Dry Run Evaluator:
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
