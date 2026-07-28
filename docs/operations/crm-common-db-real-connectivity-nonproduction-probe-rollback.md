# CRM Common DB Real Connectivity NonProduction Probe Rollback

P3 has no real connection attempts and no schema changes by default.

Rollback steps:
1. Revert the P3 PR if needed.
2. Rebuild `crm-api`.
3. Re-run health and foundation checks.
4. Confirm no `.env`, no connection string value, no DB runtime and no productive route activation.

No database rollback is required.
