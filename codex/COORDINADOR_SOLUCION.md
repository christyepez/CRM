# Coordinador de Solucion CRM

## Proposito

Este documento coordina la implementacion del CRM como modulo integrado al repositorio PortalCorporativo.

Repo CRM: https://github.com/christyepez/CRM

Repo Portal: https://github.com/christyepez/PortalCorporativo

## Decision principal

PortalCorporativo es la plataforma transversal. CRM es un modulo funcional. CRM Integration Hub es el modulo tecnico para integraciones externas con CRMs de mercado.

## Orden de ejecucion

1. Gobierno y estructura del repo CRM.
2. Contratos de integracion con PortalCorporativo.
3. Backend CRM Core.
4. Modelo de datos CRM.
5. Frontend CRM integrado al portal.
6. CRM Integration Hub.
7. Workers CRM.
8. Docker y ejecucion conjunta.
9. QA, contratos y documentacion.

## Reutilizacion obligatoria

CRM debe reutilizar del portal:

- Security API.
- Menu API.
- Configuration API.
- Catalog API.
- Audit API.
- Notification API.
- Content/File API.
- Reporting API.
- Integration API base.

## Componentes que CRM no debe reconstruir

- Usuarios.
- Roles.
- Permisos.
- Autenticacion.
- Menus.
- Temas.
- Logos.
- Parametros.
- Catalogos.
- Grids.
- Formularios.
- Botones.
- Auditoria.
- Notificaciones.
- Archivos.
- Reporteria transversal.

## Regla final

Reutilizar antes que crear. Integrar mediante contratos antes que acoplar. Parametrizar antes que quemar en codigo. Auditar acciones criticas. Notificar eventos relevantes. Mantener compatibilidad con PortalCorporativo.
