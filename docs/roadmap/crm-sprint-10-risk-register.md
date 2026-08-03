# CRM Sprint 10 P1 - Risk Register

| Risk | Impact | Mitigation |
| --- | --- | --- |
| Preparation is interpreted as production approval | Runtime could be enabled too early | Keep `ProductionActivationDecision=NoGo` and `ProductiveRuntimeActivationDecision=NoGoForProduction` in API, docs and checks |
| Future flags default open | DB/Auth/routes could activate by accident | Require explicit NonProduction flags and fail-closed defaults |
| Endpoint becomes probe or mutation | Side effects enter a decision package | Sprint 10 P1 endpoint remains GET-only and static |
| Productive routes leak into default runtime | `/api/crm/leads`, `/accounts`, `/contacts` stop returning 404 | Preserve negative route checks |
| Prior Sprint 9 probes drift | P2/P3/P4/P5 stop returning 423 by default | Keep locked probe checks in validation scripts |

No blocker remains for Sprint 10 P2 planning.
