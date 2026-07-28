# CRM Secret Provider Real NonProduction Runtime Probe Runbook

1. Confirm Sprint 7 P1 is merged.
2. Confirm `.env` is absent.
3. Run preflight and guardrails.
4. Start CRM with Docker Compose.
5. Check `/health`, `/health/live`, `/health/ready`.
6. Check `/api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe`.
7. Confirm `probeSkippedBecauseApprovalNotGranted=true`.

Do not configure real secrets during P2.
