# CRM Sprint 9 Recommended Path

## P1 decision

Sprint 9 starts with `ApprovedForNonProductionTrialsOnly`. The recommended path is:

1. P2 Secret Provider runtime enablement trial.
2. P3 Common DB runtime connectivity trial.
3. P4 Portal Auth runtime validation trial.
4. P5 Productive Route dry-run trial.
5. P6 Sprint 9 closure gate.

Production activation remains `NoGo`.

P2 is complete when Secret Provider returns only sanitized metadata and remains disabled by default.

P3 is complete when Common DB connectivity returns only sanitized metadata and remains disabled by default.

P4 is complete when Portal Auth runtime validation returns only sanitized metadata, remains disabled by default, returns 423 from the probe by default, and does not read Authorization headers or tokens.

Recommended sequence:

- Sprint 9 P1: Controlled Runtime Activation Decision.
- Sprint 9 P2: Secret Provider Runtime Enablement Trial.
- Sprint 9 P3: Common DB Runtime Connectivity Trial.
- Sprint 9 P4: Portal Auth Runtime Validation Trial.
- Sprint 9 P5: Productive Route Dry-Run Trial.
- Sprint 9 P6: Sprint 9 Gate Decision.

Each step must remain NonProduction, reversible, observable and explicitly gated.

Sprint 9 P5 is now implemented as a disabled-by-default foundation dry-run: productive routes remain 404 by default, the probe returns 423 by default and no CRUD, DELETE, DB runtime, Auth enforcement or side effects are enabled.

Sprint 9 P6 closes the sprint as `GoForSprint10ControlledProductizationReadinessPlanning`; production activation remains `NoGo`, productization remains `NotReady` and the next gate is `Sprint10P1ProductizationReadinessDecision`.
