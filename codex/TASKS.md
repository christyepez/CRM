# TASKS.md

## Proposito

Backlog inicial para que Codex implemente CRM por fases y agentes.

## Fase 0 - Coordinacion

- Crear y validar estructura del repositorio.
- Validar documentos Codex.
- Validar dependencias con PortalCorporativo.
- Actualizar docs/coordination.

## Fase 1 - Contratos Portal

- Validar APIs reales del portal.
- Crear clientes/adapters hacia servicios del portal.
- Crear contratos para seguridad, permisos, menu, configuracion, catalogos, auditoria, notificaciones y documentos.
- Registrar pendientes en open-issues.md.

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
- Integrar auditoria del portal.
- Integrar notificaciones del portal.

## Fase 3 - Base de Datos

- Crear modelo CRM.
- Crear scripts SQL.
- Crear seed inicial CRM.
- Crear modelo Integration Hub.

## Fase 4 - Frontend CRM

- Crear modulo Angular CRM.
- Crear rutas CRM.
- Crear pantallas de clientes, leads, oportunidades y casos.
- Usar menu, tema, permisos, grids y formularios del portal.

## Fase 5 - CRM Integration Hub

- Crear ExternalSystem.
- Crear IntegrationTransaction.
- Crear EntityMapping.
- Crear FieldMapping.
- Crear ExternalEntityReference.
- Crear Outbox e Inbox.
- Crear GenericRestConnector.
- Crear stubs Salesforce y Dynamics.

## Fase 6 - Workers

- Procesar Outbox.
- Procesar Inbox.
- Procesar reintentos.
- Procesar integraciones.

## Fase 7 - Docker

- Crear compose CRM.
- Documentar ejecucion junto al portal.
- Validar variables de entorno.

## Fase 8 - QA

- Crear pruebas de dominio.
- Crear pruebas API.
- Crear pruebas de contrato contra portal.
- Crear pruebas Integration Hub.
