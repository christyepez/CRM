# Portal Integration Contracts

## Proposito

Este archivo define los contratos que CRM debe usar para integrarse con PortalCorporativo.

## Portal base

Repositorio: https://github.com/christyepez/PortalCorporativo

## Capacidades reutilizables

CRM debe reutilizar del portal:

- Security
- Users
- Roles
- Permissions
- Menu
- Theme
- Configuration
- Catalogs
- Audit
- Notifications
- Documents
- Reporting

## Reglas

- CRM no crea seguridad propia.
- CRM no crea auditoria propia.
- CRM no crea notificaciones propias.
- CRM no crea menu propio.
- CRM no crea tema propio.
- CRM no guarda documentos por fuera del componente documental del portal.
- CRM valida permisos usando el portal.
- CRM registra acciones criticas usando auditoria del portal.

## Permisos CRM sugeridos

- CRM.CUSTOMERS.VIEW
- CRM.CUSTOMERS.CREATE
- CRM.CUSTOMERS.UPDATE
- CRM.CUSTOMERS.DELETE
- CRM.LEADS.VIEW
- CRM.LEADS.CREATE
- CRM.LEADS.CONVERT
- CRM.OPPORTUNITIES.VIEW
- CRM.OPPORTUNITIES.CREATE
- CRM.OPPORTUNITIES.CHANGE_STAGE
- CRM.CASES.VIEW
- CRM.CASES.CLOSE
- CRM.INTEGRATIONS.VIEW
- CRM.INTEGRATIONS.CONFIGURE
- CRM.INTEGRATIONS.RETRY

## Menu CRM sugerido

- CRM
- Dashboard
- Clientes
- Contactos
- Leads
- Oportunidades
- Actividades
- Casos
- Campanas
- Integraciones CRM

## Grids CRM sugeridos

- CRM_CUSTOMERS_GRID
- CRM_CONTACTS_GRID
- CRM_LEADS_GRID
- CRM_OPPORTUNITIES_GRID
- CRM_CASES_GRID
- CRM_INTEGRATION_TRANSACTIONS_GRID

## Interfaces recomendadas

- IPortalSecurityClient
- IPortalPermissionClient
- IPortalConfigurationClient
- IPortalCatalogClient
- IPortalAuditClient
- IPortalNotificationClient
- IPortalDocumentClient
