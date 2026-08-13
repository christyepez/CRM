# CRM Controlled Runtime Pilot First Slice Scaffold Health Smoke

## Local health/smoke

The health/smoke check is local and reports disabled/no-op metadata. It must not call Portal, read secrets, read tokens or connect to Common DB.

## Markers

- FirstSliceScaffoldHealthSmokePrepared: true.
- RuntimePortalCouplingEnabled: false.
