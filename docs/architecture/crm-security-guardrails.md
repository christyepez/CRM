# CRM Security Guardrails

## Required guardrails

- Do not store secrets, tokens, certificates or real personal data.
- Do not commit `.env`.
- Do not use `localStorage` or `sessionStorage` for auth.
- Do not create login, Identity, roles or permissions propios.
- Do not hardcode production URLs.
- Do not duplicate Portal Auth/Menu/permissions.
- Do not expose CRM production features in P1.

## P1 validation

`tools/verify-crm-foundation.ps1` checks required files, no SQL Server compose, no `.env`, expected routes and no premature mutating CRM endpoints.

Frontend `frontend/crm-web/tools/verify-crm-foundation.mjs` checks the Angular foundation files and rejects local token storage markers.
