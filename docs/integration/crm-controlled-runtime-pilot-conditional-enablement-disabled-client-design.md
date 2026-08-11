# CRM Controlled Runtime Pilot Conditional Enablement Disabled Client Design

## Design

The future Portal client must be disabled by default and fail closed unless all approval flags and safe configuration checks pass.

## Required behavior

- Return a locked or disabled result when the master flag is false.
- Never call Portal while RuntimePortalCallsEnabled remains false.
- Never expose tokens, claims, secret values or endpoint values.
- Emit only sanitized metadata in logs and evidence.

## Markers

- ConditionalEnablementDisabledClientDesignPrepared: true.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- SsoOidcProductionConfigured: false.
- BrowserTokenStorageDetected: false.
