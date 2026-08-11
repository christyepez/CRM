# CRM Controlled Runtime Pilot Disabled Client Contract

## Purpose

Prepare a future Portal client seam without creating runtime coupling in this sprint.

## Contract rules

- The default client is disabled.
- It must fail closed.
- It must not resolve private Portal endpoints.
- It must not perform network calls.
- It must not read or store tokens.
- It must return sanitized readiness metadata only when represented in future status outputs.

## Markers

- ControlledRuntimePilotDisabledClientPrepared: true.
- RuntimePortalCallsEnabled: false.
- RealPortalPrivateUrlsPresent: false.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- BrowserTokenStorageDetected: false.
