# CRM Secret Provider Safe Mock Policy

The safe mock provider is allowed only for non-production contract validation.

Allowed:

- Deterministic in-memory synthetic values.
- Logical names that represent future dependencies.
- Metadata stating `Synthetic=true`, `Sensitive=false` and `RuntimeUsable=false`.

Forbidden:

- Real secret reads.
- `.env` creation or reads.
- File reads for secrets.
- Environment variable reads for sensitive values.
- Key Vault clients.
- Azure SDK secret clients.
- Connection strings.
- DB, Portal Auth or productive runtime activation.
- Logging real secret values.
