# CRM Controlled Runtime Pilot First Slice NonProduction Activation Scaffold Validation Risk Register

| Risk | Status | Mitigation |
| --- | --- | --- |
| Accidental runtime activation | Controlled | Feature flags remain false and verifier checks disabled service semantics. |
| Portal runtime coupling introduced early | Controlled | RuntimePortalCallsEnabled: false. RuntimePortalCouplingEnabled: false. |
| Productive navigation or Gateway route exposure | Controlled | ProductivePortalNavigationEnabled: false. ProductivePortalGatewayRoutesEnabled: false. |
| Common DB activation before approval | Controlled | CommonDbRuntimeEnabled: false. RealCommonDbConnectionConfigured: false. |
| Portal capability duplication | Controlled | Portal Auth, Menu, Permissions, Audit, Notification and Configuration stay not duplicated. |
| Secrets or private data committed | Controlled | SecretsPresent: false. PrivateUrlsPresent: false. RealDataPresent: false. |

Residual risk: the next gate must still review operational approval before any controlled NonProduction activation.
