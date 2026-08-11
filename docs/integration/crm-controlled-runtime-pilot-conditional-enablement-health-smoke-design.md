# CRM Controlled Runtime Pilot Conditional Enablement Health and Smoke Design

## Design

P10 defines a future non-destructive smoke path. It does not execute external Portal calls.

## Future smoke checks

- Local CRM health remains available.
- Portal checks must use a disabled placeholder until a future implementation package approves runtime calls.
- Smoke must never write CRM data, Portal data or shared database data.
- Smoke evidence must redact endpoint, token and secret details.

## Markers

- ConditionalEnablementHealthSmokeDesignPrepared: true.
- RuntimePortalCallsEnabled: false.
- RealDataPresent: false.
- CommonDbRuntimeEnabled: false.
