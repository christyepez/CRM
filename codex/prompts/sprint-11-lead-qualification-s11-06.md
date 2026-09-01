# CRM Sprint 11 S11-06 - Lead Qualification Local Integration Validation

Repository:
https://github.com/christyepez/CRM

Objective:
Validate the complete Lead Qualification foundation workflow locally end-to-end, using the Angular foundation page and CRM foundation API together.

Base:
S11-05 merge commit required.

Expected branch:
crm-sprint-11-s11-06-lead-qualification-local-integration-validation

Suggested commit:
test(crm): validate lead qualification local integration workflow

PR title:
CRM Sprint 11 S11-06 - Lead Qualification Local Integration Validation

Scope:

- Run backend and frontend locally.
- Validate browser/client calls to `POST /api/crm/foundation/leads/{leadId}/qualification`.
- Validate GET foundation leads supports the page lead selector.
- Validate synthetic qualify, disqualify, idempotent, 400, 404 and 409 flows.
- Validate CORS/proxy behavior if the frontend and API run on different local ports.
- Capture local integration evidence.

Guardrails:

- Do not touch SimulatedProduction.
- Do not rebuild, restart or mutate `crm-prod-sim`.
- Do not activate productive CRM APIs.
- Do not add Portal Auth runtime.
- Do not read Authorization headers or tokens.
- Do not use localStorage/sessionStorage for tokens.
- Do not activate Common DB runtime.
- Do not create SQL Server, schema, migrations or data writes outside the foundation seam.
- Do not add real customer data.
- Do not add secrets or `.env`.

Validations:

- `npm run build`
- `npm run test`
- `dotnet build CRM.sln`
- `dotnet test CRM.sln --no-build`
- `tools/check-crm-guardrails.ps1`
- `tools/verify-crm-foundation.ps1`
- `tools/verify-crm-sprint-11-s11-05.ps1`
- local integration smoke for `/foundation/leads/qualification`

Expected close:

- Local Angular-to-API Lead Qualification workflow verified.
- Productive route remains unavailable.
- Foundation-only runtime evidence documented.
- S11-07 next task prepared if needed.
