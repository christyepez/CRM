# CRM Sprint 13 S13-05 - Activity / Follow-Up Test and Guardrail Hardening

Repository: https://github.com/christyepez/CRM

Base Main Commit: S13-04 merge commit required

Branch: crm-sprint-13-s13-05-activity-test-guardrail-hardening

Suggested commit: test(crm): harden activity follow-up guardrails

PR title: CRM Sprint 13 S13-05 - Activity / Follow-Up Test and Guardrail Hardening

## Objective

Harden cross-layer tests and guardrails for the Activity / Follow-Up foundation workflow delivered in S13-01 through S13-04.

## Required coverage

- ActivityType parity across Domain, API and frontend.
- ActivityStatus parity across Domain, API and frontend.
- Foundation-only API URLs in frontend.
- Productive `/api/crm/activities` routes absent.
- DELETE absent in API and frontend.
- Exactly-one target rules for Lead/Contact.
- Scheduled/Completed/Cancelled lifecycle behavior.
- Completed/Cancelled read-only frontend behavior.
- Safe 400/404/409/generic error rendering.
- XSS safety for Activity subject.
- No auth/token/browser storage usage.
- No Common DB, EF, migrations or schema changes.

## Guardrails

- Do not add productive routes.
- Do not add DELETE.
- Do not activate Portal Auth runtime.
- Do not activate Common DB runtime.
- Do not touch simulated Production.
- Do not add secrets, `.env`, tokens, certificates or real data.

## Acceptance criteria

- Backend unit, architecture and full solution tests pass.
- Frontend build/test pass.
- Existing guardrails pass.
- New S13-05 verifier proves Activity frontend/API contract safety.
