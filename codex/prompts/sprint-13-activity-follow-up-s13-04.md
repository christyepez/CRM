# CRM Sprint 13 S13-04 - Activity / Follow-Up Frontend Foundation Page

Repository: https://github.com/christyepez/CRM

Base Main Commit: S13-03 merge commit required

Branch: crm-sprint-13-s13-04-activity-follow-up-frontend-foundation-page

Suggested commit: feat(crm): add activity follow-up frontend foundation page

PR title: CRM Sprint 13 S13-04 - Activity / Follow-Up Frontend Foundation Page

## Objective

Add a foundation-only Angular Activity / Follow-Up page that consumes the S13-03 foundation API surface without enabling productive routes.

## Guardrails

- Do not add productive `/api/crm/activities` routes.
- Do not add DELETE behavior.
- Do not activate Portal Auth runtime.
- Do not activate Common DB runtime, EF runtime, migrations or schema changes.
- Do not add secrets, `.env`, tokens, certificates or real data.
- Keep simulated Production baseline untouched.
- Keep Activity UI clearly marked as foundation/non-production.

## Expected scope

- Add a frontend foundation page for Activity / Follow-Up list, create/update and lifecycle actions if current Angular conventions support it.
- Reuse existing frontend API client style and safe error rendering.
- Preserve foundation-only messaging and route labels.
- Add tests/guardrails following repository convention.

## Acceptance criteria

- Activity frontend page exists and is foundation-only.
- The page calls only `/api/crm/foundation/activities` routes.
- Productive Activity routes remain unavailable.
- DELETE remains unavailable.
- Portal/Common DB remain disabled.
