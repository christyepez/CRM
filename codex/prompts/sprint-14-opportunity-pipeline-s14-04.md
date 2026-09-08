# CRM Sprint 14 S14-04 - Opportunity Pipeline Frontend Foundation Page

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 14 S14-03 merge commit required
Branch: crm-sprint-14-s14-04-opportunity-pipeline-frontend-foundation-page
Suggested commit: feat(crm): add opportunity pipeline frontend foundation page
PR title: CRM Sprint 14 S14-04 - Opportunity Pipeline Frontend Foundation Page

## Objective
Implement a usable Angular foundation-only Opportunity Pipeline workflow at `/foundation/opportunities` using only `/api/crm/foundation/opportunities`.

## Required workflow
- List and select Opportunities.
- Create and edit Open Opportunities.
- Display AccountName, ExpectedValue, Currency, Probability, Pipeline, Stage and Status.
- Use a deterministic synthetic pipeline stage catalog in the frontend that matches domain ordering rules.
- Progress only to the next stage.
- Mark Open Opportunity Won, Lost or Cancelled through explicit lifecycle endpoints.
- Terminal Opportunities are read-only; repeat terminal actions are not presented.
- Show loading, empty, validation, not-found and safe generic error states.
- Refresh after create/update/progress/win/lose/cancel and reflect normalized backend values.
- Prevent duplicate submission.
- Responsive and accessible basics; no raw JSON/developer console UI.

## API routes
GET/POST `/api/crm/foundation/opportunities`, GET/PUT `/api/crm/foundation/opportunities/{id}`, and POST actions `/progress`, `/win`, `/lose`, `/cancel`.

## Guardrails
- No `/api/crm/opportunities` productive route or frontend reference.
- No DELETE.
- No Lead conversion, Account Management activation or assignment/owner runtime.
- No Portal Auth/token/header storage.
- No Common DB, EF, migrations, schema, SQL, secrets or real data.
- Do not touch `crm-prod-sim`.

## Deliverables
Angular page/service/models/tests, foundation navigation entry, docs, `tools/verify-crm-sprint-14-s14-04.ps1`, S14-05 prompt, `codex/TASKS.md` and `codex/next-task.md` handoff.
