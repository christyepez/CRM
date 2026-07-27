# CRM Sprint 6 Recommended Path

Recommended sequence:

1. Sprint 6 P1: NonProduction Runtime Approval Package - created, approvals not granted.
2. Sprint 6 P2: Secret Provider Safe Mock Activation.
3. Sprint 6 P3: Common DB Connectivity Dry-Run Contract.
4. Sprint 6 P4: Portal Auth Token Propagation Dry-Run Contract.
5. Sprint 6 P5: Locked Stub Runtime Registration Trial.
6. Sprint 6 P6: Sprint 6 Gate Decision.

Do not implement Sprint 6 runtime activation before P1 approvals. After P1, the only recommended next gate is `Sprint6P2SecretProviderSafeMockActivation`, still without real secret reads.
