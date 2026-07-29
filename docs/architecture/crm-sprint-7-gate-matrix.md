# CRM Sprint 7 Gate Matrix

| Area | Decision | Evidence |
| --- | --- | --- |
| Secret Provider approval package | PreparedOnly | Approval exists; granted false |
| Secret Provider runtime probe | NoGo | Disabled; real secret read false |
| Common DB real connectivity | NoGo | Disabled; connection string resolved false |
| Portal Auth real runtime | NoGo | Skipped; Portal HTTP and token/header reads false |
| Locked productive route registration | GoOnlyAsExplicitNonProductionLocked423 | Default 404; explicit NonProduction 423 |
| Productive routes default | NoGo | Routes are not registered by default |
| Productive CRUD | NoGo | No domain execution or stores |
| DELETE | NoGo | No DELETE routes |
| EF runtime / migrations | NoGo | No runtime or migration activation |
| SQL Server compose | NoGo for CRM-owned SQL | CRM compose has no SQL Server |
| Observability / rollback | Required | Required before Sprint 8 runtime decisions |
