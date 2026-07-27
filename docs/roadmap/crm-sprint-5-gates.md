# CRM Sprint 5 Gates

## P2 Secret Provider Runtime Contract Validation

Decision: contract exists, runtime provider not connected and secret reads not enabled. Next: `Sprint5P3CommonDbProbeOptionalActivationInNonProduction`.

## P1 Controlled Runtime Probe Activation Plan

Decision: plan exists, activation not approved. Next: `Sprint5P2SecretProviderRuntimeContractValidation`.

| Gate | Purpose |
| --- | --- |
| `Sprint5P1ControlledRuntimeProbeActivationPlan` | Define controlled non-production runtime activation plan. |
| `Sprint5P2SecretProviderRuntimeContractValidation` | Validate secret provider contracts without real secrets in repo. |
| `Sprint5P3CommonDbProbeOptionalActivationInNonProduction` | Optionally activate common DB probe in non-production only after secret provider contract validation. |
| `Sprint5P4PortalAuthProbeOptionalActivation` | Optionally activate Portal Auth probe in non-production only. |
| `Sprint5P5LockedProductiveRouteStubTrial` | Trial locked route stubs without business execution. |
| `Sprint5P6GateDecision` | Decide whether Sprint 5 can close. |
