# CRM Secrets and Connection Management Strategy

Status: `StrategyOnly`.

Rules:
- No hardcoded values.
- No `.env` file committed or required.
- No secrets in the repository.
- No production URL, token, password or private endpoint in source.
- Future configuration source: Portal Configuration, external secret provider or secure runtime environment.

Sprint 3 P2 must define the common DB connection contract and secret injection strategy before any runtime persistence work.
