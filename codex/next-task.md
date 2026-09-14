# Next CRM Task

Phase: CRM Sprint 22 S22-05 - CRM Document Metadata Test and Guardrail Hardening
Branch: crm-sprint-22-s22-05-crm-document-metadata-test-guardrail-hardening
Prompt: codex/prompts/sprint-22-crm-document-metadata-s22-05.md
Suggested commit: test(crm): harden crm document metadata guardrails

Rules:
- Foundation-only runtime.
- Metadata/reference only; no file picker, upload/download or binary storage.
- No productive document route or DELETE.
- Do not activate Portal Content/File runtime, Common DB, real data, crm-prod-sim, port 8094 or Production.
