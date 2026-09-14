# Next CRM Task

Phase: CRM Sprint 22 S22-06 - CRM Document Metadata Local Integration Validation
Branch: crm-sprint-22-s22-06-crm-document-metadata-local-integration-validation
Prompt: codex/prompts/sprint-22-crm-document-metadata-s22-06.md
Suggested commit: test(crm): validate crm document metadata local integration

Rules:
- Foundation-only runtime.
- Metadata/reference only; no binary storage or file operations.
- Use isolated localhost ports; never 8094.
- Do not activate Portal Content/File runtime, Common DB, real data, crm-prod-sim or Production.
