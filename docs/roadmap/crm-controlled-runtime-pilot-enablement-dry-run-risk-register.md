# CRM Controlled Runtime Pilot Enablement Dry Run Risk Register

| Risk | Dry run result | Mitigation |
| --- | --- | --- |
| Dry run is mistaken for real runtime activation | Not detected | All evidence is marked `DryRunOnly`. |
| Real Portal endpoint is introduced | Not detected | Dry run uses logical placeholders only. |
| Feature flags are enabled | Not detected | All flags remain planned false. |
| Common DB runtime is activated | Not detected | Runtime remains disabled and no DB artifacts are created. |
| Portal capabilities are duplicated | Not detected | Portal retains Auth, Menu, Permissions, Audit, Notification and Configuration ownership. |
| Sensitive material enters repository | Not detected | Guardrails scan for secret, token, certificate and private endpoint markers. |

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
- DryRunOnly: true.
