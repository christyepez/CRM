# CRM Common DB Real Connectivity NonProduction Probe Runbook

1. Confirm Sprint 7 P2 is merged.
2. Confirm `.env` is absent.
3. Run preflight and guardrails.
4. Start CRM with Docker Compose.
5. Check `/health`, `/health/live`, `/health/ready`.
6. Check `/api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe`.
7. Confirm `connectionProbeSkippedBecauseSecretProviderApprovalNotGranted=true`.
8. Confirm productive routes remain 404.

Do not configure real DB secrets during P3.
