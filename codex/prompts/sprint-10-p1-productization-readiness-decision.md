# CRM Sprint 10 P1 - Productization Readiness Decision

Actúa como Codex Task Runner + Architecture Governance Agent + Security Agent + DevOps Agent + QA Lead Agent + Documentation Agent + Release Gate Agent para el proyecto CRM.

Repository:
christyepez/CRM

GitHub Repository:
https://github.com/christyepez/CRM

Phase:
CRM Sprint 10 - P1 Productization Readiness Decision

Base obligatoria:
1c711833d7fcce4744f04aac88c40a6783c2a3b8

Objetivo:
Crear una decisión formal de productization readiness para Sprint 10, usando la evidencia de Sprint 9 P1-P6 y definiendo si CRM puede avanzar a activaciones controladas NonProduction de productización.

Contexto:
Sprint 9 quedó cerrado con:
- OverallSprint9Decision: GoForSprint10ControlledProductizationReadinessPlanning.
- ProductionActivationDecision: NoGo.
- ProductizationStatus: NotReady.
- NextGate: Sprint10P1ProductizationReadinessDecision.

Decisión esperada:
- Sprint10P1Decision: GoForControlledNonProductionProductizationPreparation.
- ProductionActivationDecision: NoGo.
- ProductiveRuntimeActivationDecision: NoGoForProduction.
- CommonDbControlledActivationDecision: GoOnlyAsExplicitNonProductionPreparation.
- PortalAuthControlledActivationDecision: GoOnlyAsExplicitNonProductionPreparation.
- ProductiveRouteControlledActivationDecision: GoOnlyAsExplicitNonProductionPreparation.
- ProductiveCrudPilotDecision: NoGoUntilP5.
- ProductiveUiDecision: NoGo.
- ProductizationStatus: PreparationOnly.
- NextGate: Sprint10P2CommonDbControlledActivationPlan.

Comportamiento por defecto esperado:
- Sprint10P1ProductizationReadinessDecisionExists: true.
- Sprint10P1Approved: true.
- Sprint9GateReviewed: true.
- Sprint9ProductionNoGoPreserved: true.
- ProductionActivationApproved: false.
- ProductiveRuntimeActivationApprovedForProduction: false.
- CommonDbControlledPreparationApproved: true.
- PortalAuthControlledPreparationApproved: true.
- ProductiveRouteControlledPreparationApproved: true.
- ProductiveCrudPilotApproved: false.
- ProductiveUiApproved: false.
- NonProductionOnly: true.
- ExplicitFlagsRequired: true.
- FailClosedByDefault: true.
- ObservabilityMetadataOnly: true.
- RollbackAvailable: true.
- ProductizationStatus: PreparationOnly.
- NextGate: Sprint10P2CommonDbControlledActivationPlan.

Endpoint esperado:
GET /api/crm/foundation/sprint-10/productization-readiness-decision

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

No agregar POST probe para P1.

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

Archivos permitidos:
- docs/roadmap/crm-sprint-10-productization-readiness-decision.md
- docs/roadmap/crm-sprint-10-go-no-go.md
- docs/roadmap/crm-sprint-10-risk-register.md
- docs/roadmap/crm-sprint-10-recommended-path.md
- docs/operations/crm-sprint-10-productization-readiness-runbook.md
- docs/architecture/crm-sprint-10-productization-readiness-architecture.md
- src/CRM.Application/Foundation/CrmSprint10ProductizationReadinessDecisionContracts.cs
- src/CRM.Application/Foundation/CrmSprint10ProductizationReadinessDecisionStatusService.cs
- src/CRM.Api/Program.cs
- tests/CRM.UnitTests/CrmSprint10ProductizationReadinessDecisionStatusServiceTests.cs
- tests/CRM.ArchitectureTests/Sprint10ProductizationReadinessDecisionArchitectureTests.cs
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
  - /api/crm/foundation/sprint-9/gate-decision
  - /api/crm/foundation/sprint-10/productization-readiness-decision
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
crm-sprint-10-p1-productization-readiness-decision

Title:
docs: add crm sprint 10 productization readiness decision

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
Sprint 10 Productization Readiness Documentation:
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
Sprint 10 Decision:
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
