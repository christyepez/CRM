# Contratos de integración CRM con PortalCorporativo

## Propósito

Definir cómo el repositorio `CRM` debe consumir, extender o adaptar las capacidades transversales de `PortalCorporativo`.

## Repositorios

```text
PortalCorporativo: https://github.com/christyepez/PortalCorporativo
CRM: https://github.com/christyepez/CRM
CodexCommonAgents: https://github.com/christyepez/CodexCommonAgents
```

## Regla central

Antes de crear cualquier componente CRM, Codex debe validar si existe una capacidad reutilizable en PortalCorporativo.

Clasificación obligatoria:

```text
REUSE   = usar componente del portal.
EXTEND  = extender configuración, permisos, catálogos, menús o contratos del portal.
ADAPT   = crear adaptador hacia API/servicio del portal.
CREATE  = crear componente propio del dominio CRM.
BLOCKED = no implementar hasta resolver revisión del portal.
```

## Matriz de integración

| Capacidad CRM | PortalCorporativo | Clasificación | Criterio |
|---|---|---|---|
| Autenticación | Security API | REUSE | CRM no implementa login propio. |
| Usuarios | Security API | REUSE | CRM consume identidad del portal. |
| Roles CRM | Security API | EXTEND | Registrar roles/permisos CRM. |
| Permisos CRM | Security API | EXTEND | Validación real en backend. |
| Menú CRM | Menu API | EXTEND | Registrar menús dinámicos CRM. |
| Grids CRM | Configuration API | EXTEND | Columnas, filtros y acciones configurables. |
| Formularios CRM | Configuration API | EXTEND | Metadata configurable. |
| Catálogos CRM | Catalog API | EXTEND/CREATE | Globales en portal; específicos en CRM. |
| Auditoría CRM | Audit API | ADAPT | Publicar eventos auditables. |
| Notificaciones CRM | Notification API | ADAPT | Notificar eventos comerciales relevantes. |
| Archivos CRM | Content/File API | ADAPT | Guardar documentos vía portal si aplica. |
| Reportes CRM | Reporting API | EXTEND | Publicar datasets/reportes CRM. |
| Integraciones externas | Integration API + CRM Integration Hub | ADAPT/CREATE | Salesforce/Dynamics no acoplados al core. |
| Outbox CRM | SQL Outbox / Workers | EXTEND/CREATE | Reusar patrón del portal. |

## Componentes propios CRM

Se permite crear:

- Customers.
- Contacts.
- Leads.
- Opportunities.
- Activities.
- Cases.
- Campaigns.
- Interactions.
- CRM Documents metadata.
- CRM Integration Hub.
- Connectors Salesforce/Dynamics/Generic REST.
- Mapeos de integración.
- Outbox/Inbox CRM cuando el dominio lo requiera.

## Prohibido duplicar

No crear en CRM:

- Login.
- Usuarios globales.
- Roles globales.
- Menú engine.
- Configuración visual global.
- Auditoría transversal.
- Notificaciones transversales.
- Gestión genérica de archivos.
- API Gateway propio.

## Salida obligatoria de Codex

Cada implementación debe reportar:

```text
Agent:
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
