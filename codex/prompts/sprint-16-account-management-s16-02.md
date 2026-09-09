# CRM Sprint 16 S16-02 - Account Application Service and Foundation Store Modernization

Repository: https://github.com/christyepez/CRM
Base Main Commit: S16-01 merge commit required
Branch: crm-sprint-16-s16-02-account-application-store-modernization
Suggested commit: feat(crm): modernize account application foundation store
PR title: CRM Sprint 16 S16-02 - Account Application Service and Foundation Store Modernization

## Objective
Replace the generic free-form Account foundation service semantics with a dedicated Application service that uses `AccountManagementPolicy`, while preserving foundation-only synthetic persistence.

## Required behavior
- Add `IAccountManagementService` and explicit Application request/result contracts.
- Reuse `IAccountFoundationStore`; evolve its contract only as needed for typed Account snapshots.
- Modernize `InMemoryAccountFoundationStore` to store deterministic Account profile/status data.
- Operations: list, detail, create, update, activate, deactivate.
- Call the Domain policy before writes.
- Zero writes for invalid, not-found and no-change results.
- Preserve CancellationToken throughout.
- `PersistenceMode=NonProductionSeam`, durable/productive persistence false.

## Explicitly forbidden
- Do not change Account API registrations during S16-02.
- No productive `/api/crm/accounts`.
- No DELETE.
- No Lead conversion, automatic Account creation or Contact relationship mutations.
- No Portal Auth/users/owners/assignment.
- No Common DB, EF, migrations, schema, SQL, secrets, real data, external connectors or Production.

## Validation and handoff
Add focused Application/store tests and architecture tests. Run full backend/frontend regressions, guardrails, Foundation, P1, S16-01 and S16-02 verifiers. Prepare S16-03 Account Foundation API Modernization and update next-task. Do not auto-merge.
