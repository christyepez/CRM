# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 9 - P5 Productive Route Dry Run Trial

Base Main Commit:
3da901f1d00fae351af1f4df60e80ad906cc9cf6

Branch:
crm-sprint-9-p5-productive-route-dry-run-trial

Commit sugerido:
feat: add crm productive route dry run trial

PR title:
feat: add crm productive route dry run trial

Objetivo:
Implementar el dry-run controlado de rutas productivas CRM únicamente para NonProduction, disabled/fail-closed por defecto, sin activar CRUD productivo, sin DELETE, sin side effects, sin DB productiva y sin enforcement real de autorización.

Guardrails:
- No production activation.
- No CRUD productivo real.
- No DELETE.
- No side effects.
- No escritura en base.
- No DB runtime productivo.
- No EF productivo.
- No migrations.
- No schema changes.
- No Portal Auth enforcement real.
- No lectura de Authorization headers por defecto.
- No lectura de tokens por defecto.
- No token storage.
- No [Authorize] productivo.
- No login/logout CRM.
- No Identity propio.
- No UI productiva.
- No datos reales.
- Productive routes siguen 404 por defecto.
- Dry-run probe 423 por defecto.

Prompt File:
codex/prompts/sprint-9-p5-productive-route-dry-run-trial.md
