# CRM Runtime Probe Secret Handling Policy

Secret provider validation is required before any future probe activation.

Rules:

- No secrets in repository.
- No `.env` or `.env.local`.
- No real connection strings.
- No token/header reads in Sprint 5 P1.
- Logs must redact secret names, values, passwords, certificates and tokens.

Next gate: `Sprint5P2SecretProviderRuntimeContractValidation`.
