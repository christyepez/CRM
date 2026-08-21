# P43 Production Test Matrix

QAProductionReadiness: ReadyForApproval

| TestId | Category | Description | Environment | ExecutionPhase | ExpectedResult | Blocking | Evidence | OwnerRole |
| --- | --- | --- | --- | --- | --- | --- | --- | --- |
| P43-T01 | PreDeployment | Build solution | CI/NonProduction | PreDeployment | pass | Yes | `dotnet build CRM.sln` | QA Lead |
| P43-T02 | PreDeployment | Unit/architecture tests | CI/NonProduction | PreDeployment | 281 pass | Yes | `dotnet test CRM.sln --no-build` | QA Lead |
| P43-T03 | Smoke | health | NonProduction/P45 | PostDeployment | 200 | Yes | health check | DevOps |
| P43-T04 | Smoke | readiness | NonProduction/P45 | PostDeployment | 200 | Yes | readiness check | DevOps |
| P43-T05 | Functional | CRM readiness API | NonProduction/P45 | PostDeployment | 200 | Yes | readiness API | QA |
| P43-T06 | Negative | locked routes | NonProduction/P45 | PostDeployment | remain safe | Yes | guardrails | Security |
| P43-T07 | Security | secret/private URL scan | Repo | PreDeployment | pass | Yes | scan | Security |
| P43-T08 | Performance | latency baseline | NonProduction | PreDeployment | captured | No | perf doc | Performance |
| P43-T09 | Resilience | restart count | NonProduction | PreDeployment | no loop | Yes | docker ps/inspect | DevOps |
| P43-T10 | Rollback | rollback model | Docs | PreDeployment | prepared | Yes | rollback doc | DevOps |
