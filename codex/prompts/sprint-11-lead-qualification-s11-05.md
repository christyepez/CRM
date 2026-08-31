# CRM Sprint 11 S11-05 - Lead Qualification Test and Guardrail Hardening

Repository: https://github.com/christyepez/CRM

Base: S11-04 merge commit required

Expected branch: crm-sprint-11-s11-05-lead-qualification-test-guardrail-hardening

Suggested commit: test(crm): harden lead qualification foundation guardrails

PR title: CRM Sprint 11 S11-05 - Lead Qualification Test and Guardrail Hardening

## Objective

Harden cross-layer tests and guardrails for the Lead Qualification foundation slice delivered in S11-01 through S11-04.

## Scope

- Backend integration/contract tests.
- Frontend workflow tests or repository-equivalent verification.
- Productive route negative tests.
- Security tests for no tokens, no Portal Auth runtime, no Common DB runtime and no secret exposure.
- Contract consistency between Angular models and API DTOs.
- Accessibility/source checks for the foundation page.
- Guardrail verifier improvements.

## Guardrails

- Do not unlock productive `/api/crm/leads`.
- Do not add DELETE.
- Do not activate Portal Auth runtime.
- Do not read/store tokens.
- Do not activate Common DB runtime, EF Core, migrations or schema.
- Do not add secrets, `.env`, certificates, external production URLs or real data.
- Do not touch SimulatedProduction.

## Validation

- `git diff --check`
- frontend build/test
- `dotnet build CRM.sln`
- `dotnet test CRM.sln --no-build`
- `powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/check-crm-guardrails.ps1`
- `powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/verify-crm-foundation.ps1`

