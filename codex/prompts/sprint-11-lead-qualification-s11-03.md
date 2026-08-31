# CRM Sprint 11 S11-03 - Lead Qualification API Foundation Endpoints

Repository: https://github.com/christyepez/CRM

Base: S11-02 merge commit required

Expected branch: crm-sprint-11-s11-03-lead-qualification-api-foundation-endpoints

Suggested commit: feat(crm): add lead qualification foundation endpoints

PR title: CRM Sprint 11 S11-03 - Lead Qualification API Foundation Endpoints

## Objective

Expose foundation-only Lead Qualification API endpoints that invoke `LeadQualificationService` without unlocking productive routes.

## Scope

- Register development/foundation endpoint(s) only under `/api/crm/foundation/...`.
- Map HTTP request/response DTOs to S11-02 application contracts.
- Return safe deterministic status codes for success, validation failure, lead not found and invalid transitions.
- Add API/security tests.
- Preserve existing locked productive route behavior.

## Guardrails

- Do not register productive `/api/crm/leads`.
- Do not add DELETE.
- Do not activate Portal Auth runtime.
- Do not read Authorization headers or bearer tokens by default.
- Do not activate Common DB runtime, EF Core, migrations or schema.
- Do not create Docker services or touch SimulatedProduction.
- Do not add secrets, `.env`, tokens, certificates or real data.

## Validation

- `git diff --check`
- `dotnet build CRM.sln`
- `dotnet test CRM.sln --no-build`
- `powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/check-crm-guardrails.ps1`
- `powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/verify-crm-foundation.ps1`

