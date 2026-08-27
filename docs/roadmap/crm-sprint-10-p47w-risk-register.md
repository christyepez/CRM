# CRM P47W Risk Register

| Risk | Severity | Status | Mitigation |
| --- | --- | --- | --- |
| User expects a web UI at `/`. | Medium | Mitigated | Classified API-only; frontend is outside first slice scope. |
| Swagger unavailable in Production environment. | Low | Accepted | Do not weaken Production-like security or rebuild candidate. |
| Local simulation confused with real Production. | Critical | Mitigated | Packet and manifests state `RealProduction: false`. |
| Azure Container Apps real Production remains deferred. | Medium | Accepted | Future real Production track required. |
