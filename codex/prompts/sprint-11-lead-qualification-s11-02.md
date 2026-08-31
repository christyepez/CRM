# CRM Sprint 11 S11-02 - Lead Qualification Application Service

Repository: https://github.com/christyepez/CRM

Base: S11-01 merge commit required

Expected branch: crm-sprint-11-s11-02-lead-qualification-application-service

Suggested commit: feat(crm): add lead qualification foundation service

PR title: CRM Sprint 11 S11-02 - Lead Qualification Application Service

## Objective

Implement the application service that orchestrates Lead Qualification using the S11-01 contracts and domain policy.

## Scope

- Use the existing foundation/nonproduction seam only.
- Invoke `LeadQualificationPolicy`.
- Return `LeadQualificationResult` with foundation guardrail metadata.
- Keep persistence as `NonProductionSeam`.
- Add focused unit tests.

## Guardrails

- Do not unlock productive `/api/crm/leads`.
- Do not add API routes yet unless a later story explicitly asks.
- Do not activate Portal Auth runtime.
- Do not read Authorization headers or tokens by default.
- Do not activate Common DB runtime, EF Core, migrations or schema.
- Do not create SQL Server or Docker services.
- Do not touch SimulatedProduction.
- Do not add secrets, `.env`, tokens, certificates or real data.

## Validation

- `git diff --check`
- `dotnet build CRM.sln`
- `dotnet test CRM.sln --no-build`
- `powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/check-crm-guardrails.ps1`
- `powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/verify-crm-foundation.ps1`

