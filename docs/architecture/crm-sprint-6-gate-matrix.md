# CRM Sprint 6 Gate Matrix

| Area | Decision | Evidence |
| --- | --- | --- |
| NonProduction approval package | PreparedOnly | P1 exists; approvals remain false |
| Secret Provider safe mock | GoForMockOnly | Synthetic values only |
| Common DB dry-run | NoGoForRealConnection | Connection attempts false |
| Portal Auth token propagation | NoGoForRealRuntime | Token/header/Portal HTTP attempts false |
| Locked stub registration | NoGo | Routes remain unregistered and 404 |
| Productive routes / CRUD / DELETE | NoGo | Negative route checks pass |
| EF / migrations / SQL Server | NoGo | No DB runtime or SQL Server compose |
| Observability / rollback | Required | Sprint 7 gates must include explicit evidence |
