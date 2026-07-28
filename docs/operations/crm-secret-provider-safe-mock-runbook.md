# CRM Secret Provider Safe Mock Runbook

Use this runbook to validate Sprint 6 P2 locally.

1. Confirm GitHub `main` contains Sprint 6 P1.
2. Run build and tests.
3. Run `tools/preflight-crm-local.ps1`.
4. Run `tools/check-crm-guardrails.ps1`.
5. Start Docker with `docker compose up -d --build`.
6. Check `GET /api/crm/foundation/sprint-6/secret-provider-safe-mock-activation`.
7. Confirm negative productive routes still return 404.

Rollback:

- Revert the P2 branch or disable the endpoint by reverting its commit.
- No secret, DB, Portal or runtime state needs cleanup because P2 does not create external state.

Do not create `.env`, configure real secrets, connect DB, call Portal Auth, register productive routes or add DELETE.
