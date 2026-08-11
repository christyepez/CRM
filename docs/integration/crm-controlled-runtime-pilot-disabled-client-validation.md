# CRM Controlled Runtime Pilot Disabled Client Validation

## Validation

The disabled Portal client contract remains a seam only. P6 does not add a real HTTP client, private endpoint, SSO/OIDC configuration, client credential, token handling or credential storage.

## Markers

- ControlledRuntimePilotDisabledClientValidationPrepared: true.
- ControlledRuntimePilotDisabledClientPrepared: true.
- RuntimePortalCallsEnabled: false.
- RealPortalPrivateUrlsPresent: false.
- SsoOidcProductionConfigured: false.
- RealSecretProviderConfigured: false.
- BrowserTokenStorageDetected: false.
