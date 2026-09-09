# CRM Sprint 16 S16-03 - Account Foundation API Modernization

Repository: https://github.com/christyepez/CRM
Base Main Commit: S16-02 merge commit required
Branch: crm-sprint-16-s16-03-account-foundation-api-modernization
Suggested commit: feat(crm): modernize account foundation api
PR title: CRM Sprint 16 S16-03 - Account Foundation API Modernization

## Objective
Replace the generic Account foundation CRUD endpoint behavior with explicit Account Management API contracts backed by `IAccountManagementService`, while preserving the existing `/api/crm/foundation/accounts` route family.

## Required behavior
- Keep GET list/detail, POST create and PUT update on `/api/crm/foundation/accounts`.
- Add POST `{id}/activate` and POST `{id}/deactivate`.
- Use explicit API request/response DTOs and mapping to Application contracts.
- Map AccountNotFound to 404, validation/lifecycle errors to 400/409 as appropriate.
- Preserve FoundationOnly/NonProductionSeam flags.
- No DELETE.

## Guardrails
- Productive `/api/crm/accounts` remains unavailable.
- No Lead conversion or automatic Account creation.
- No Contact relationship mutations.
- No Portal Auth/users/assignment.
- No Common DB, EF, migrations, schema, SQL, real data, external connectors or Production.

## Validation and handoff
Add endpoint and architecture tests. Run full backend/frontend regressions, guardrails, Foundation, P1, S16-01..S16-03 verifiers. Prepare S16-04 Account Management Frontend Foundation Page and update next-task. Do not auto-merge.
