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

## Sprint 14 S14-04

| Dependencia | Estado |
|---|---|
| Portal Menu API | EXTEND diferido; se agrega entrada local foundation mientras el menu runtime del portal sigue pendiente. |
| Portal Configuration API | EXTEND diferido; catalogo sintetico de etapas definido en frontend foundation sin consumir catalogos reales. |
| Opportunity Foundation API | CREATE usado exclusivamente por `/foundation/opportunities` mediante `/api/crm/foundation/opportunities`. |
| Productive Opportunity API | No activado; sin referencias frontend a `/api/crm/opportunities`. |
| Lead conversion | Diferido; no implementado ni invocado por la pagina Opportunity Pipeline. |
| Account Management runtime | Diferido; `AccountName` se captura como valor foundation sin activar cuentas productivas. |
| Assignment / Portal users | Diferido hasta Portal Auth runtime aprobado; no owner runtime. |
| Common DB runtime | Deshabilitado; sin EF, migraciones, schema changes, SQL ni datos reales. |
| Simulated Production | No tocado por S14-04; `crm-prod-sim` permanece fuera de alcance. |

## Sprint 14 S14-05

| Dependencia | Estado |
|---|---|
| PortalCorporativo reusable capabilities | No disponible en el workspace local; guardrail registrado y sin activacion runtime. |
| CodexCommonAgents playbook | No disponible en el workspace local; se aplican instrucciones locales de `AGENTS.md`. |
| Portal Catalog API | EXTEND diferido; paridad validada contra catalogo sintetico foundation de Opportunity Pipeline. |
| Opportunity Foundation API | CREATE validado mediante pruebas de dominio, aplicacion, API y verificadores frontend. |
| Productive Opportunity API | No activado; `/api/crm/opportunities` permanece 404/no disponible. |
| Lead conversion | Diferido; no implementado ni invocado. |
| Account Management runtime | Diferido; no activado. |
| Assignment / Portal users | Diferido hasta Portal Auth runtime aprobado; no owner runtime. |
| Common DB runtime | Deshabilitado; sin EF, migraciones, schema changes, SQL ni datos reales. |
| Simulated Production | No tocado por S14-05; `crm-prod-sim` permanece fuera de alcance. |

## Sprint 14 S14-06

| Dependencia | Estado |
|---|---|
| Opportunity Foundation API | CREATE validado localmente por HTTP real bajo `/api/crm/foundation/opportunities`. |
| Opportunity Frontend Foundation | CREATE validado localmente en `/foundation/opportunities` mediante proxy a `http://localhost:8093`. |
| Productive Opportunity API | No activado; `/api/crm/opportunities` permanece no disponible. |
| DELETE | No activado; rutas DELETE productivas y foundation permanecen no disponibles. |
| Portal Auth runtime | REUSE diferido; no se observo token, header Authorization ni runtime Portal. |
| Common DB runtime | Deshabilitado; FoundationOnly con store in-memory, sin EF, migraciones, schema changes ni SQL. |
| Lead conversion | Diferido; no implementado ni invocado por S14-06. |
| Account Management runtime | Diferido; no activado por S14-06. |
| Assignment / Portal users | Diferido hasta Portal Auth runtime aprobado. |
| Simulated Production | No tocado por S14-06; `crm-prod-sim` en `8094` permanecio fuera de alcance. |

## Sprint 15 P1 - Campaign Management

| Dependencia | Estado |
|---|---|
| Campaign domain concept | Existe como record conceptual `Campaign(Id, Name, ActiveRange)`; debe inventariarse antes de ampliar. |
| Portal Catalog/Menu/Security | REUSE/EXTEND diferido; no activar runtime en P1. |
| Lead/Opportunity attribution | Potencial valor futuro; P1 debe decidir incluir o diferir sin acoplamiento productivo. |
| Common DB / Productive API | Deshabilitado; mantener FoundationOnly / NonProductionSeam. |
| Simulated Production | Fuera de alcance; no tocar `crm-prod-sim`. |
