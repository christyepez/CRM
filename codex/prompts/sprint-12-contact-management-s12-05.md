# CRM Sprint 12 S12-05 - Contact Management Test and Guardrail Hardening

Repository:
https://github.com/christyepez/CRM

Objective:
Harden cross-layer tests and guardrails for Contact Management after S12-01 through S12-04.

Scope:

- Verify Domain/Application/API/Frontend DTO consistency.
- Verify productive Contact routes remain unavailable.
- Verify foundation API is the only frontend Contact API namespace.
- Verify no DELETE behavior.
- Verify no Lead conversion.
- Verify no Portal runtime/token storage.
- Verify no Common DB runtime/schema changes.
- Verify PII/security and XSS guardrails.
- Expand accessibility and responsive source checks where practical.

Guardrails:

- Do not unlock productive `/api/crm/contacts`.
- Do not add DELETE.
- Do not implement Lead conversion.
- Do not activate Portal Auth runtime.
- Do not activate Common DB runtime.
- Do not add schema, migrations, EF runtime or SQL Server.
- Do not touch simulated Production.
- Do not add secrets, tokens, real connection strings or real data.

Acceptance:

- Backend tests pass.
- Frontend build/test pass.
- Guardrails pass.
- Contact-specific verifier passes.
- Next task prepared for S12-06 local integration validation.
