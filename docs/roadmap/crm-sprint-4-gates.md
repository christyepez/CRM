# CRM Sprint 4 Gates

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
