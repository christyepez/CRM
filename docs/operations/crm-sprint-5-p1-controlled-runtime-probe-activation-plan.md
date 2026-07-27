# CRM Sprint 5 P1 - Controlled Runtime Probe Activation Plan

Status: `ControlledRuntimeProbeActivationPlan`.

This package creates a plan only. No runtime probe activation is approved in Sprint 5 P1.

Default decisions:

- `runtimeProbeActivationPlanExists=true`.
- `runtimeProbeActivationApproved=false`.
- `commonDbProbeActivationApproved=false`.
- `portalAuthProbeActivationApproved=false`.
- `productiveRoutesActivationApproved=false`.
- `realActivationApproved=false`.
- `nonProductionOnly=true`.
- `syntheticDataRequired=true`.
- `rollbackPlanRequired=true`.
- `observabilityRequired=true`.
- `secretProviderRequired=true`.
- `deleteStillNoGo=true`.

Next gate: `Sprint5P2SecretProviderRuntimeContractValidation`.

The plan separates common DB probe, Portal Auth probe and productive route locked stubs. Each future activation requires formal approval, secret provider validation, synthetic data, rollback, observability, health checks, negative route checks and no DELETE.
