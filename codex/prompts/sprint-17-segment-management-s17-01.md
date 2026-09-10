# CRM Sprint 17 S17-01 - Segment Contracts and Domain Rules

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 17 P1 merge commit required
Branch: crm-sprint-17-s17-01-segment-contracts-domain-rules
Suggested commit: feat(crm): add segment contracts and domain rules
PR title: CRM Sprint 17 S17-01 - Segment Contracts and Domain Rules

## Objective
Implement only the Segment domain contracts and deterministic policy defined by Sprint 17 P1.

## Required domain model
- Reuse existing conceptual Segment evidence safely; do not create conflicting parallel runtime models.
- Add SegmentStatus if needed: Draft, Active, Inactive.
- Add SegmentManagementOperation, SegmentManagementCommand, SegmentManagementSnapshot, SegmentManagementErrorCode, SegmentManagementRuleResult and SegmentManagementPolicy.
- Name: required, trim, max 160.
- CriteriaSummary: required, trim, max 1000, descriptive text only.
- Create starts Draft.
- Update allowed in Draft/Active/Inactive with no-change detection.
- Activate Draft/Inactive -> Active; repeated Active is idempotent.
- Deactivate Active -> Inactive; repeated Inactive is idempotent.

## Required tests
- Valid create and normalization.
- Empty/too-long Name.
- Empty/too-long CriteriaSummary.
- Update changed and no-change.
- Activate/deactivate transitions and repeated idempotency.
- Invalid/missing snapshot and invalid id behavior.

## Guardrails
- Domain-only story: no Application service/store implementation, API or frontend.
- No `/api/crm/segments` and no foundation Segment route yet.
- No DELETE.
- No Campaign targeting/linking, automatic audience assignment or Account auto-classification.
- No arbitrary criteria/query execution.
- No Portal Auth/users/assignment, Common DB/EF/migrations/schema/SQL/real data.
- No external connectors, `crm-prod-sim`, port 8094 or real Production changes.

## Validation and handoff
Add S17-01 docs/verifier and prepare S17-02 Application Service and Foundation Store prompt. Run full .NET and Angular regressions, Foundation and Sprint 17 verifiers before PR. Do not auto-merge.
