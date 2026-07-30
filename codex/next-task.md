# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 9 - P4 Portal Auth Runtime Validation Trial

Base Main Commit:
25a0951c7bd1d342a7a83676619f4349d036d326

Branch:
crm-sprint-9-p4-portal-auth-runtime-validation-trial

Commit sugerido:
feat: add crm portal auth runtime validation trial

PR title:
feat: add crm portal auth runtime validation trial

Objetivo:
Implementar el trial controlado de validación Portal Auth únicamente para NonProduction, disabled/fail-closed por defecto, sin activar Auth productivo, sin login propio, sin token storage y sin leer Authorization headers por defecto.

Guardrails:
- No production activation.
- No Auth productivo.
- No login/logout CRM.
- No Identity propio.
- No token storage.
- No lectura de Authorization header por defecto.
- No [Authorize] productivo.
- No Portal HTTP por defecto.
- No URLs privadas reales.
- No client secrets reales.
- No certificados.
- No DB runtime productivo.
- No CRUD productivo.
- No DELETE.
- No UI productiva.
- Probe 423 por defecto.

Prompt File:
codex/prompts/sprint-9-p4-portal-auth-runtime-validation-trial.md
