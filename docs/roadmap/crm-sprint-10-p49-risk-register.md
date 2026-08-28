# CRM Sprint 10 P49 - Risk Register

P49RiskRegisterExists: true

| Risk | Status | Mitigation |
| --- | --- | --- |
| Local simulated Production could be mistaken for corporate Production. | Accepted | All artifacts keep `RealProduction=false` and `LocalSimulation=true`. |
| `/` and `/swagger` return 404. | Accepted | Current scope is API-only health/readiness surface. |
| P48 approval reuse after execution. | Closed | Approval was consumed; future retry requires new approval. |
| Hidden Portal/Common DB runtime coupling. | Monitored | Compose config and logs show Portal/Common DB disabled/not included. |
| Local Docker host variance. | Accepted | Evidence is scoped to `localhost-local-docker-engine` only. |
