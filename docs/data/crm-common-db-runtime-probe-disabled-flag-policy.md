# CRM Common DB Runtime Probe Disabled Flag Policy

The probe is present only to make future enablement explicit and reviewable.

Default flag state:

- `commonDbRuntimeProbeEnabled=false`
- `realDatabaseConfigured=false`
- `connectionStringsConfigured=false`
- `secretProviderRuntimeConnected=false`
- `dbConnectionAttemptedByRuntime=false`

Do not enable the probe until:

- Secret provider is approved.
- Common DB infrastructure is approved.
- Rollback and backup are documented.
- Synthetic data is defined.
- Portal Auth gate is clear.

No `.env`, real secret, private URL or password may be committed.
