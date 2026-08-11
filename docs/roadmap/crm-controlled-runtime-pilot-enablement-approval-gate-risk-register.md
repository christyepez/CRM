# CRM Controlled Runtime Pilot Enablement Approval Gate Risk Register

| Risk | P9 state | Mitigation |
| --- | --- | --- |
| Approval gate is mistaken for runtime approval | Controlled | P9 explicitly remains `ApprovalGateOnly` and `NoGo`. |
| Conditional future Go is executed early | Controlled | `ConditionalFutureGoExecuted` remains false. |
| Real Portal endpoint or secret appears | Controlled | Only logical placeholders are allowed. |
| Common DB runtime crosses Portal boundary | Controlled | Common DB runtime remains disabled. |
| Portal capabilities are duplicated in CRM | Controlled | Portal retains Auth, Menu, Permissions, Audit, Notification and Configuration ownership. |
| Production status changes accidentally | Controlled | ProductionActivationDecision remains NoGo and CRM production ready remains false. |

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
- ApprovalGateOnly: true.
