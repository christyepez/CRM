# CRM Productization Gates

## Sprint 5 P3 common DB probe optional activation

Sprint 5 P3 does not change productization readiness. Common DB optional activation exists, but activation, database connection, EF runtime, migrations, durable persistence and API database dependency remain not approved.

## Sprint 5 P2 secret provider contract

Sprint 5 P2 does not change productization readiness. Secret Provider contract exists, but runtime connection, secret reads, common DB probe activation, Portal Auth probe activation and runtime probe activation remain not approved.

## Sprint 5 P1 runtime probe plan

Sprint 5 P1 does not change productization readiness. Runtime probe activation, common DB probe activation, Portal Auth probe activation, productive route activation and real activation remain not approved.

## Sprint 4 closure

Sprint 4 closes as foundation-only. Productization remains `NotReady`; real activation, durable persistence, common DB runtime, Portal Auth runtime, productive routes, DELETE and productive UI remain `NoGo`.

Productization remains `NotReady` until:

- Persistence design is approved.
- Portal authorization integration is approved.
- API versioning and security are approved.
- UI shell avoids local login/token storage.
- Integration contracts are stable.
- Testing strategy includes regression, architecture and security checks.
- Docker build is validated outside `BLOCKED_EXTERNAL_REGISTRY` conditions.

No production activation may bypass these gates.

Sprint 2 P1 adds persistence readiness metadata but keeps productization `NotReady`.
## P2 gate result

Foundation store seam is active, but productization remains `NotReady` until Portal authorization simulation, migration strategy approval and durable persistence activation are completed.
## P3 gate result

P3 completes the Portal authorization simulation gate, but productization remains `NotReady` until controlled CRUD and durable persistence are explicitly approved.

P4 completes controlled foundation CRUD previews, but productization remains `NotReady` until real Portal authorization and durable persistence are approved.

P5 confirms productization remains `NotReady`. Continue to P6 for an explicit productization gate decision.

P6 closes Sprint 2 with `OverallDecision=NoGoForProductiveActivation`. Sprint 3 Planning is `Go` and the next gate is `Sprint3P1DurablePersistenceSetupDesign`.

Sprint 3 P1 keeps productization blocked: Durable Persistence is `DesignOnly`, Real DB is `NotConfigured`, EF Runtime is `NotEnabled`, migrations are `PlannedOnly`, and the next gate is `Sprint3P2CommonDbConnectionContractAndSecretStrategy`.

Sprint 3 P2 keeps productization blocked: common DB and secret strategy are `ContractOnly`, no real secrets or connection values are configured, and the next gate is `Sprint3P3EfDbContextPrototypeBehindDisabledFlag`.
# Sprint 3 P3 productization status

Productization remains `NoGo`. The EF/DbContext prototype is disabled and does not enable productive CRUD, DB runtime or Portal authorization runtime.

# Sprint 3 P4 productization status

Productization remains `NoGo`. Portal Auth runtime is contract-only and does not enable middleware, guards, credential storage or productive CRM routes.

# Sprint 3 P5 productization status

Productization remains `NoGo`. Productive API routes are draft-only and not registered.
# Sprint 3 P6 closure

Sprint 3 closes with `OverallDecision=NoGoForRealActivation`. Foundation capabilities remain `GoFoundationOnly`, Sprint 4 planning is `Go`, and the next gate is `Sprint4P1RuntimeEnvironmentReadinessAndLocalToolingHardening`.

## Sprint 4 P1 runtime readiness

Sprint 4 P1 starts runtime environment readiness and local tooling hardening. Productization remains `NotReady`; next gate is `Sprint4P2ControlledCommonDbRuntimeProbeBehindDisabledFlag`.

## Sprint 4 P2 common DB runtime probe

Sprint 4 P2 keeps productization blocked: common DB runtime probe is present but disabled, no connection is attempted, no connection values are configured and CRM still owns no SQL Server container.

## Sprint 4 P3 Portal Auth runtime probe

Sprint 4 P3 keeps productization blocked: Portal Auth runtime probe is present but disabled, no token is read, no Portal runtime call is attempted and CRM still does not own login, Identity, roles or permissions.

## Sprint 4 P4 productive routes locked stub validation

Sprint 4 P4 keeps productization blocked: productive route stubs remain document-only, no productive routes are registered, no DELETE endpoint exists and foundation CRUD stays separate.

## Sprint 4 P5 non-production E2E pilot readiness

Sprint 4 P5 keeps productization blocked: E2E pilot is foundation-only, synthetic-data-only and requires negative route validation before P6.

## Sprint 5 P4 Productization Gate
Productive authorization remains disabled. CRM still has no login, Identity, token storage or persisted permissions.
## Sprint 5 P5 Productization Gate

Productive route registration remains blocked. CRM still has no productive CRUD, DELETE, DB runtime, Auth runtime or Portal runtime.
## Sprint 5 P6 Productization Gate

Productization remains `NotReady`. Real activation, productive CRUD, DELETE, DB runtime, Auth runtime, Portal runtime and productive UI remain NoGo.

## Sprint 6 P1 Productization Gate

The non-production runtime approval package exists, but all approvals remain false. Productization remains `NotReady`; real activation, productive routes, DELETE, secret reads, DB runtime, Portal Auth runtime and locked stub runtime registration remain blocked.

## Sprint 6 P2 Productization Gate

Secret Provider safe mock exists and is enabled only for synthetic values. Productization remains `NotReady`; real secrets, DB runtime, Portal Auth runtime, productive routes, DELETE and real activation remain blocked.

## Sprint 6 P3 Productization Gate

Common DB connectivity dry-run contract exists, but approval, connection attempts, real connection strings, EF runtime and migrations remain disabled. Productization remains `NotReady`.
## Sprint 6 P4 Productization Gate

Portal Auth token propagation is No-Go for productive activation. CRM remains foundation-only and must not read tokens/headers, call Portal, enable Auth middleware, implement Identity or persist permissions.
## Sprint 6 P5 Productization Gate

Locked stub runtime registration remains No-Go for productive activation. CRM keeps productive routes unregistered by default; future 423 Locked behavior requires explicit NonProduction approval.
# Sprint 7 P1 productization gate

Productization remains `NotReady`.

Secret Provider real NonProduction approval package exists, but real secret access is not approved. Productive activation remains blocked.

# Sprint 6 P6 productization gate

Sprint 6 closes with Productization Status `NotReady`.

Allowed next work: Sprint 7 controlled NonProduction activation planning.

Blocked until explicit gate approval:

- Real secret provider runtime.
- Real common DB connection.
- Portal Auth runtime.
- Locked productive route runtime registration.
- Productive routes, productive CRUD, DELETE and productive UI.

# Sprint 7 P2 productization gate

Productization remains `NotReady`.

Secret Provider real NonProduction runtime probe exists but is skipped because approval is not granted. Real activation remains blocked.

# Sprint 7 P3 productization gate

Productization remains `NotReady`.

Common DB real connectivity probe exists but is skipped because Secret Provider approval is not granted. DB runtime, EF runtime, migrations and productive persistence remain blocked.

# Sprint 7 P4 productization gate

Productization remains `NotReady`.

Portal Auth real runtime probe exists but is skipped because Portal Auth approval is not granted. Portal HTTP, Portal URL resolution, token/header reads, Auth middleware, CRM Identity, login/logout, roles/permissions persistence and productive routes remain blocked.
## CRM Sprint 7 P5

Locked productive route runtime registration exists only as a safe NonProduction gate. It does not approve productive CRUD, persistence, Portal Auth runtime, DELETE endpoints or UI. Default negative route status remains `404`; explicit locked route status is `423`.
## CRM Sprint 7 P6

Sprint 7 is closed. Productization remains `NotReady`; real activation, productive CRUD, DELETE, productive UI, real DB and Portal Auth runtime remain `NoGo`. Sprint 8 planning is approved for controlled runtime approval and pilot planning.
## CRM Sprint 8 P1

Secret Provider approval decision exists for controlled NonProduction read planning. No real secret read occurs in P1. Productization remains `NotReady` and real activation remains `NoGo`.

## CRM Sprint 8 P2

Controlled real NonProduction Secret Provider read scaffold exists, but default read remains disabled and fail-closed. No secret values are exposed, cached or persisted. Productization remains `NotReady`; DB/Auth/Portal runtime and production activation remain separate NoGo gates.
