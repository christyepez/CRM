# CRM OPS-04 Risk Register

| Risk | Severity | Status | Mitigation |
| --- | --- | --- | --- |
| Local simulation could be confused with real Production. | Critical | Mitigated | Every artifact states `RealProduction: false` and `SimulatedProduction: true`. |
| `/api/crm/readiness` reports planned integrations from current contract. | Medium | Open | P47W must bind scope to no Portal/Common DB services in compose and document current endpoint semantics. |
| Docker healthcheck cannot use curl/wget in runtime image. | Low | Mitigated | Healthcheck validates local TCP listener; HTTP endpoints are validated externally. |
| Azure Container Apps real Production path remains deferred. | Medium | Accepted | Separate future real Production architecture/provisioning work. |
