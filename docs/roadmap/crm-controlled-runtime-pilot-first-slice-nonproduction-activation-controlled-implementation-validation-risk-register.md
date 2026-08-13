# CRM Controlled Implementation Validation Risk Register

| Risk | Status | Validation |
| --- | --- | --- |
| P24 scaffold executes activation | Controlled | Disabled service and dry-run report execution false. |
| Portal call introduced | Controlled | RuntimePortalCallsEnabled: false. |
| Portal coupling introduced | Controlled | RuntimePortalCouplingEnabled: false. |
| Feature flags enabled | Controlled | Feature flags remain false. |
| Compose adds Portal or SQL | Controlled | PortalServicesInCrmCompose: false. |
| Common DB runtime appears | Controlled | CommonDbRuntimeEnabled: false. |
| Secrets or private data appear | Controlled | SecretsPresent: false. PrivateUrlsPresent: false. RealDataPresent: false. |
