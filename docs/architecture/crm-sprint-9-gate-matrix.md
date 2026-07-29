# CRM Sprint 9 Gate Matrix

| Gate | Scope | P1 Decision | Enabled Now |
| --- | --- | --- | --- |
| P2 | Secret Provider runtime enablement trial | ApprovedForNonProductionTrialsOnly | false |
| P3 | Common DB runtime connectivity trial | ApprovedForNonProductionTrialsOnly | false |
| P4 | Portal Auth runtime validation trial | ApprovedForNonProductionTrialsOnly | false |
| P5 | Productive Route dry-run trial | ApprovedForNonProductionTrialsOnly | false |
| Production | Real activation | NoGo | false |

All gates require explicit NonProduction flags, rollback, observability and fail-closed behavior.
