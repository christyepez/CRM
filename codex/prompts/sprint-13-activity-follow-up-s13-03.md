# CRM Sprint 13 S13-03 - Activity Foundation API

Repository: https://github.com/christyepez/CRM

Task: Implement Activity / Follow-Up foundation API endpoints.

Base Main Commit: S13-02 merge commit required

Branch: crm-sprint-13-s13-03-activity-foundation-api

Commit sugerido: feat(crm): add activity foundation api

PR title: CRM Sprint 13 S13-03 - Activity Foundation API

## Objective

Expose Activity / Follow-Up foundation endpoints over `IActivityManagementService` without activating productive Activity routes.

## Scope

- Register `IActivityManagementService` and `IActivityFoundationStore` in API composition if needed.
- Add explicit API DTOs for foundation Activity create/update/result/status mapping.
- Add foundation routes only:
  - `GET /api/crm/foundation/activities`
  - `GET /api/crm/foundation/activities/{id}`
  - `POST /api/crm/foundation/activities`
  - `PUT /api/crm/foundation/activities/{id}`
  - `POST /api/crm/foundation/activities/{id}/complete`
  - `POST /api/crm/foundation/activities/{id}/cancel`
- Map validation/not-found/lifecycle outcomes to safe HTTP responses.
- Add API tests.
- Add productive route negative tests.

## Guardrails

- No productive `/api/crm/activities`.
- No DELETE.
- No Angular Activity page yet.
- No Portal Auth runtime, `[Authorize]`, Authorization header reads or token parsing.
- No Common DB, EF runtime, migrations, schema or SQL container.
- No Account Management or Opportunity runtime dependency.
- No simulated Production or real Production changes.
- No secrets, `.env`, tokens, certificates or real data.

## Required Validation

- `git diff --check`
- `dotnet build CRM.sln`
- `dotnet test tests/CRM.UnitTests/CRM.UnitTests.csproj --no-build`
- `dotnet test tests/CRM.ArchitectureTests/CRM.ArchitectureTests.csproj --no-build`
- `dotnet test CRM.sln --no-build`
- `npm run build`
- `npm run test`
- `tools/check-crm-guardrails.ps1`
- `tools/verify-crm-foundation.ps1`
- `tools/verify-crm-sprint-13-p1.ps1`
- `tools/verify-crm-sprint-13-s13-01.ps1`
- `tools/verify-crm-sprint-13-s13-02.ps1`

## Expected Closure

- Foundation Activity API implemented and tested.
- Productive Activity route remains absent.
- Next task points to S13-04 Activity / Follow-Up Frontend Foundation Page.
