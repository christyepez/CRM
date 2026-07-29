# CRM Locked Route Authorization Policy Rollback

Rollback is configuration-only for runtime behavior:

- Set `Crm:ProductiveRoutes:LockedRegistrationEnabled=false`.
- Set `Crm:ProductiveRoutes:LockedAuthorizationPolicyEnabled=false`.
- Restart CRM API if configuration was materialized at startup.

Expected rollback result:

- Productive routes return 404.
- No locked route handlers are registered.
- No policy metadata is returned by productive routes.
- Foundation endpoint remains available for audit/status.

No database rollback is required because P5 creates no migrations, tables, or persisted data.
