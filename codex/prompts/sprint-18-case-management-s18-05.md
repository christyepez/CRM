# CRM Sprint 18 S18-05 - Case Test and Guardrail Hardening

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 18 S18-04 merge commit required
Branch: crm-sprint-18-s18-05-case-test-guardrail-hardening
Suggested commit: test(crm): harden case foundation guardrails
PR title: CRM Sprint 18 S18-05 - Case Test and Guardrail Hardening

## Objective
Harden Case Management across domain, application, API and Angular foundation layers without expanding runtime scope.

## Required work
- Add/strengthen regression tests for create/update/start/resolve/close and idempotency.
- Validate HTTP 400/404/409 mapping and explicit transport contracts.
- Validate frontend foundation route, lifecycle actions and safe error states.
- Add cross-layer guards against productive Case routes, DELETE, Portal token/runtime use, Common DB/EF/SQL, real data and external connectors.
- Preserve synthetic in-memory foundation storage only.

## Guardrails
No productive `/api/crm/cases`, DELETE, customer mutation, assignment/SLA/notification runtime, Portal Auth/users runtime, Common DB/EF/schema/SQL/real data, connectors, `crm-prod-sim`, port 8094 or Production.

## Validation and handoff
Run full .NET/Angular/foundation regression, document evidence, prepare S18-06 Local Integration Validation and update next-task.
