# CRM Sprint 4 Gates

## P5 non-production E2E pilot readiness

Decision: prepared for foundation-only E2E pilot. `Non-production E2E pilot readiness only; no real activation`.

Next: `Sprint4P6Sprint4GateDecision`.

## P4 productive routes locked stub validation

Decision: document-only preferred. `Productive routes locked stub validation only; no productive routes are active`.

Next: `Sprint4P5NonProductionE2EPilotReadiness`.

## P3 Portal Auth runtime probe

Decision: disabled probe only. `Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted`.

Next: `Sprint4P4ProductiveRoutesLockedStubValidation`.

## P2 common DB runtime probe

Decision: disabled probe only. `Common DB runtime probe exists but is disabled; no database connection is attempted`.

Next: `Sprint4P3PortalAuthRuntimeProbeBehindDisabledFlag`.

## P1 runtime readiness

Decision: local tooling hardening only. `Runtime readiness only; no real activation`.

Next: `Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag`.

Gate decisions:

- P1: tooling and local runtime evidence.
- P2: common DB probe remains disabled by default.
- P3: Portal Auth probe remains disabled by default.
- P4: productive route stubs stay locked.
- P5: non-production E2E uses synthetic data only.
- P6: formal GO/NO-GO before any activation.

Default decision remains `NoGoForRealActivation` until proven otherwise.
