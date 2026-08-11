# CRM Controlled Runtime Pilot Enablement Smoke Plan

## Future smoke plan

The next gate may simulate readiness using local checks only. P7 does not call external services. Smoke must confirm that production remains NoGo and that all runtime flags are still disabled unless a future dry-run branch explicitly approves otherwise.

## Markers

- ControlledRuntimePilotSmokePlanPrepared: true.
- RuntimePortalCallsEnabled: false.
- ProductivePortalNavigationEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
