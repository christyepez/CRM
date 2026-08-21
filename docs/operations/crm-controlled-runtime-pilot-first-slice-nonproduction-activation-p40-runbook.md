# P40 Controlled Execution Runbook

1. Synchronize `main` and validate base commit `5e873b82cad377736f5d2564e6b955642625b316`.
2. Create branch `crm-sprint-10-p40-controlled-runtime-pilot-first-slice-nonproduction-activation-controlled-execution`.
3. Revalidate P39A approval markers.
4. Validate Docker Compose configuration with `.env.example`.
5. Capture baseline with no CRM compose container running.
6. Build backend.
7. Run tests.
8. Start CRM compose service with `.env.example`.
9. Capture container status, health endpoints, logs and stats.
10. Validate Portal/Common DB remain disabled unless separately approved.
11. Validate productive routes remain locked or absent.
12. Record decision and P41 entry conditions.

Rollback: stop CRM compose service and restore configuration defaults if health, monitoring, route lock, environment or dependency checks fail.
