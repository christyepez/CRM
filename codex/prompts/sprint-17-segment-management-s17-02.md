# CRM Sprint 17 S17-02 - Segment Application Service and Foundation Store

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 17 S17-01 merge commit required
Branch: crm-sprint-17-s17-02-segment-application-foundation-store
Suggested commit: feat(crm): add segment application service and foundation store
PR title: CRM Sprint 17 S17-02 - Segment Application Service and Foundation Store

## Objective
Add the typed Application orchestration and in-memory foundation persistence seam for Segment Management, delegating all business rules to SegmentManagementPolicy.

## Required implementation
- I/SegmentManagementService with list/detail/create/update/activate/deactivate.
- Application request/result contracts.
- I/SegmentFoundationStore and deterministic InMemorySegmentFoundationStore.
- Store normalized Name, CriteriaSummary and SegmentStatus only.
- Invalid/not-found/no-change operations must not write.
- Persistence classification remains FoundationOnly / NonProductionSeam.

## Tests
Cover deterministic seed, create normalization, read-after-write, update/no-change, lifecycle/idempotency, invalid/not-found and write suppression.

## Guardrails
No API/frontend yet; no Productive Segment route; no DELETE; no criteria execution, targeting, Account classification, Portal/Common DB/real data/connectors/Production.

## Handoff
Prepare S17-03 Segment Foundation API prompt, docs/verifier/TASKS/next-task and run full regressions before PR/merge.