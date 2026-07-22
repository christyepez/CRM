# CRM DB Secret Provider Contract

Allowed future sources:
- Portal Configuration.
- External secret provider.
- Environment vault.
- CI/CD secure variables.

Forbidden sources:
- Repository files.
- `.env`.
- appsettings with real values.
- Passwords, tokens or private URLs committed to source.

Current status: `ContractOnly`. No runtime provider is configured or connected.
