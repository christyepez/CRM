# CRM Productive Route Dry Run Trial Runbook

Purpose: verify P5 without enabling productive CRM runtime.

Steps:
1. Confirm `main` contains the approved P5 base commit.
2. Build and test the solution.
3. Run guardrails and foundation verification.
4. Start Docker Compose.
5. Confirm health endpoints return 200.
6. Confirm `GET /api/crm/foundation/sprint-9/productive-route-dry-run-trial` returns metadata.
7. Confirm `POST /api/crm/foundation/sprint-9/productive-route-dry-run-trial/probe` returns 423 by default.
8. Confirm productive routes remain 404 by default.

Do not:
- enable production activation
- register productive CRUD routes by default
- execute DB writes
- enable DELETE
- enable Portal Auth enforcement
- add UI productiva

If any productive route returns a non-404 default response, stop the rollout and rollback.
