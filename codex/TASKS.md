# TASKS.md

## CRM Sprint 1 P2 - Core Domain Discovery and API Contract Baseline

- [x] Validate GitHub main contains P1 commit.
- [x] Create pure CRM domain baseline.
- [x] Create application contract catalog.
- [x] Add non-mutating API contract endpoints.
- [x] Document domain, API and integration boundaries.
- [x] Add unit and architecture tests.
- [x] Keep DB/migrations/auth/token storage out of scope.

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
