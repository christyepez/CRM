# CRM Secret Provider Approval Gates

P2 does not approve runtime activation. The following gates must pass before any future secret read:

| Gate | Owner | P2 status | Required evidence |
| --- | --- | --- | --- |
| Provider approval | Security | Not approved | Provider and least-privilege access model approved. |
| Logical names approval | Architecture Governance | Not approved | Contract-only names approved without values. |
| Masking/logging approval | Security | Not approved | No secret values in responses, logs or telemetry. |
| Rotation policy approval | DevOps | Not approved | Rotation and rollback runbook approved. |
| Synthetic non-production approval | Data Architect | Not approved | Synthetic data and non-production-only scope approved. |

Next gate: `Sprint5P3CommonDbProbeOptionalActivationInNonProduction`.
