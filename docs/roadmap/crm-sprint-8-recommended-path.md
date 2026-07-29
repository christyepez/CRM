# CRM Sprint 8 Recommended Path

Recommended packages:

- Sprint 8 P1: Secret Provider Approval Decision.
- Sprint 8 P2: Secret Provider Controlled Real NonProduction Read.
- Sprint 8 P3: Common DB Controlled Real Connectivity.
- Sprint 8 P4: Portal Auth Controlled Real Runtime Validation.
- Sprint 8 P5: Locked Route Authorization Policy Integration.
- Sprint 8 P6: Sprint 8 Gate Decision.

Do not implement Sprint 8 runtime behavior until each explicit approval gate is satisfied.
## P1 - Secret Provider Approval Decision

Sprint 8 starts with a planning-only approval decision. P1 approves moving to P2 controlled NonProduction read planning, but performs no real secret read and exposes no values.

Next gate: `Sprint8P2SecretProviderControlledRealNonProductionRead`.
