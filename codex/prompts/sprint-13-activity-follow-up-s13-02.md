# CRM Sprint 13 S13-02 - Activity Application Service and Foundation Store

Repository: https://github.com/christyepez/CRM

Task: Implement Activity application orchestration and foundation in-memory store.

Base Main Commit: S13-01 merge commit required

Branch: crm-sprint-13-s13-02-activity-application-service-foundation-store

Commit sugerido: feat(crm): add activity application service and foundation store

PR title: CRM Sprint 13 S13-02 - Activity Application Service and Foundation Store

## Objective

Use the S13-01 ActivityManagement domain policy to implement foundation-only application orchestration and an in-memory NonProduction seam for Activity / Follow-Up.

## Scope

- Add `IActivityManagementService`.
- Add `ActivityManagementService`.
- Add safe application request/response contracts.
- Add `IActivityFoundationStore`.
- Add `InMemoryActivityFoundationStore`.
- Implement create, update, complete and cancel orchestration if cancellation remains selected.
- Use `ActivityManagementPolicy`.
- Suppress writes when `Changed=false`.
- Return safe not-found and validation responses.
- Validate Lead/Contact existence strategy using foundation seams only if current stores make it safe; otherwise document as deferred structural validation.
- No API routes yet.

## Guardrails

- No productive `/api/crm/activities`.
- No Activity API route registration yet.
- No Angular Activity page yet.
- No DELETE.
- No Portal Auth runtime.
- No Authorization header/token reads.
- No Common DB, EF runtime, migrations, schema or SQL container.
- No CRM-owned Identity/login.
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

## Expected Closure

- Application service and in-memory foundation store implemented.
- Domain policy reused, not duplicated.
- Tests added.
- Next task points to S13-03 Activity Foundation API.
