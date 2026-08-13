# CRM Controlled Runtime Pilot First Slice NonProduction Activation Plan

## Scope

This plan defines the conditions for a future controlled NonProduction activation. It does not enable runtime Portal calls, routes, navigation, Common DB runtime or production paths.

## Required posture

- Activation is future-only.
- Flags remain disabled by default.
- Configuration remains logical placeholder-only.
- P17 must perform a dry run before any real activation.

## Markers

- FirstSliceNonProductionActivationPlanAttempted: true.
- FirstSliceNonProductionActivationPlanPrepared: true.
- NonProductionActivationPlanOnly: true.
- NonProductionActivationExecuted: false.
