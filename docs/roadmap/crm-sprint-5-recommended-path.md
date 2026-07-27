# CRM Sprint 5 Recommended Path

## Sprint 5 P3 result

P3 creates `CommonDbProbeOptionalActivation` only. The Common DB probe exists, but activation is not approved, the probe is disabled, no database connection is attempted and no EF/runtime/migration path is active. Portal Auth probe optional activation is the next gate: `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction`.

Recommended next package: `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction`.

## Sprint 5 P2 result

P2 creates `SecretProviderRuntimeContractValidation` only. No secrets are read, no `.env` is required, no Key Vault runtime client is configured and DB/Auth/Portal runtime activation remains blocked. Common DB probe optional activation is the next gate: `Sprint5P3CommonDbProbeOptionalActivationInNonProduction`.

Recommended next package: `Sprint5P3CommonDbProbeOptionalActivationInNonProduction`.

## Sprint 5 P1 result

P1 creates `ControlledRuntimeProbeActivationPlan` only. No runtime probe activation is approved. Secret provider validation is the next gate: `Sprint5P2SecretProviderRuntimeContractValidation`.

Recommended next package: `Sprint5P1ControlledRuntimeProbeActivationPlan`.

Sprint 5 should first design how runtime probes may be activated in non-production without introducing secrets, real customer data, productive routes, DELETE, Portal Auth production dependency or CRM-owned SQL Server.

Recommended sequence: P1 plan, P2 secret provider contract, P3 optional DB probe, P4 optional Portal Auth probe, P5 locked stub trial, P6 gate decision.

## P4 Portal Auth Probe Optional Activation
P4 adds a disabled-by-default Portal Auth probe optional activation contract. Next gate: Sprint5P5LockedProductiveRouteStubTrialInNonProduction.
