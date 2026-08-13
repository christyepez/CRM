# CRM Controlled Runtime Pilot First Slice Health and Smoke

## Design

The first slice smoke should validate local disabled state only. It must not call Portal, write data, use Common DB runtime or require real credentials.

## Markers

- FirstSliceHealthSmokePrepared: true.
- RuntimePortalCallsEnabled: false.
- CommonDbRuntimeEnabled: false.
- RealDataPresent: false.
