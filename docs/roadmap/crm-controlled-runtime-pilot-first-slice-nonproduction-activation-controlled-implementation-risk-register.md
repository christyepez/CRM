# CRM Controlled NonProduction Activation Implementation Risk Register

| Risk | Status | Control |
| --- | --- | --- |
| Scaffold mistaken for activation | Controlled | Runtime remains disabled-by-default and fail-closed. |
| Portal call introduced early | Controlled | RuntimePortalCallsEnabled: false. |
| Coupling introduced before validation | Controlled | RuntimePortalCouplingEnabled: false. |
| Feature flags enabled | Controlled | ConditionalGoFutureExecuted: false. |
| Common DB runtime introduced | Controlled | CommonDbRuntimeEnabled: false. |
| Portal capability duplicated | Controlled | Portal Auth/Menu/Permissions/Audit/Notification/Configuration remain not duplicated. |
| Secret or private data leakage | Controlled | SecretsPresent: false. PrivateUrlsPresent: false. RealDataPresent: false. |
