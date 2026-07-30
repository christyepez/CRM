# CRM Productive Route Dry Run Trial Architecture

P5 adds a foundation dry-run layer only.

Components:
- Application contracts and status service define the P5 safety posture.
- `CrmProductiveRouteDryRunTrialEvaluator` is pure and performs no I/O.
- API-level `ProductiveRouteDryRunTrialService` converts evaluator decisions into sanitized probe metadata.
- Foundation endpoints expose status and probe results.

Architecture boundaries:
- No CRM Identity.
- No Portal Auth enforcement.
- No Authorization header or token reads by default.
- No database runtime, EF runtime, migrations or schema changes.
- No productive CRM route registration by default.
- No DELETE routes.
- No side effects.

P5 depends only on sanitized metadata availability from:
- Sprint 9 P2 Secret Provider Runtime Enablement Trial.
- Sprint 9 P3 Common DB Runtime Connectivity Trial.
- Sprint 9 P4 Portal Auth Runtime Validation Trial.

Next gate:
- Sprint9P6Sprint9GateDecision.
