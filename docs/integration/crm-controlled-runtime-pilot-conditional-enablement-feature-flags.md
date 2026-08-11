# CRM Controlled Runtime Pilot Conditional Enablement Feature Flags

## Proposed flags

| Flag | Default | Future use |
| --- | --- | --- |
| CRM_CONTROLLED_RUNTIME_PILOT_ENABLED | false | Master switch for a future NonProduction pilot |
| CRM_PORTAL_RUNTIME_CLIENT_ENABLED | false | Allows a future Portal client adapter to run |
| CRM_PORTAL_GATEWAY_ROUTES_ENABLED | false | Allows future route registration after approval |
| CRM_PORTAL_NAVIGATION_ENABLED | false | Allows future Portal navigation exposure after approval |
| CRM_COMMON_DB_RUNTIME_ENABLED | false | Allows future CRM logical database connectivity checks only after approval |
| CRM_PILOT_SMOKE_ENABLED | false | Allows future non-destructive smoke execution |

## Markers

- ConditionalEnablementFeatureFlagsPrepared: true.
- ConditionalFutureGoDefined: true.
- ConditionalFutureGoExecuted: false.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- CommonDbRuntimeEnabled: false.
