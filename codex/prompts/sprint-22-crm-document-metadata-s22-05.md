# CRM Sprint 22 S22-05 - CRM Document Metadata Test and Guardrail Hardening

## Objective
Harden domain/application/API/frontend boundaries for document metadata/reference management.

## Required
- Boundary tests for field lengths and required references.
- No-change update persistence suppression.
- Archive idempotency and archived update conflict.
- 404 missing metadata.
- Productive route and DELETE unavailable.
- Frontend foundation-only API usage.
- Explicit no file picker/upload/download/binary storage guardrails.

## Guardrails
No Portal Content/File runtime, Common DB, EF/SQL, real data, external connectors, crm-prod-sim, port 8094 or Production.
