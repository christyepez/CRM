# CRM Common DB Probe Activation Gates

P3 does not approve activation. Future non-production activation requires:

| Gate | Owner | P3 status | Required evidence |
| --- | --- | --- | --- |
| Secret Provider runtime approval | Security | Not approved | Secret provider approvals closed and no real values in repository. |
| Synthetic data approval | Data Architect | Not approved | Synthetic-only data approved for probe execution. |
| Shared SQL boundary approval | Architecture Governance | Not approved | Reuse common SQL container; no CRM-owned SQL Server. |
| Rollback approval | DevOps | Not approved | Disable flag and health regression procedure approved. |
| Observability approval | QA Lead | Not approved | Health, logs without sensitive values and negative routes covered. |

Next gate: `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction`.
