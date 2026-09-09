# CRM Sprint 14 S14-05 - Opportunity Pipeline Test and Guardrail Hardening

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 14 S14-04 merge commit required
Branch: crm-sprint-14-s14-05-opportunity-pipeline-test-guardrail-hardening
Suggested commit: test(crm): harden opportunity pipeline foundation guardrails
PR title: CRM Sprint 14 S14-05 - Opportunity Pipeline Test and Guardrail Hardening

## Objective

Harden Opportunity Pipeline foundation tests and guardrails across domain, application, API and Angular frontend.

## Required validation

- Validate stage catalog parity and ordering across backend domain rules and frontend synthetic catalog.
- Validate create, update, no-change update, progress-next-stage only, win, lose and cancel behavior.
- Validate terminal Opportunities are read-only and repeat terminal actions are not presented in the frontend.
- Validate not-found, validation and safe generic error states.
- Validate duplicate submission protection before every mutation call.
- Validate frontend uses only `/api/crm/foundation/opportunities`.
- Validate productive `/api/crm/opportunities` remains unavailable.
- Validate no DELETE route or frontend delete behavior exists.

## Guardrails

- No productive Opportunity route or frontend reference.
- No DELETE.
- No Lead conversion, Account Management activation or assignment/owner runtime.
- No Portal Auth/token/header storage.
- No Common DB, EF, migrations, schema, SQL, secrets or real data.
- Do not touch `crm-prod-sim`.

## Deliverables

Focused domain/application/API/frontend tests or source verifiers, guardrail documentation, `tools/verify-crm-sprint-14-s14-05.ps1`, S14-06 prompt, `codex/TASKS.md` and `codex/next-task.md` handoff.
