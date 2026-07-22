# CRM Sprint 2 Open Risks

Open risks:
- Durable persistence could be activated without rollback, backup or restore governance.
- Portal Auth runtime could be bypassed if CRM implements its own authentication.
- Foundation CRUD could be confused with productive CRUD.
- Productive routes could be exposed before database and security gates.
- Docker build can depend on external Microsoft Container Registry availability.

Mitigation:
- Keep architecture tests and verifiers active.
- Keep foundation route prefix.
- Keep `ProductizationStatus=NotReady`.
- Start Sprint 3 with design and disabled gates only.
