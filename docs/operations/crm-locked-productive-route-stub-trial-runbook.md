# CRM Locked Productive Route Stub Trial Runbook

## Validate default state

1. Run backend tests and architecture tests.
2. Run frontend foundation verification.
3. Run preflight and guardrails.
4. Start Docker and validate health endpoints.
5. Confirm `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` return 404.

## Rollback

No runtime rollback is required in P5 because no productive stubs are registered. If a future trial registers them, disable the flag and verify routes return 404 again.
