# CRM Portal Auth Real Runtime Probe Rollback

Rollback is simple because P4 does not enable runtime side effects.

If needed:

1. Revert the P4 commit.
2. Redeploy CRM API.
3. Verify health endpoints.
4. Confirm no Portal HTTP, token/header reads, Auth middleware or productive routes exist.

No database rollback is required because P4 creates no schema, migrations or data.
