# CRM Common DB Runtime Probe Safety Gates

Safety gates before any future enablement:

| Gate | Default | Required before GO |
| --- | --- | --- |
| Secret provider | NoGo | Approved provider and rotation process. |
| Common DB infrastructure | NoGo | Common SQL host and logical `CrmDb` approved. |
| Rollback/backup | NoGo | Restore and rollback drill documented. |
| Synthetic data | NoGo | No personal or production data. |
| Portal Auth | NoGo | Authorization gate clarified before productive data access. |

P2 does not satisfy these gates; it only documents and exposes their state.
