# CRM Sprint 13 S13-01 - Activity Contracts and Domain Rules

Repository: https://github.com/christyepez/CRM

Task: Implement Activity / Follow-Up contracts and deterministic domain rules.

Base Main Commit: Sprint 13 P1 merge commit required

Branch: crm-sprint-13-s13-01-activity-contracts-domain-rules

Commit sugerido: feat(crm): add activity contracts and domain rules

PR title: CRM Sprint 13 S13-01 - Activity Contracts and Domain Rules

## Objective

Create the first functional Activity / Follow-Up foundation increment by defining domain contracts and rules for scheduling and completing activities linked to a Lead or Contact.

## Scope

- Add ActivityManagement command/result/error contracts in `CRM.Domain`.
- Add deterministic ActivityManagement policy.
- Reuse canonical terminology: Activity and Follow-Up.
- Reuse existing `ActivityType`: Call, Email, Meeting, Task.
- Reuse existing `ActivityStatus`: Scheduled, Completed, Cancelled.
- Model Follow-Up as an Activity with scheduled/due date and Lead or Contact target.
- Require exactly one target for foundation: LeadId or ContactId.
- Validate bounded subject/details/outcome if introduced.
- Define completion behavior: only Scheduled activities can be completed.
- Add unit tests for domain rules.
- Update documentation and guardrails if needed.

## Guardrails

- Do not add Activity application service yet.
- Do not add API routes yet.
- Do not add Angular UI yet.
- Do not add productive `/api/crm/activities`.
- Do not add DELETE.
- Do not activate Portal Auth runtime.
- Do not read Authorization headers or tokens.
- Do not activate Common DB, EF runtime, migrations or schema changes.
- Do not add CRM-owned Identity/login.
- Do not touch simulated Production or real Production.
- Do not implement Opportunity Pipeline or Account Management as hidden dependencies.
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

## Expected Closure

- Activity domain contracts and rules implemented.
- Tests added.
- Productive routes still unavailable.
- Portal/Common DB still disabled.
- Next Sprint 13 story prepared.
