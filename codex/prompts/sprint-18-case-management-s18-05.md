# CRM Sprint 18 S18-05 - Case Test and Guardrail Hardening

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 18 S18-04 merge commit required
Branch: crm-sprint-18-s18-05-case-test-guardrail-hardening
Suggested commit: test(crm): harden case management guardrails
PR title: CRM Sprint 18 S18-05 - Case Test and Guardrail Hardening

## Objective
Harden Case Management domain, application, API and frontend guardrails without expanding runtime scope.

## Required coverage
- Domain validation, normalization, lifecycle transitions and idempotency.
- Application invalid/not-found/no-change persistence suppression.
- API 400/404/409 mapping and explicit DTO boundaries.
- Frontend remains foundation-only and aligned to CustomerId, Title, Summary, Priority and status lifecycle.
- Explicit absence of productive Case routes and all Case DELETE routes.
- Cross-layer parity for limits, priority values and Open/InProgress/Resolved/Closed states.

## Guardrails
No customer mutation/lookup, assignment runtime, SLA runtime, notifications runtime, Portal runtime, Common DB/EF/SQL, real data, connectors, `crm-prod-sim`, port 8094 or real Production.

## Handoff
Prepare S18-06 Local Integration Validation and run complete regressions before commit/PR.
