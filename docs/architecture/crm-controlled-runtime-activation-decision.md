# CRM Controlled Runtime Activation Decision

This architecture decision keeps CRM in foundation mode while preparing the order of Sprint 9 runtime trials.

Allowed in P1:
- Read-only foundation contract.
- Static application service with no external I/O.
- Documentation, test and verification markers.

Not allowed in P1:
- Secret Provider runtime reads.
- Common DB connection attempts.
- Portal Auth HTTP/token/header validation.
- Productive route activation.
- Productive CRUD or DELETE.

Next gate: Sprint9P2SecretProviderRuntimeEnablementTrial.
