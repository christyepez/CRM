# Sprint 7 P1 update

Sprint 7 starts with `SecretProviderRealNonProductionApproval`.

P1 creates the approval package only. The recommended path is to keep approval false until security, architecture, DevOps, rollback and observability evidence are complete.

Next gate: `Sprint7P2SecretProviderRealNonProductionRuntimeProbe`.

# CRM Sprint 7 Recommended Path

Recommended path:

- Sprint 7 P1: Secret Provider Real NonProduction Approval.
- Sprint 7 P2: Secret Provider Real NonProduction Runtime Probe.
- Sprint 7 P3: Common DB Real Connectivity NonProduction Probe.
- Sprint 7 P4: Portal Auth Real Runtime Probe.
- Sprint 7 P5: Locked Productive Route Runtime Registration With 423.
- Sprint 7 P6: Sprint 7 Gate Decision.

Do not implement Sprint 7 runtime activation until each prior approval gate passes.

## Sprint 7 P2 update

P2 prepares `SecretProviderRealNonProductionRuntimeProbe` as a skipped runtime probe. Approval remains false, no real secret values are read, and the next gate is `Sprint7P3CommonDbRealConnectivityNonProductionProbe`.
