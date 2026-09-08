# CRM Next Codex Task

Repository:
christyepez/CRM

Phase:
CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules

Base Main Commit:
Sprint 14 P1 merge commit required

Branch:
crm-sprint-14-s14-01-opportunity-pipeline-contracts-domain-rules

Commit sugerido:
feat(crm): add opportunity pipeline contracts and domain rules

PR title:
CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules

Objetivo:
Implementar contratos y reglas deterministicas foundation-only para Opportunity Pipeline.

Guardrails:
- Do not add Opportunity application service yet.
- Do not add Opportunity foundation store yet.
- Do not add Opportunity API routes yet.
- Do not add Angular Opportunity UI yet.
- No productive Opportunity API activation.
- No `/api/crm/opportunities` route.
- No `/api/crm/foundation/opportunities` route.
- No DELETE.
- No Lead conversion.
- No Account Management activation.
- No assignment, owner or Portal user feature activation.
- No DB runtime productivo ni Common DB activation.
- No EF runtime.
- No migrations.
- No schema changes.
- No Portal Auth runtime activation.
- No Authorization header/token reads by default.
- No CRM-owned Identity/login.
- No secrets, `.env`, tokens, certificates or real data.
- Keep simulated Production baseline untouched.
- Do not touch `crm-prod-sim`.
- Do not reopen Sprint 10 Production gates.

Prompt File:
codex/prompts/sprint-14-opportunity-pipeline-s14-01.md

Acceptance Criteria:
- Opportunity Pipeline domain contracts exist.
- Deterministic stage ordering and transition policy exists.
- Create, update, progress, win and loss rules are explicit.
- Synthetic relationship references remain contract-only.
- No Opportunity service, store, API route or UI is added.
- Productive Opportunity routes remain unavailable.
- DELETE is not added.
- Portal/Common DB remain disabled.
- Lead conversion, Account activation and assignment remain deferred.
- Guardrails pass.
