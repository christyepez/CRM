# CRM Locked Productive Route Runtime Registration Rollback

Rollback is configuration-only:

1. Disable `Crm:ProductiveRoutes:LockedRegistrationEnabled`.
2. Restart the CRM API if required by the hosting mode.
3. Verify productive routes return `404` again.
4. Keep foundation endpoint available for evidence.

No data rollback is required because P5 has no database, EF, migrations, stores, domain execution or side effects.
