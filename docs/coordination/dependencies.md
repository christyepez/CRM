# Dependencias

## PortalCorporativo

| Dependencia | Estado |
|---|---|
| Security API | Pendiente de validar |
| Menu API | Pendiente de validar |
| Configuration API | Pendiente de validar |
| Catalog API | Pendiente de validar |
| Audit API | Pendiente de validar |
| Notification API | Pendiente de validar |
| Content API | Pendiente de validar |

## Regla

Cuando una dependencia del portal no este lista, crear adapter o stub y registrar pendiente.

## Sprint 13 Closure

| Dependencia | Estado |
|---|---|
| Activity Lead target seam | Foundation-only validado con `ILeadFoundationStore`; sin Lead conversion. |
| Activity Contact target seam | Foundation-only validado con `IContactFoundationStore`; sin datos reales. |
| Account Management | Diferido; no activado por S13. |
| Opportunity Pipeline | Seleccionado como siguiente capacidad de negocio para Sprint 14 P1; no activado por S13. |
| Assignment / Portal users | Diferido hasta Portal Auth runtime aprobado. |
| Common DB runtime | Deshabilitado; sin EF, migraciones ni schema changes. |
