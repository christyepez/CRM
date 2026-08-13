# CRM NonProduction Activation Final Approval Gate Risk Register

| Risk | Status | Required control |
| --- | --- | --- |
| Activation without approval | Blocked | P23 is gate-only and keeps activation unexecuted. |
| Portal coupling before P24 | Blocked | RuntimePortalCouplingEnabled: false. RuntimePortalCallsEnabled: false. |
| Feature flags changed prematurely | Blocked | ConditionalGoFutureExecuted: false. |
| Productive route exposure | Blocked | ProductivePortalGatewayRoutesEnabled: false. ProductivePortalNavigationEnabled: false. |
| Common DB or direct Portal DB access | Blocked | CommonDbRuntimeEnabled: false. PortalDatabaseDirectAccessEnabled: false. |
| Capability duplication | Blocked | Portal Auth/Menu/Permissions/Audit/Notification/Configuration not duplicated. |
| Secrets or private data leakage | Blocked | SecretsPresent: false. PrivateUrlsPresent: false. RealDataPresent: false. |

Residual risk: P24 must still be reviewed as an implementation PR, not an automatic activation.
