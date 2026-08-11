# CRM Controlled Runtime Pilot Validation Risk Register

| Risk | Validation result | Mitigation |
| --- | --- | --- |
| Scaffold accidentally becomes runtime coupling | Not detected | Guardrails require disabled Portal calls and no runtime coupling markers. |
| Productive Portal routes or navigation become enabled | Not detected | GO/NO-GO keeps both disabled. |
| CRM compose starts Portal services | Not detected | Compose scan remains part of P6 guardrails. |
| Common DB runtime crosses Portal boundary | Not detected | P2/P6 markers keep runtime disabled and prevent shared tables. |
| Portal capability duplication appears in CRM | Not detected | Auth, Menu, Permissions, Audit, Notification and Configuration remain Portal-owned. |
| Sensitive material enters repository | Not detected | Secret, token, certificate and private URL scans remain mandatory. |

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
