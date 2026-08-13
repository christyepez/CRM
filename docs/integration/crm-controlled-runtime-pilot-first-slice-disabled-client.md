# CRM Controlled Runtime Pilot First Slice Disabled Client

## Design

The future first slice client must fail closed. When disabled, it returns a locked or disabled status and performs no external calls.

## Required behavior

- No Portal calls by default.
- No token persistence.
- No secret values exposed.
- Sanitized evidence only.

## Markers

- FirstSliceDisabledClientPrepared: true.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- BrowserTokenStorageDetected: false.
