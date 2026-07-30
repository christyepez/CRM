# CRM Secret Provider Runtime Enablement Trial Policy

Policy:
- NonProduction-only.
- Disabled by default.
- Explicit flag required.
- Allow-list enforced.
- Metadata-only.
- Fail closed.
- Rollback is disabling `Crm:RuntimeTrials:SecretProviderEnabled`.

Forbidden:
- `.env` or real secret values in repository.
- Secrets in `appsettings`.
- Public contracts that materialize secret values.
- DB/Auth/Portal runtime usage from P2.
- Production Secret Provider runtime.
