# CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation

Repository: https://github.com/christyepez/CRM

Base:
S13-05 merge commit required.

Branch:
crm-sprint-13-s13-06-activity-local-integration-validation

Commit sugerido:
test(crm): validate activity follow-up local integration

PR title:
CRM Sprint 13 S13-06 - Activity / Follow-Up Local Integration Validation

## Objective

Validate Activity / Follow-Up locally across the existing foundation-only stack: Angular foundation route, CRM API foundation endpoints, application service, domain policy and in-memory foundation store.

## Scope

- Use synthetic data only.
- Exercise `/foundation/activities` against `/api/crm/foundation/activities`.
- Validate create, read, update, no-change update, complete, cancel, target-not-found and lifecycle conflict behavior.
- Validate Lead and Contact target seams without activating Account, Opportunity or assignment dependencies.
- Validate UTC date handling and derived overdue display behavior.
- Validate negative productive routes and DELETE remain unavailable.
- Validate logs do not disclose secrets, tokens, Authorization headers or PII beyond synthetic fixtures.
- Keep Portal Auth runtime disabled.
- Keep Common DB, EF, schema and migrations disabled.
- Keep simulated Production baseline untouched.

## Guardrails

- No productive Activity route activation.
- No DELETE.
- No Lead conversion.
- No Account Management activation.
- No Opportunity or assignment feature.
- No DB runtime, EF runtime, schema changes or migrations.
- No Portal Auth runtime, token reads, Authorization header reads or CRM-owned login.
- No secrets, `.env`, certificates, tokens, private URLs or real data.

## Validations

- `dotnet build CRM.sln`
- `dotnet test tests/CRM.UnitTests/CRM.UnitTests.csproj --no-build`
- `dotnet test tests/CRM.ArchitectureTests/CRM.ArchitectureTests.csproj --no-build`
- `dotnet test CRM.sln --no-build`
- `npm run build` from `frontend/crm-web`
- `npm run test` from `frontend/crm-web`
- `tools/check-crm-guardrails.ps1`
- `tools/verify-crm-foundation.ps1`
- `tools/verify-crm-sprint-13-s13-03.ps1`
- `tools/verify-crm-sprint-13-s13-04.ps1`
- `tools/verify-crm-sprint-13-s13-05.ps1`

## Expected Close

S1306Decision: Implemented
NextTaskPhase: CRM Sprint 13 S13-07 - Activity / Follow-Up Sprint Closure
