# CRM Sprint 12 S12-06 - Contact Management Local Integration Validation

Repository:
https://github.com/christyepez/CRM

Task:
Validate the Contact Management foundation workflow locally end-to-end after S12-05 is merged.

Base:
S12-05 merge commit required.

Expected branch:
crm-sprint-12-s12-06-contact-management-local-integration-validation

Suggested commit:
test(crm): validate contact management local integration

PR title:
CRM Sprint 12 S12-06 - Contact Management Local Integration Validation

## Objective

Validate the real local Angular to API foundation workflow for Contact Management without enabling productive CRM routes, Portal runtime, Common DB runtime, simulated production, DELETE, Lead conversion or real data.

## Scope

- Run CRM API locally.
- Run Angular CRM frontend locally.
- Validate Angular to API interaction through `/api/crm/foundation/contacts`.
- Validate Contact list.
- Validate create Contact.
- Validate edit Contact.
- Validate no-change update.
- Validate invalid create.
- Validate not-found update/detail handling.
- Validate read-after-write.
- Validate proxy/CORS behavior for the local foundation route.
- Validate local logs do not contain PII payloads, tokens, secrets or connection strings.
- Use synthetic data only.

## Guardrails

- Do not use `/api/crm/contacts`.
- Do not add or call DELETE.
- Do not implement Lead conversion.
- Do not activate Portal runtime.
- Do not activate Common DB runtime.
- Do not touch `crm-prod-sim`.
- Do not add production URLs, secrets, tokens or real connection strings.
- Do not execute migrations or create schema.
- Do not use real customer/contact data.

## Required validations

- `git diff --check`
- `dotnet build CRM.sln`
- `dotnet test tests/CRM.UnitTests/CRM.UnitTests.csproj --no-build`
- `dotnet test tests/CRM.ArchitectureTests/CRM.ArchitectureTests.csproj --no-build`
- `dotnet test CRM.sln --no-build`
- `npm run build`
- `npm run test`
- `tools/check-crm-guardrails.ps1`
- `tools/verify-crm-foundation.ps1`
- `tools/verify-crm-sprint-12-s12-05.ps1`

## Expected closure

Document local integration evidence, HTTP statuses, frontend workflow observations, proxy/CORS result, read-after-write result, negative productive route result, risks and S12-07 recommendation.
