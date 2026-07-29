# CRM Secret Provider Runtime Enablement Trial Runbook

Default run:
- Keep `Crm:RuntimeTrials:SecretProviderEnabled=false`.
- Validate status endpoint.
- Probe endpoint must return 423 when disabled.

Controlled NonProduction trial:
- Use synthetic/non-real configuration only.
- Enable `Crm:RuntimeTrials:SecretProviderEnabled=true`.
- Probe only allow-listed names.
- Capture status code, elapsedMs and sanitized category.
- Do not use values for DB/Auth/Portal.

Production is blocked.
