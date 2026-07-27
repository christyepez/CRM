# CRM Sprint 5 Gate Decision

## Decision

Sprint 5 gate decision: `GoForControlledNonProductionPreparation`.

## No-Go decisions

- RealActivationDecision: NoGo.
- SecretProviderRuntimeDecision: NoGoForRuntimeRead.
- CommonDbRuntimeDecision: NoGoForConnectionAttempt.
- PortalAuthRuntimeDecision: NoGoForPortalHttpOrTokenRead.
- ProductiveRoutesDecision: NoGo.
- LockedStubRuntimeDecision: NoGoForRuntimeRegistration.
- ProductiveCrudDecision: NoGo.
- DeleteDecision: NoGo.
- ProductiveUiDecision: NoGo.

## Go decision

- Sprint6PlanningDecision: Go.
- NextGate: Sprint6P1NonProductionRuntimeApprovalPackage.
