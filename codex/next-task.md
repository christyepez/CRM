# Next Task

Phase: CRM Sprint 22 S22-03 - CRM Document Metadata Foundation API
Prompt: codex/prompts/sprint-22-crm-document-metadata-s22-03.md

Expose only foundation CRM document metadata/reference endpoints with explicit DTOs and safe 400/404/409 mappings.

Guardrails:
- metadata/reference only; no binary content
- no Portal Content/File runtime activation
- no upload/download, filesystem or blob storage
- no DELETE or productive CRM document route
- no Common DB/EF/SQL, real data, external connectors, crm-prod-sim, port 8094 or Production
