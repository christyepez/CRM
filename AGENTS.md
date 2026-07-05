# AGENTS.md

## Propósito

Definir agentes lógicos para que Codex implemente el CRM de forma paralela, controlada y alineada con `PortalCorporativo` y `CodexCommonAgents`.

## Repositorios relacionados

```text
PortalCorporativo: https://github.com/christyepez/PortalCorporativo
CRM: https://github.com/christyepez/CRM
CodexCommonAgents: https://github.com/christyepez/CodexCommonAgents
```

## Lectura obligatoria

Antes de modificar código, Codex debe leer:

- `README.md`
- `codex/COORDINADOR_SOLUCION.md`
- `codex/INSTRUCTIONS.md`
- `codex/ARCHITECTURE_RULES.md`
- `codex/PORTAL_INTEGRATION_CONTRACTS.md`
- `codex/TASKS.md`
- `PortalCorporativo/codex/REUSABLE_CAPABILITIES.md` si está disponible
- `CodexCommonAgents/AGENTS.md` y playbook aplicable cuando esté disponible

No leer todo el repositorio si la tarea no lo requiere.

## Agentes

### Agent 00 - Solution Coordinator

Coordina fases, dependencias, backlog, estado, riesgos y entregables.

Carpetas permitidas:

```text
/
codex/
docs/coordination/
docs/architecture/
```

### Agent 01 - Portal Integration Architect

Valida cómo CRM consume APIs reutilizables del portal.

Contratos:

- Seguridad.
- Permisos.
- Menú.
- Configuración.
- Catálogos.
- Auditoría.
- Notificaciones.
- Archivos.
- Reporting.
- Integration API.

### Agent 02 - CRM Backend Core

Implementa el backend CRM: Customers, Contacts, Leads, Opportunities, Activities, Cases, Campaigns, Interactions y Documents metadata.

### Agent 03 - CRM Database

Implementa modelo SQL CRM e Integration Hub.

### Agent 04 - CRM Frontend

Implementa el módulo Angular CRM integrado al shell del portal.

### Agent 05 - CRM Integration Hub

Implementa conectores, mapeos, transacciones, outbox/inbox, stubs Salesforce/Dynamics y Generic REST Connector.

### Agent 06 - CRM Workers

Procesa outbox, inbox, reintentos, notificaciones e integración externa.

### Agent 07 - DevOps CRM

Gestiona Docker Compose, variables, scripts y despliegue local junto al portal.

### Agent 08 - QA CRM

Gestiona pruebas unitarias, integración, contratos con portal, outbox/inbox y frontend.

### Agent 09 - Documentation

Consolida documentación técnica, funcional, APIs, despliegue y guías para Codex.

## Reglas generales

- Reutilizar APIs transversales del portal.
- Clasificar cada componente como REUSE, EXTEND, ADAPT, CREATE o BLOCKED.
- No crear seguridad aislada para CRM.
- No crear auditoría aislada para CRM.
- No crear notificaciones aisladas para CRM.
- No quemar menús, colores, logos, permisos, grids, formularios, botones ni catálogos.
- No acoplar CRM Core directamente con Salesforce o Dynamics.
- Usar `CRM Integration Hub` para integraciones externas.
- Registrar dependencias en `docs/coordination/dependencies.md`.

## Salida esperada

```text
Agent:
Task:
Portal Capability Checked:
Reuse Classification:
Portal Components Reused:
Portal Components Extended:
New CRM Components Created:
Files Created:
Files Modified:
Tests Added:
Risks:
Next Step:
```
