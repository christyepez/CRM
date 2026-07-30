# CRM Sprint 9 P6 - Sprint 9 Gate Decision

Actúa como Codex Task Runner + Architecture Governance Agent + Security Agent + DevOps Agent + QA Lead Agent + Documentation Agent + Release Gate Agent para el proyecto CRM.

Repository:
christyepez/CRM

GitHub Repository:
https://github.com/christyepez/CRM

Phase:
CRM Sprint 9 - P6 Sprint 9 Gate Decision

Base obligatoria:
eea6d3ef8f96f3571908ee3a9e5e1307a0e07ffc

Objetivo:
Cerrar formalmente Sprint 9 con una decisión de gate documentada y expuesta por foundation endpoint, consolidando la evidencia de P1 a P5 y definiendo si CRM puede avanzar a Sprint 10.

Contexto Sprint 9:
P1 - Controlled Runtime Activation Decision:
- NonProduction trials approved for planning only.
- Production activation: NoGo.
- Runtime enabled-now flags: false.

P2 - Secret Provider Runtime Enablement Trial:
- Trial existe.
- Disabled/fail-closed por defecto.
- Metadata-only.
- No secret values exposed/logged/persisted/cached.
- Probe 423 por defecto.

P3 - Common DB Runtime Connectivity Trial:
- Trial existe.
- Disabled/fail-closed por defecto.
- Metadata-only.
- No connection strings exposed/logged/persisted/cached.
- No schema, migrations, EF runtime, DB writes or productive persistence.
- Probe 423 por defecto.

P4 - Portal Auth Runtime Validation Trial:
- Trial existe.
- Disabled/fail-closed por defecto.
- Metadata-only.
- No Auth productivo.
- No login/logout CRM.
- No Identity propio.
- No Authorization header/token reads by default.
- No Portal HTTP by default.
- Probe 423 por defecto.

P5 - Productive Route Dry Run Trial:
- Trial existe.
- Disabled/fail-closed por defecto.
- Metadata-only.
- Productive routes remain 404 by default.
- Probe 423 por defecto.
- No CRUD real, no DELETE, no side effects, no DB writes, no Auth enforcement.

Decisión esperada:
- OverallSprint9Decision: GoForSprint10ControlledProductizationReadinessPlanning.
- ProductionActivationDecision: NoGo.
- SecretProviderRuntimeTrialDecision: GoOnlyAsExplicitNonProductionTrial.
- CommonDbRuntimeConnectivityTrialDecision: GoOnlyAsExplicitNonProductionTrial.
- PortalAuthRuntimeValidationTrialDecision: GoOnlyAsExplicitNonProductionTrial.
- ProductiveRouteDryRunTrialDecision: GoOnlyAsExplicitNonProductionDryRun.
- ProductiveRouteRegistrationDecision: NoGoByDefault.
- ProductiveCrudDecision: NoGo.
- DeleteDecision: NoGo.
- DbRuntimeDecision: NoGoForProduction.
- PortalAuthEnforcementDecision: NoGoForProduction.
- ProductizationStatus: NotReady.
- NextGate: Sprint10P1ProductizationReadinessDecision.

Comportamiento por defecto esperado:
- Sprint9GateDecisionExists: true.
- Sprint9GateDecisionApproved: true.
- Sprint9Closed: true.
- Sprint9EvidenceComplete: true.
- Sprint9P1Complete: true.
- Sprint9P2Complete: true.
- Sprint9P3Complete: true.
- Sprint9P4Complete: true.
- Sprint9P5Complete: true.
- ProductionActivationApproved: false.
- RuntimeActivationApprovedForProduction: false.
- ProductiveRoutesApprovedByDefault: false.
- ProductiveCrudApproved: false.
- DeleteApproved: false.
- DatabaseWritesApproved: false.
- EfRuntimeApproved: false.
- MigrationsApproved: false.
- SchemaChangesApproved: false.
- PortalAuthEnforcementApproved: false.
- TokenHeaderReadsApproved: false.
- LoginLogoutApproved: false.
- IdentityRuntimeApproved: false.
- ProductiveUiApproved: false.
- NonProductionTrialsRemainAllowedOnlyWithExplicitFlags: true.
- AllTrialsFailClosedByDefault: true.
- AllObservabilityMetadataOnly: true.
- RollbackAvailable: true.
- ProductizationStatus: NotReady.
- NextGate: Sprint10P1ProductizationReadinessDecision.

Endpoint esperado:
GET /api/crm/foundation/sprint-9/gate-decision

Este endpoint debe:
- ser GET-only.
- devolver decisión documental/foundation status.
- no activar runtime.
- no hacer probes.
- no leer secretos.
- no leer DB.
- no leer Portal.
- no leer headers/tokens.
- no registrar rutas productivas.
- no ejecutar dominio.
- no escribir en base.
- no permitir side effects.

No agregar POST probe para P6, salvo que ya exista un patrón documental estrictamente necesario. Preferencia: P6 solo GET status.

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
- Productive routes deben seguir 404 por defecto.
- P2/P3/P4/P5 probes deben seguir 423 por defecto.

Archivos permitidos:
- docs/roadmap/crm-sprint-9-gate-decision.md
- docs/roadmap/crm-sprint-9-go-no-go.md
- docs/roadmap/crm-sprint-9-evidence-summary.md
- docs/roadmap/crm-sprint-9-risk-register.md
- docs/roadmap/crm-sprint-9-release-notes.md
- docs/operations/crm-sprint-9-gate-decision-runbook.md
- docs/architecture/crm-sprint-9-gate-decision-architecture.md
- src/CRM.Application/Foundation/CrmSprint9GateDecisionContracts.cs
- src/CRM.Application/Foundation/CrmSprint9GateDecisionStatusService.cs
- src/CRM.Api/Program.cs
- tests/CRM.UnitTests/CrmSprint9GateDecisionStatusServiceTests.cs
- tests/CRM.ArchitectureTests/Sprint9GateDecisionArchitectureTests.cs
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
  - /api/crm/foundation/sprint-9/controlled-runtime-activation-decision
  - /api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial
  - /api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial
  - /api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial
  - /api/crm/foundation/sprint-9/productive-route-dry-run-trial
  - /api/crm/foundation/sprint-9/gate-decision
- probe checks:
  - P2 probe debe retornar 423 por defecto.
  - P3 probe debe retornar 423 por defecto.
  - P4 probe debe retornar 423 por defecto.
  - P5 probe debe retornar 423 por defecto.
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
crm-sprint-9-p6-sprint-9-gate-decision

Title:
docs: close crm sprint 9 gate decision

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
Sprint 9 Gate Decision Documentation:
Application Contracts:
Application Service:
API Foundation Endpoint:
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
Sprint 9 Decision:
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
