# CRM Controlled Runtime Pilot Enablement Risk Register

| Risk | Status | Planned mitigation |
| --- | --- | --- |
| Planning accidentally becomes runtime enablement | Controlled | P7 guardrails allow docs, tools and codex only. |
| Real Portal endpoint appears in configuration | Controlled | Safe configuration uses logical placeholders only. |
| Feature flags are interpreted as enabled | Controlled | All planned flags remain false until a future approved dry run. |
| Common DB runtime crosses Portal boundary | Controlled | P7 keeps runtime disabled and forbids shared tables. |
| Portal capabilities are duplicated in CRM | Controlled | Portal remains owner of Auth, Menu, Permissions, Audit, Notification and Configuration. |
| Sensitive material enters repository | Controlled | Secret, token, certificate and private endpoint scans remain required. |

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
