# CRM Sprint 4 Gate Matrix

| Capability | Gate result | Evidence | Productive activation |
| --- | --- | --- | --- |
| Runtime readiness | Prepared | P1 health/tooling | No |
| Common DB runtime probe | Exists disabled | P2 disabled probe | No |
| Portal Auth runtime probe | Exists disabled | P3 disabled probe | No |
| Productive routes locked stub | DocumentOnlyPreferred | P4 docs/guardrails | No |
| Non-production E2E readiness | Prepared | P5 health/negative checks | Foundation only |
| DELETE | NoGo | Guardrails | No |
| DB runtime / EF / migrations | NoGo | Guardrails | No |
| Secrets / token propagation | NoGo | Security scans | No |
| Audit / observability | Foundation evidence only | Health/build/tooling | No real runtime |
| Backup / rollback | Deferred | No durable persistence | Sprint 5+ |
| CI/CD readiness | Local verification | Build/test/docker | Future automation |
