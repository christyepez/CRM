# CRM Sprint 9 P2 Secret Provider Runtime Enablement Trial

Sprint 9 P2 enables a controlled Secret Provider runtime trial for NonProduction only. It is disabled by default and requires the explicit flag `Crm:RuntimeTrials:SecretProviderEnabled=true`.

Allowed logical secret names:
- `crm-common-db-connection`
- `crm-portal-auth-base-url`
- `crm-portal-auth-client-id`
- `crm-portal-auth-client-secret`
- `crm-observability-endpoint`

The trial returns sanitized metadata only. Secret values are never returned by API, logged, persisted or cached.

Production remains `NoGo`. P3 may consume only sanitized availability metadata, not secret values.
