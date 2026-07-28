# CRM Secret Provider Real NonProduction Approval Policy

Approval is not granted by default. P1 only documents the conditions required before P2 can run a controlled NonProduction runtime probe.

Required before P2:

- External secret scope defined outside the repository.
- Least-privilege access approved.
- Owner and rotation process identified.
- Rollback approved.
- Observability approved with sanitized logs.
- Security, architecture and DevOps reviews complete.

Prohibited in P1:

- Reading real secrets.
- Reading `.env`.
- Reading sensitive environment variables.
- Creating a runtime secret store client.
- Using any runtime SDK for secrets.
- Logging secret values.
- Adding real connection strings.
