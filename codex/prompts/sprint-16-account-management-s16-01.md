# CRM Sprint 16 S16-01 - Account Contracts and Domain Rules

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 16 P1 merge commit required
Branch: crm-sprint-16-s16-01-account-contracts-domain-rules
Suggested commit: feat(crm): add account contracts and domain rules
PR title: CRM Sprint 16 S16-01 - Account Contracts and Domain Rules

## Objective
Create dedicated Account Management domain contracts and rules that replace free-form foundation semantics with deterministic business behavior, without changing productive runtime.

## Required domain behavior
- Name required, trimmed, max 160.
- TaxId optional, bounded and normalized.
- Industry optional, bounded.
- Segment optional, bounded.
- Create -> Draft.
- Activate Draft/Inactive -> Active.
- Deactivate Active -> Inactive.
- Repeated activation/deactivation to the same status is idempotent.
- Profile updates allowed in Draft/Active/Inactive with explicit no-change result.
- No DELETE.

## Explicitly deferred
- Contact linking/unlinking mutation.
- Lead conversion and automatic Account creation.
- Portal users/owners/assignment.
- Productive `/api/crm/accounts`.
- Common DB/EF/migrations/schema/SQL/real data.
- External connectors and Production.

## Validation
Add focused unit tests for all normalization, validation, lifecycle and idempotency rules plus architecture tests that keep Domain independent and Productive/DELETE absent.
Run full backend/frontend regressions, guardrails, Foundation and Sprint 16 P1/S16-01 verifiers.

## Handoff
Prepare S16-02 Account Application Service and Foundation Store Modernization prompt and update next-task after validation. Do not auto-merge.
