# CRM Controlled Runtime Pilot Conditional Implementation Configuration Plan

## Future configuration plan

- Use logical placeholder names in repository content.
- Resolve real NonProduction values only through approved runtime configuration outside the repository.
- Keep all pilot flags disabled by default.
- Redact endpoint, credential and token metadata in evidence.

## Markers

- ConditionalImplementationConfigurationPlanPrepared: true.
- RealPortalPrivateUrlsPresent: false.
- RealCommonDbConnectionConfigured: false.
- RealSecretProviderConfigured: false.
- SecretsPresent: false.
- PrivateUrlsPresent: false.
