# CRM Sprint 5 Gates

## P3 Common DB Probe Optional Activation

Decision: optional activation exists, activation not approved and no database connection is attempted. Next: `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction`.

## P2 Secret Provider Runtime Contract Validation

Decision: contract exists, runtime provider not connected and secret reads not enabled. Next: `Sprint5P3CommonDbProbeOptionalActivationInNonProduction`.

## P1 Controlled Runtime Probe Activation Plan

Decision: plan exists, activation not approved. Next: `Sprint5P2SecretProviderRuntimeContractValidation`.

| Gate | Purpose |
| --- | --- |
| `Sprint5P1ControlledRuntimeProbeActivationPlan` | Define controlled non-production runtime activation plan. |
| `Sprint5P2SecretProviderRuntimeContractValidation` | Validate secret provider contracts without real secrets in repo. |
| `Sprint5P3CommonDbProbeOptionalActivationInNonProduction` | Optionally activate common DB probe in non-production only after secret provider contract validation. |
| `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction` | Optionally activate Portal Auth probe in non-production only. |
| `Sprint5P5LockedProductiveRouteStubTrial` | Trial locked route stubs without business execution. |
| `Sprint5P6GateDecision` | Decide whether Sprint 5 can close. |

## P4 Gate
Portal Auth probe optional activation exists, approval is false, runtime connected is false, Portal HTTP attempted is false, token/header reads are false.
## P5 Gate

Locked productive route stub trial exists; registration approval is false, stubs are not registered, productive routes remain 404 by default, and the next gate is `Sprint5P6Sprint5GateDecision`.
