# CRM Sprint 17 S17-07 - Segment Management Sprint Closure

Repository: https://github.com/christyepez/CRM
Base Main Commit: S17-06 merge commit required
Branch: crm-sprint-17-s17-07-segment-management-sprint-closure
Suggested commit: docs(crm): close segment management sprint
PR title: CRM Sprint 17 S17-07 - Segment Management Sprint Closure

## Objective
Close Sprint 17 Segment Management only after confirming the integrated Domain/Application/API/frontend foundation slice and S17-06 local HTTP evidence remain green.

## Closure requirements
- Re-run full .NET build/tests and Angular build/test.
- Re-run CRM guardrails, Foundation and Sprint 17 P1/S17-01..S17-07 verifiers.
- Capture integrated test counts and S17-06 runtime evidence.
- Confirm Name/CriteriaSummary and Draft/Active/Inactive contracts remain aligned.
- Confirm create/update/activate/deactivate lifecycle and idempotency remain intact.
- Confirm Productive `/api/crm/segments` and DELETE remain unavailable.
- Confirm criteria execution, Campaign targeting and Account auto-classification remain deferred.
- Confirm Portal runtime, Common DB, real data, connectors and real Production remain disabled.

## Closure artifacts
Create the Sprint 17 closure document, integrated evidence summary, S17-07 verifier, next-capability recommendation, and next sprint P1 handoff prompt. Preserve all prior evidence immutably.

## Guardrails
No Productive Segment route, DELETE, criteria execution, targeting, Account auto-classification, Portal Auth/users/assignment, Common DB/EF/migrations/schema/SQL/real data, external connectors, `crm-prod-sim`, port 8094 or real Production changes.

## Decision
If all checks pass, mark Sprint 17 `ClosedSuccessfully`, select the next bounded business capability from existing repository evidence, update `codex/next-task.md`, and prepare its P1 baseline prompt. Do not activate deferred capabilities.
