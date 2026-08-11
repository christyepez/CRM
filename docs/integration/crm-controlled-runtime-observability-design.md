# CRM Controlled Runtime Observability Design

P4 prepares observability design only. No real observability provider is configured.

## Future telemetry

- correlation identifier.
- environment name.
- feature flag state.
- sanitized adapter status.
- latency buckets.
- health result.

## Redaction

Telemetry must not include tokens, secrets, private URLs, connection strings, personal data or raw payloads.

## Markers

- ControlledRuntimeObservabilityDesignPrepared: true.
- RealObservabilityProviderConfigured: false.
- SecretsPresent: false.
- PrivateUrlsPresent: false.
