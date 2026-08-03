# CRM Portal Consumer Configuration Contract

CRM will consume Portal Configuration for future feature flags and integration settings through contract adapters only.

## Logical configuration keys

- `crm.runtime.portalIntegration.enabled`
- `crm.runtime.commonDb.enabled`
- `crm.runtime.productiveRoutes.enabled`
- `crm.runtime.notifications.enabled`
- `crm.runtime.audit.enabled`

Values are logical placeholders. P3 does not resolve real configuration from Portal.

## Markers

- CrmPortalConfigurationContractPrepared: true.
- PortalConfigurationDuplicated: false.
- RealSecretProviderConfigured: false.
- RealPortalPrivateUrlsPresent: false.
