# Reglas de Arquitectura CRM

## Integracion con PortalCorporativo

CRM depende de contratos hacia el portal y reutiliza capacidades transversales.

## Contextos CRM

- CrmCore
- CrmIntegrations
- CrmReporting
- CrmWorkers

## Contextos del portal usados por CRM

- Identity
- Authorization
- Menu
- Configuration
- Catalog
- Audit
- Notification
- Content
- Reporting
- Integration

## Backend

Usar Clean Architecture con Domain, Application, Infrastructure, Api y Contracts.

## Frontend

Usar modulo Angular de CRM dentro del portal.

## Parametrizacion

Menus, acciones, columnas, grids, formularios, catalogos, estados, colores, logos y permisos visibles deben manejarse como configuracion.

## Mensajeria

Primera version: SQL Outbox, SQL Inbox, Workers y reintentos.
