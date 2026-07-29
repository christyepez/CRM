# CRM Sprint 9 Recommended Path

## P1 decision

Sprint 9 starts with `ApprovedForNonProductionTrialsOnly`. The recommended path is:

1. P2 Secret Provider runtime enablement trial.
2. P3 Common DB runtime connectivity trial.
3. P4 Portal Auth runtime validation trial.
4. P5 Productive Route dry-run trial.
5. P6 Sprint 9 closure gate.

Production activation remains `NoGo`.

Recommended sequence:

- Sprint 9 P1: Controlled Runtime Activation Decision.
- Sprint 9 P2: Secret Provider Runtime Enablement Trial.
- Sprint 9 P3: Common DB Runtime Connectivity Trial.
- Sprint 9 P4: Portal Auth Runtime Validation Trial.
- Sprint 9 P5: Productive Route Dry-Run Trial.
- Sprint 9 P6: Sprint 9 Gate Decision.

Each step must remain NonProduction, reversible, observable and explicitly gated.
