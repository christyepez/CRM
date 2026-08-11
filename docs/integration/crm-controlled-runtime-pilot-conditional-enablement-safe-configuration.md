# CRM Controlled Runtime Pilot Conditional Enablement Safe Configuration

## Placeholder configuration

| Setting | Placeholder | Rule |
| --- | --- | --- |
| Portal logical base | PORTAL_RUNTIME_LOGICAL_BASE | Logical name only; no network URL |
| Portal auth audience | PORTAL_AUTH_AUDIENCE_LOGICAL | Logical value only |
| Client credential name | CRM_PORTAL_CLIENT_SECRET_LOGICAL_NAME | Secret marker only |
| Common DB logical connection | CRM_COMMON_DB_CONNECTION_LOGICAL_NAME | Secret marker only |
| Observability destination | CRM_OBSERVABILITY_LOGICAL_DESTINATION | Logical marker only |

## Markers

- ConditionalEnablementSafeConfigurationPrepared: true.
- RealPortalPrivateUrlsPresent: false.
- RealCommonDbConnectionConfigured: false.
- RealSecretProviderConfigured: false.
- RealObservabilityProviderConfigured: false.
- SecretsPresent: false.
- PrivateUrlsPresent: false.
- EnvRealFileCommitted: false.
