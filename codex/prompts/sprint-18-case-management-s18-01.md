# CRM Sprint 18 S18-01 - Case Contracts and Domain Rules

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 18 P1 merge commit required
Branch: crm-sprint-18-s18-01-case-contracts-domain-rules
Suggested commit: feat(crm): add case contracts and domain rules
PR title: CRM Sprint 18 S18-01 - Case Contracts and Domain Rules

## Objective
Implement only the Case Management domain contracts and deterministic policy defined by Sprint 18 P1.

## Required domain model
- Add CasePriority if needed: Low, Medium, High, Critical.
- Add CaseStatus if needed: Open, InProgress, Resolved, Closed.
- Add CaseManagementOperation, CaseManagementCommand, CaseManagementSnapshot, CaseManagementErrorCode, CaseManagementRuleResult and CaseManagementPolicy.
- CustomerId: required structural reference only; no Customer creation or mutation.
- Title: required, trim, max 160.
- Summary: required, trim, max 1000.
- Priority: required and canonical.
- Create starts Open.
- Update allowed only in Open/InProgress with no-change detection.
- Start Open -> InProgress; repeated InProgress is idempotent.
- Resolve Open/InProgress -> Resolved; repeated Resolved is idempotent.
- Close Resolved -> Closed; repeated Closed is idempotent.

## Required tests
- Valid create and normalization.
- Empty/too-long CustomerId, Title and Summary.
- Invalid priority.
- Update changed and no-change.
- Start/resolve/close transitions and repeated idempotency.
- Reject update after Resolved or Closed.
- Invalid/missing snapshot and invalid id behavior.

## Guardrails
- Domain-only story: no Application service/store implementation, API or frontend.
- No `/api/crm/cases` and no foundation Case route yet.
- No DELETE.
- No automatic Customer conversion. No Customer creation or mutation.
- No Portal Auth/users/assignment runtime, Common DB/EF/migrations/schema/SQL/real data.
- No external connectors, `crm-prod-sim`, port 8094 or real Production changes.

## Validation and handoff
Add S18-01 docs/verifier and prepare S18-02 Application Service and Foundation Store prompt. Run full .NET and Angular regressions, Foundation and Sprint 18 verifiers before committing.
