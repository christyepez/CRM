# CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline and Backlog

Repository: https://github.com/christyepez/CRM

Task: Define the Activity / Follow-Up foundation slice after Sprint 12 Contact Management closure.

Base Main Commit: S12-07 merge commit required

Branch: crm-sprint-13-p1-activity-follow-up-functional-baseline

Commit sugerido: docs(crm): add activity follow-up functional baseline

PR title: CRM Sprint 13 P1 - Activity / Follow-Up Functional Baseline and Backlog

## Objective

Review existing CRM evidence and define the next foundation-only Activity / Follow-Up slice without implementing runtime business functionality yet.

## Scope

- Validate existing Activity, Follow-Up, Lead and Contact relationships.
- Define minimum Activity / Follow-Up contracts, domain rules, application seams, API foundation routes, frontend foundation needs and guardrails.
- Keep productive routes locked.
- Keep Portal Auth runtime disabled.
- Keep Common DB runtime disabled.
- Keep Simulated Production and real Production untouched.

## Guardrails

- No productive Activity API activation.
- No DELETE.
- No real DB runtime, EF runtime, migrations or schema changes.
- No Portal Auth runtime activation.
- No Authorization header or token reads by default.
- No CRM-owned Identity/login.
- No secrets, `.env`, tokens, certificates or real data.
- No Lead conversion.
- No Account Management activation.
- No simulated Production or real Production changes.

## Validations

- `git diff --check`
- `dotnet build CRM.sln`
- `dotnet test CRM.sln --no-build`
- `npm run build`
- `npm run test`
- Existing CRM guardrails.

## Expected Closure

- Activity / Follow-Up baseline documented.
- Sprint 13 implementation packages defined.
- Next Sprint 13 implementation task prepared in `codex/next-task.md`.
- No runtime activation.
