# AGENTS.md

## Proposito

Definir agentes logicos para que Codex implemente el CRM de forma paralela, controlada y alineada con `PortalCorporativo`.

## Lectura obligatoria

Antes de modificar codigo, Codex debe leer:

- `README.md`
- `codex/COORDINADOR_SOLUCION.md`
- `codex/INSTRUCTIONS.md`
- `codex/ARCHITECTURE_RULES.md`
- `codex/PORTAL_INTEGRATION_CONTRACTS.md`
- `codex/TASKS.md`

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

Valida como CRM consume APIs reutilizables del portal.

Contratos:

- Seguridad
- Permisos
- Menu
- Configuracion
- Catalogos
- Auditoria
- Notificaciones
- Archivos
- Reporting
- Integration API

### Agent 02 - CRM Backend Core

Implementa el backend CRM: Customers, Contacts, Leads, Opportunities, Activities, Cases, Campaigns, Interactions y Documents metadata.

### Agent 03 - CRM Database

Implementa modelo SQL CRM e Integration Hub.

### Agent 04 - CRM Frontend

Implementa el modulo Angular CRM integrado al shell del portal.

### Agent 05 - CRM Integration Hub

Implementa conectores, mapeos, transacciones, outbox/inbox, stubs Salesforce/Dynamics y Generic REST Connector.

### Agent 06 - CRM Workers

Procesa outbox, inbox, reintentos, notificaciones e integracion externa.

### Agent 07 - DevOps CRM

Gestiona Docker Compose, variables, scripts y despliegue local junto al portal.

### Agent 08 - QA CRM

Gestiona pruebas unitarias, integracion, contratos con portal, outbox/inbox y frontend.

### Agent 09 - Documentation

Consolida documentacion tecnica, funcional, APIs, despliegue y guias para Codex.

## Reglas generales

- Reutilizar APIs transversales del portal.
- No crear seguridad aislada para CRM.
- No crear auditoria aislada para CRM.
- No crear notificaciones aisladas para CRM.
- No quemar menus, colores, logos, permisos, grids, formularios, botones ni catalogos.
- No acoplar CRM Core directamente con Salesforce o Dynamics.
- Usar `CRM Integration Hub` para integraciones externas.
- Registrar dependencias en `docs/coordination/dependencies.md`.
