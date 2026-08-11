# CRM Controlled Runtime Activation Sequence

This sequence is a future design, not active implementation.

## Future sequence

1. Confirm P2 Common DB and P3 Portal consumer contract evidence.
2. Confirm Portal Sprint 21 contract handoff remains valid.
3. Approve NonProduction-only pilot scaffold.
4. Enable safe metadata probes first.
5. Validate health and smoke checks with synthetic data.
6. Enable one integration adapter at a time behind explicit flags.
7. Keep production, productive navigation and productive Gateway routes disabled.

## Markers

- ControlledRuntimeActivationSequencePrepared: true.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- RuntimePortalCouplingEnabled: false.
