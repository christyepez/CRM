# CRM Sprint 10 P1 - Productization Readiness Runbook

## Purpose

Validate that CRM has recorded a preparation-only productization decision without enabling runtime behavior.

## Checks

1. Build and test the solution.
2. Run guardrail, preflight and foundation verification scripts.
3. Start Docker Compose.
4. Validate:
   - `/health`
   - `/health/live`
   - `/health/ready`
   - `/api/crm/readiness`
   - `/api/crm/foundation/sprint-9/gate-decision`
   - `/api/crm/foundation/sprint-10/productization-readiness-decision`
5. Confirm Sprint 9 P2/P3/P4/P5 probes return 423 by default.
6. Confirm productive CRM routes return 404 by default.

## Rollback

Revert the Sprint 10 P1 branch or PR. No data, schema, secrets, runtime flags or external services are changed by this package.

## Manual fallback

If automation fails, inspect the API response and confirm it still reports `PreparationOnly`, `NoGo` for production and `NoGoForProduction` for productive runtime activation.
