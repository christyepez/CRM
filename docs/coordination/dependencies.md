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

## Sprint 14 P1

| Dependencia | Estado |
|---|---|
| Lead Qualification | Cerrado desde Sprint 11; solo referencia sintetica permitida. |
| Contact Management | Cerrado desde Sprint 12; solo referencia sintetica permitida. |
| Activity / Follow-Up | Cerrado desde Sprint 13; relacion Opportunity -> Activity diferida y sin acoplamiento runtime en P1. |
| Opportunity Pipeline | Baseline y backlog definidos; S14-01 debe crear contratos y reglas de dominio. |
| Pipeline / PipelineStage | Conceptual/catalogo solamente; requiere reglas deterministicas en S14-01. |
| Account Management | Diferido; no activar en S14 P1 ni S14-01. |
| Assignment / Portal users | Diferido hasta Portal Auth runtime aprobado. |
| Common DB runtime | Deshabilitado; sin EF, migraciones ni schema changes. |
