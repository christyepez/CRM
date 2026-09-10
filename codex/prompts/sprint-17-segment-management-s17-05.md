# CRM Sprint 17 S17-05 - Segment Test and Guardrail Hardening

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 17 S17-04 merge commit required
Branch: crm-sprint-17-s17-05-segment-test-guardrail-hardening
Suggested commit: test(crm): harden segment management guardrails
PR title: CRM Sprint 17 S17-05 - Segment Test and Guardrail Hardening

## Objective
Harden Segment lifecycle, API and cross-layer guardrails without expanding runtime scope.

## Required coverage
- Domain normalization, limits, invalid ids/snapshots and lifecycle idempotency.
- Application invalid/not-found/no-change persistence suppression.
- API 400/404/409 mapping and repeated lifecycle success.
- Frontend route/service remains Foundation-only.
- Cross-layer parity for Name and CriteriaSummary.
- Explicit absence of Productive Segment routes and all Segment DELETE routes.

## Guardrails
No criteria execution, targeting, auto-classification, Portal runtime, Common DB, real data, connectors, `crm-prod-sim` or real Production.

## Handoff
Prepare S17-06 Local Integration Validation and run complete regressions before PR/merge.
