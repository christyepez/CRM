# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 9 - P3 Common DB Runtime Connectivity Trial

Base Main Commit:
84e2496bc66f585890077ce143b6b1d25e0bf284

Branch:
crm-sprint-9-p3-common-db-runtime-connectivity-trial

Commit sugerido:
feat: add crm common db runtime connectivity trial

PR title:
feat: add crm common db runtime connectivity trial

Objetivo:
Implementar el trial controlado de conectividad Common DB únicamente para NonProduction, disabled/fail-closed por defecto, usando metadata sanitizada del Secret Provider Sprint 9 P2 sin exponer connection strings ni activar persistencia productiva.

Guardrails:
- No production activation.
- No DB runtime productivo.
- No EF runtime productivo.
- No migrations.
- No schema changes.
- No CRUD productivo.
- No DELETE.
- No connection strings en API/logs/docs.
- No secretos reales.
- No .env.
- No datos reales.
- No Portal Auth runtime.
- No rutas productivas por defecto.
- Probe 423 por defecto.

Prompt File:
codex/prompts/sprint-9-p3-common-db-runtime-connectivity-trial.md
