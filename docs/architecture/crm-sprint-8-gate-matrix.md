# CRM Sprint 8 Gate Matrix

| Capability | Default | Explicit NonProduction | Gate Decision |
| --- | --- | --- | --- |
| Secret Provider controlled read | Disabled | 423/fail-closed probe | GoOnlyAsExplicitNonProductionFlag |
| Common DB controlled connectivity | Disabled | 423/fail-closed probe | GoOnlyAsExplicitNonProductionFlag |
| Portal Auth controlled validation | Disabled | 423/fail-closed probe | GoOnlyAsExplicitNonProductionFlag |
| Locked route authorization policy | Not evaluated | 423 sanitized metadata | GoOnlyAsExplicitNonProductionLocked423 |
| Productive routes | 404 | Locked 423 only | NoGo by default |
| Productive CRUD | Disabled | Disabled | NoGo |
| DELETE | Disabled | Disabled | NoGo |
| EF runtime/migrations | Disabled | Disabled | NoGo |
| Productive UI | Disabled | Disabled | NoGo |

Sprint 9 readiness: planning only.
