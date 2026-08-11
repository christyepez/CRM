# CRM Controlled Runtime Pilot Scaffold Risk Register

| Risk | Status | Mitigation |
| --- | --- | --- |
| Accidental Portal runtime coupling | Controlled | Keep disabled client contract and guardrail checks. |
| Productive route exposure | Controlled | Productive Portal navigation and Gateway routes remain disabled. |
| Secret or token exposure | Controlled | No real provider, no credential values and no browser token storage. |
| Common DB boundary violation | Controlled | No DB runtime, no shared Portal tables and no cross-domain migrations. |
| Portal capability duplication | Controlled | CRM does not duplicate Auth, Menu, Permissions, Audit, Notification or Configuration. |

## Markers

- RealPortalPrivateUrlsPresent: false.
- PortalServicesInCrmCompose: false.
- RealCommonDbConnectionConfigured: false.
- SharedPortalTablesAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
- PortalDatabaseDirectAccessEnabled: false.
- SecretsPresent: false.
- EnvRealFileCommitted: false.
- PrivateUrlsPresent: false.
- RealDataPresent: false.
