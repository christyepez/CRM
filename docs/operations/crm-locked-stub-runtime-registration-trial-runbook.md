# CRM Locked Stub Runtime Registration Trial Runbook

Runbook for Sprint 6 P5:

1. Confirm GitHub `main` contains Sprint 6 P4.
2. Run .NET tests.
3. Run frontend foundation verifier.
4. Run `tools/preflight-crm-local.ps1`.
5. Run `tools/check-crm-guardrails.ps1`.
6. Run `tools/verify-crm-foundation.ps1`.
7. Start Docker with `docker compose up -d --build`.
8. Validate `/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial`.
9. Validate `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` return 404.

Rollback:

- Remove the P5 endpoint and status service.
- Confirm no productive route registration exists.
- Re-run negative route checks.

No data cleanup is required because P5 must not use DB, stores, domain services, Auth, Portal or real data.
