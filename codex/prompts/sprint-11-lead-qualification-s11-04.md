# CRM Sprint 11 S11-04 - Lead Intake Frontend Foundation Page

Repository: https://github.com/christyepez/CRM

Base: S11-03 merge commit required

Expected branch: crm-sprint-11-s11-04-lead-intake-frontend-foundation-page

Suggested commit: feat(crm): add lead qualification foundation frontend

PR title: CRM Sprint 11 S11-04 - Lead Intake Frontend Foundation Page

## Objective

Implement an Angular 18 development/foundation page for Lead Intake and Qualification using the S11-03 foundation API endpoint.

## Scope

- Foundation-only route/page.
- Lead selection/intake controls using synthetic foundation data.
- Qualification form with decision and reason controls.
- API client/service for `/api/crm/foundation/leads/{leadId}/qualification`.
- Loading, success and safe error states.
- Basic responsive and accessibility behavior.

## Guardrails

- Do not use productive `/api/crm/leads`.
- Do not activate Portal Auth runtime.
- Do not read or store tokens.
- Do not activate Common DB runtime.
- Do not add real data, secrets, `.env`, certificates or external dependencies.
- Do not touch SimulatedProduction.

## Validation

- `git diff --check`
- backend build/tests if backend contracts are touched
- frontend lint/build/test according to repository scripts
- `powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/check-crm-guardrails.ps1`
- `powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/verify-crm-foundation.ps1`

