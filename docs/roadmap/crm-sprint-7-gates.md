# Sprint 7 P1 gate

Decision: Approval package exists; approval not granted.

NoGo remains for real secret reads, runtime provider connection, DB runtime, Portal Auth runtime, productive routes, DELETE and productive UI.

Next gate: `Sprint7P2SecretProviderRealNonProductionRuntimeProbe`.

# CRM Sprint 7 Gates

Sprint 7 gate sequence:

| Gate | Objective | Activation |
| --- | --- | --- |
| P1 | Approve real secret provider in NonProduction | Approval only |
| P2 | Probe real secret provider runtime | NonProduction only |
| P3 | Probe common DB real connectivity | NonProduction only |
| P4 | Probe Portal Auth real runtime | NonProduction only |
| P5 | Register locked productive route stubs with 423 | NonProduction only |
| P6 | Decide Sprint 7 gate | No production merge without approval |

Production remains out of scope.

## Sprint 7 P2 gate

Decision: Runtime probe exists; approval is not granted, so probe is skipped.

NoGo remains for real secret reads, DB runtime, Portal Auth runtime, productive routes, DELETE and productive UI.

Next gate: `Sprint7P3CommonDbRealConnectivityNonProductionProbe`.

## Sprint 7 P3 gate

Decision: Common DB real connectivity probe exists; Secret Provider approval is not granted, so the connection probe is skipped.

NoGo remains for real connection strings, DB runtime, EF runtime, migrations, Portal Auth runtime, productive routes, DELETE and productive UI.

Next gate: `Sprint7P4PortalAuthRealRuntimeProbe`.

## Sprint 7 P4 gate

Decision: Portal Auth real runtime probe exists; Portal Auth approval is not granted, so the Portal probe is skipped.

NoGo remains for Portal HTTP, Portal Auth base URL resolution, token/header reads, Auth middleware, login/logout, CRM Identity, roles/permissions persistence, productive routes, DELETE and productive UI.

Next gate: `Sprint7P5LockedProductiveRouteRuntimeRegistrationWith423`.
## Sprint 7 P5 Gate

Status: `LockedProductiveRouteRuntimeRegistrationWith423`.

Decision: route shapes may be registered only as locked NonProduction stubs returning `423` behind explicit flag. Default remains `404`. Productive activation remains `NoGo`.

Next: `Sprint7P6Sprint7GateDecision`.
