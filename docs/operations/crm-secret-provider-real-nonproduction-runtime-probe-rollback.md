# CRM Secret Provider Real NonProduction Runtime Probe Rollback

P2 has no runtime secret reads and no external provider calls by default.

Rollback steps:
1. Disable the P2 endpoint by reverting the PR if needed.
2. Rebuild `crm-api`.
3. Re-run health and foundation checks.
4. Confirm no `.env`, no provider client and no secret value exposure.

No data rollback is required because P2 creates no database changes.
