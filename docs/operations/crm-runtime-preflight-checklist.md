# CRM Runtime Preflight Checklist

## Sprint 9 P1 controlled runtime activation decision

- Confirm docs for Sprint 9 P1 exist.
- Confirm endpoint `/api/crm/foundation/sprint-9/controlled-runtime-activation-decision` returns 200.
- Confirm `RuntimeTrialsEnabledNow=false`.
- Confirm `ProductionActivationDecision=NoGo`.
- Confirm no DB, EF, migrations, secrets, token/header reads, Portal HTTP, productive CRUD, DELETE or productive UI are enabled.

## Sprint 5 P3 common DB probe optional activation checks

- [ ] `/api/crm/foundation/sprint-5/common-db-probe-optional-activation` is registered as GET-only.
- [ ] `CrmCommonDbProbeOptionalActivationStatusService` exists.
- [ ] `CommonDbProbeOptionalActivationPlaceholder` exists.
- [ ] `Common DB probe optional activation only; no database connection is attempted` is present.
- [ ] `commonDbProbeEnabled=false`.
- [ ] `commonDbConnectionAttempted=false`.
- [ ] No database connection, `UseSqlServer`, migrations, connection strings, secret reads, SQL Server compose, productive routes or DELETE endpoints are active.
- [ ] `Sprint5P4PortalAuthProbeOptionalActivationInNonProduction` is the next gate.

## Sprint 5 P2 secret provider runtime contract checks

- [ ] `/api/crm/foundation/sprint-5/secret-provider-runtime-contract` is registered as GET-only.
- [ ] `CrmSecretProviderRuntimeContractStatusService` exists.
- [ ] `SecretProviderRuntimeContractPlaceholder` exists.
- [ ] `Secret Provider contract validation only; no secrets are read` is present.
- [ ] `secretProviderRuntimeConnected=false`.
- [ ] `secretProviderReadsEnabled=false`.
- [ ] `secretReadAttemptedByRuntime=false`.
- [ ] `.env` is absent.
- [ ] No real secrets, Key Vault runtime client, connection strings, DB runtime, Portal Auth runtime, productive routes or DELETE endpoints are active.
- [ ] `Sprint5P3CommonDbProbeOptionalActivationInNonProduction` is the next gate.

## Sprint 5 P1 controlled runtime probe activation plan checks

- [ ] `/api/crm/foundation/sprint-5/runtime-probe-activation-plan` is registered as GET-only.
- [ ] `Runtime probe activation plan only; no runtime activation approved` is present.
- [ ] `runtimeProbeActivationPlanExists=true`.
- [ ] `runtimeProbeActivationApproved=false`.
- [ ] `commonDbProbeActivationApproved=false`.
- [ ] `portalAuthProbeActivationApproved=false`.
- [ ] `productiveRoutesActivationApproved=false`.
- [ ] `realActivationApproved=false`.
- [ ] `deleteStillNoGo=true`.
- [ ] `Sprint5P2SecretProviderRuntimeContractValidation` is the next gate.

## Sprint 4 P6 gate decision checks

- [ ] `/api/crm/foundation/sprint-4/gate-decision` is registered as GET-only.
- [ ] `Sprint 4 gate decision only; no real activation` is present.
- [ ] `OverallDecision=GoForNonProductionFoundationPilot`.
- [ ] `RealActivationDecision=NoGo`.
- [ ] `CommonDbRuntimeDecision=NoGoForRuntimeActivation`.
- [ ] `PortalAuthRuntimeDecision=NoGoForRuntimeActivation`.
- [ ] `NonProductionE2EPilotDecision=GoFoundationOnly`.
- [ ] `Sprint5P1ControlledRuntimeProbeActivationPlan` is the next gate.
- [ ] Productive routes and DELETE remain inactive.

## Sprint 4 P5 non-production E2E pilot checks

- [ ] `/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness` is registered as GET-only.
- [ ] `Non-production E2E pilot readiness only; no real activation` is present.
- [ ] `e2ePilotScope=FoundationOnly`.
- [ ] `productiveRoutesUsed=false`.
- [ ] `realDatabaseUsed=false`.
- [ ] `portalAuthRuntimeUsed=false`.
- [ ] Negative route validation is required.
- [ ] `tools/check-crm-e2e-foundation.ps1` passes when API is running.

## Sprint 4 P4 productive routes locked stub checks

- [ ] `/api/crm/foundation/sprint-4/productive-routes-locked-stub` is registered as GET-only.
- [ ] `Productive routes locked stub validation only; no productive routes are active` is present.
- [ ] `lockedStubsStrategy=DocumentOnlyPreferred`.
- [ ] `productiveRoutesRegistered=false`.
- [ ] `lockedStubsRegistered=false`.
- [ ] No active `/api/crm/leads`, `/api/crm/accounts` or `/api/crm/contacts` routes exist.
- [ ] No DELETE endpoint exists.
- [ ] Foundation CRUD remains under `/api/crm/foundation/...`.

## Sprint 4 P3 Portal Auth runtime probe checks

- [ ] `/api/crm/foundation/sprint-4/portal-auth-runtime-probe` is registered as GET-only.
- [ ] `Portal Auth runtime probe exists but is disabled; no tokens are read and no Portal HTTP calls are attempted` is present in Application and Infrastructure placeholders.
- [ ] `portalAuthRuntimeProbeEnabled=false`.
- [ ] `tokenReadAttemptedByRuntime=false`.
- [ ] `portalHttpAttemptedByRuntime=false`.
- [ ] No login/logout endpoint exists.
- [ ] No Auth middleware or productive authorization is registered.
- [ ] No Portal runtime call or Portal URL exists.

## Sprint 4 P2 common DB runtime probe checks

- [ ] `/api/crm/foundation/sprint-4/common-db-runtime-probe` is registered as GET-only.
- [ ] `Common DB runtime probe exists but is disabled; no database connection is attempted` is present in Application and Infrastructure placeholders.
- [ ] `commonDbRuntimeProbeEnabled=false`.
- [ ] `dbConnectionAttemptedByRuntime=false`.
- [ ] `connectionStringsConfigured=false`.
- [ ] `sqlServerOwnedByCrm=false`.
- [ ] No SQL Server service is defined by CRM Compose.
- [ ] No migration or database folder is introduced.

Before Sprint 4 runtime probes:

- [ ] GitHub `main` is current.
- [ ] Worktree is clean before branch creation.
- [ ] `dotnet restore`, build and tests pass.
- [ ] `docker compose config` passes.
- [ ] Port `8093` is available or intentionally owned by `crm-api`.
- [ ] No SQL Server in CRM Compose.
- [ ] No `.env` committed.
- [ ] No productive `/api/crm/leads`, `/api/crm/accounts`, `/api/crm/contacts`.
- [ ] No DELETE.
- [ ] No Auth runtime, token storage, Portal HTTP or real configuration values.
- [ ] Node PATH issue is documented or bundled Node verifier passes.

Use:

```powershell
powershell.exe -ExecutionPolicy Bypass -File tools\preflight-crm-local.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-guardrails.ps1
powershell.exe -ExecutionPolicy Bypass -File tools\check-crm-health.ps1
```

## Sprint 5 P4 Portal Auth Probe Optional Activation
- Confirm P4 docs exist.
- Confirm endpoint/service/placeholder exist.
- Confirm disabled flags and warning.
- Confirm no Portal HTTP, token/header reads, Auth middleware, login/logout, Identity or token storage.
## Sprint 5 P5 Locked Productive Route Stub Trial

- Confirm P5 docs exist.
- Confirm the foundation endpoint exists.
- Confirm productive routes are not registered by default.
- Confirm negative route checks return 404.
- Confirm no DELETE, DB, Auth, Portal runtime or productive UI was added.
## Sprint 5 P6 Gate Decision

- Confirm Sprint 5 closure docs exist.
- Confirm Sprint 6 roadmap docs exist.
- Confirm gate decision endpoint exists.
- Confirm real activation remains NoGo.
- Confirm productive route negative checks remain 404.

## Sprint 6 P1 NonProduction Runtime Approval Package

- Confirm approval package docs exist.
- Confirm `/api/crm/foundation/sprint-6/nonproduction-runtime-approval-package` is registered as GET-only.
- Confirm `CrmNonProductionRuntimeApprovalPackageStatusService` exists.
- Confirm `nonProductionRuntimeApprovalPackageExists=true`.
- Confirm all runtime approvals remain false.
- Confirm synthetic data, rollback, observability, security review and architecture review remain required.
- Confirm the warning is `NonProduction runtime approval package only; no runtime approval is granted`.
- Confirm next gate is `Sprint6P2SecretProviderSafeMockActivation`.
- Confirm no secrets, DB connection, Portal HTTP, token/header reads, locked stubs runtime, productive routes, DELETE or productive UI were enabled.

## Sprint 6 P2 Secret Provider Safe Mock Activation

- Confirm safe mock docs exist.
- Confirm `/api/crm/foundation/sprint-6/secret-provider-safe-mock-activation` is registered as GET-only.
- Confirm `CrmSecretProviderSafeMockActivationStatusService` exists.
- Confirm `SecretProviderSafeMock` exists and uses deterministic synthetic values only.
- Confirm `secretProviderSafeMockEnabled=true`.
- Confirm `secretProviderReadsRealSecrets=false`.
- Confirm `secretProviderReadsSyntheticValues=true`.
- Confirm `secretProviderReadsEnabledForMockOnly=true`.
- Confirm `.env`, file reads, environment reads, Key Vault clients and Azure secret SDKs are absent.
- Confirm DB/Auth/Portal runtime, productive routes, locked stubs runtime and DELETE remain disabled.
- Confirm next gate is `Sprint6P3CommonDbConnectivityDryRunContract`.

## Sprint 6 P3 Common DB Connectivity Dry-Run Contract

- Confirm dry-run docs exist.
- Confirm `/api/crm/foundation/sprint-6/common-db-connectivity-dry-run` is registered as GET-only.
- Confirm `CrmCommonDbConnectivityDryRunStatusService` exists.
- Confirm `CommonDbConnectivityDryRun` placeholder exists.
- Confirm `commonDbDryRunEnabled=false`.
- Confirm `commonDbConnectionAttempted=false`.
- Confirm `syntheticConnectionReference=mock://crm/common-db`.
- Confirm `realConnectionStringUsed=false`.
- Confirm `connectionStringResolved=false`.
- Confirm no real DB connection, EF runtime, migrations, SQL Server compose, secrets/env reads, Portal Auth runtime, productive routes or DELETE are enabled.
- Confirm next gate is `Sprint6P4PortalAuthTokenPropagationDryRunContract`.
## Sprint 6 P4 Portal Auth token propagation dry-run

- Confirm P4 documentation exists.
- Confirm `CrmPortalAuthTokenPropagationDryRunStatusService` exists.
- Confirm `PortalAuthTokenPropagationDryRun` placeholder exists.
- Confirm endpoint `/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run` returns 200.
- Confirm tokenReadAttempted=false, headerReadAttempted=false and portalHttpAttempted=false.
- Confirm no Auth middleware, no token/header reads, no Portal HTTP and no productive routes.
## Sprint 6 P5 locked stub runtime registration trial

- Confirm P5 documentation exists.
- Confirm `CrmLockedStubRuntimeRegistrationTrialStatusService` exists.
- Confirm endpoint `/api/crm/foundation/sprint-6/locked-stub-runtime-registration-trial` returns 200.
- Confirm lockedStubRuntimeRegistrationEnabled=false and lockedStubsRegisteredAtRuntime=false.
- Confirm productiveRoutesRegistered=false and default negative routes return 404.
- Confirm no DELETE, DB, Auth, Portal, token/header reads, domain services or store usage from stubs.
# Sprint 7 P1 secret provider approval preflight addendum

- Validate `GET /api/crm/foundation/sprint-7/secret-provider-real-nonproduction-approval`.
- Confirm approval package exists but approvalGranted=false.
- Confirm runtimeEnabled=false, runtimeConnected=false and realSecretReadAttempted=false.
- Confirm no `.env`, secret value, environment secret read, DB runtime, Portal Auth runtime, productive route, locked stub runtime or DELETE endpoint exists.

# Sprint 6 P6 gate decision preflight addendum

- Validate `GET /api/crm/foundation/sprint-6/gate-decision`.
- Confirm `Sprint6GateDecision`, `GoForSprint7ControlledNonProductionActivationPlanning`, `RealActivationDecision=NoGo`, `ProductizationStatus=NotReady` and `Sprint7PlanningDecision=Go`.
- Confirm productive `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` remain 404.
- Confirm no `.env`, real secret provider, DB runtime, Portal Auth runtime, DELETE endpoint or productive UI activation exists.

# Sprint 7 P2 secret provider runtime probe preflight addendum

- Validate `GET /api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe`.
- Confirm runtime probe exists but approvalGranted=false.
- Confirm probeEnabled=false, probeAttempted=false, runtimeConnected=false and probeSkippedBecauseApprovalNotGranted=true.
- Confirm no real secret read, no value materialization, no value logs, no API value return, no Key Vault runtime call, no Azure secret SDK runtime call, no `.env`, no DB runtime, no Portal Auth runtime, no productive route, no locked stub runtime or DELETE endpoint exists.

# Sprint 7 P3 common DB real connectivity preflight addendum

- Validate `GET /api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe`.
- Confirm commonDbRealConnectivityApprovalGranted=false and secretProviderRealNonProductionApprovalGranted=false.
- Confirm connectionStringResolved=false, commonDbProbeEnabled=false, commonDbProbeAttempted=false, commonDbConnected=false.
- Confirm `mock://crm/common-db` is the only connection reference.
- Confirm no real connection strings, DB connection, EF runtime, migrations, SQL Server compose, Auth runtime, Portal runtime, productive routes, locked stub runtime or DELETE endpoint exists.

# Sprint 7 P4 Portal Auth real runtime preflight addendum

- Validate `GET /api/crm/foundation/sprint-7/portal-auth-real-runtime-probe`.
- Confirm portalAuthRealRuntimeApprovalGranted=false and secretProviderRealNonProductionApprovalGranted=false.
- Confirm portalAuthRealRuntimeProbeEnabled=false, portalAuthRealRuntimeProbeAttempted=false, portalAuthRuntimeConnected=false and portalHttpCallAttempted=false.
- Confirm tokenReadAttempted=false, headerReadAttempted=false and authorizationHeaderReadAttempted=false.
- Confirm `mock://crm/portal-auth` and `mock://crm/portal-user` are the only references.
- Confirm no Portal Auth base URL resolution, Portal HTTP, token/header reads, Auth middleware, `[Authorize]`, login/logout, CRM Identity, persisted roles/permissions, DB runtime, productive routes, locked stub runtime or DELETE endpoint exists.
## Sprint 7 P5 locked productive route runtime registration

- Confirm docs, contracts, service and API registrar exist.
- Confirm `Crm:ProductiveRoutes:LockedRegistrationEnabled` is false by default.
- Confirm `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` return 404 by default.
- Confirm explicit NonProduction fixture covers 423 for GET/POST/PUT/PATCH and no DELETE.
- Confirm no DB, EF, Portal Auth runtime, token/header reads, secrets, productive UI or side effects.
## Sprint 7 P6 gate decision

- Confirm Sprint 7 closure docs and gate matrix exist.
- Confirm Sprint 8 roadmap docs exist.
- Confirm `/api/crm/foundation/sprint-7/gate-decision` is GET-only.
- Confirm real activation remains NoGo and productization remains NotReady.
- Confirm default productive routes remain 404 and locked route 423 remains explicit NonProduction-only.
## Sprint 8 P1 secret provider approval decision

- Confirm Sprint 8 P1 docs exist.
- Confirm endpoint `/api/crm/foundation/sprint-8/secret-provider-approval-decision` is GET-only.
- Confirm approved for next sprint is true and real read enabled now is false.
- Confirm no `.env`, no real secret reads, no runtime client, no secret values in logs/API/repo.
- Confirm DB, Portal Auth, productive routes, CRUD, DELETE and productive UI remain disabled.

## Sprint 8 P2 secret provider controlled real NonProduction read

- Confirm Sprint 8 P2 docs exist.
- Confirm endpoint `/api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read` exists.
- Confirm probe endpoint is locked by default.
- Confirm `CrmSecretProviderControlledRealReadStatusService` exists.
- Confirm `ISecretProviderRuntime`, `DisabledSecretProviderRuntime` and `ControlledNonProductionSecretProviderRuntime` exist.
- Confirm enabled=false, attempted=false, no value returned/logged/persisted/cached.
- Confirm no `.env`, no real secret values, no appsettings secrets, no DB/Auth/Portal runtime, no SQL Server, no DELETE and no productive UI.

## Sprint 8 P3 common DB controlled real connectivity

- Confirm Sprint 8 P3 docs exist.
- Confirm endpoint `/api/crm/foundation/sprint-8/common-db-controlled-real-connectivity` exists.
- Confirm probe endpoint is locked by default.
- Confirm `CrmCommonDbControlledRealConnectivityStatusService` exists.
- Confirm no connection strings are returned, logged, persisted or cached.

## Sprint 8 P4 Portal Auth controlled real runtime validation

- Confirm Sprint 8 P4 docs exist.
- Confirm endpoint `/api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation` exists.
- Confirm probe endpoint is locked by default.
- Confirm `CrmPortalAuthControlledRealRuntimeValidationStatusService` exists.
- Confirm no Portal URLs, secrets, tokens or request headers are returned, logged, persisted or cached.
- Confirm no login/logout, Identity, auth middleware, `[Authorize]`, roles/permissions persistence, productive CRUD or DELETE is enabled.
- Confirm `ICommonDbConnectivityProbe`, `DisabledCommonDbConnectivityProbe` and `ControlledNonProductionCommonDbConnectivityProbe` exist.

## Sprint 8 P5 Locked route authorization policy integration

- Confirm Sprint 8 P5 docs exist.
- Confirm endpoint `/api/crm/foundation/sprint-8/locked-route-authorization-policy-integration` exists.
- Confirm `CrmLockedRouteAuthorizationPolicyIntegrationStatusService` exists.
- Confirm `CrmLockedRouteAuthorizationPolicyEvaluator` exists and is pure application logic.
- Confirm productive routes return 404 by default.
- Confirm explicit NonProduction locked routes return 423.
- Confirm policy metadata is returned only with explicit policy flag.
- Confirm DELETE, CRUD, DB runtime, EF runtime, migrations, Portal HTTP, token/header reads, auth middleware, `[Authorize]`, product UI and secrets remain disabled.

## Sprint 8 P6 Sprint 8 gate decision

- Confirm Sprint 8 closure docs and gate matrix exist.
- Confirm Sprint 9 roadmap docs exist.
- Confirm endpoint `/api/crm/foundation/sprint-8/gate-decision` exists.
- Confirm `CrmSprint8GateDecisionStatusService` exists.
- Confirm production activation remains `NoGo`.
- Confirm productization remains `NotReady`.
- Confirm productive routes return 404 by default.
- Confirm locked routes remain 423 only under explicit NonProduction flags.
- Confirm no DELETE, DB runtime, EF runtime, migrations, Portal HTTP, token/header reads, auth middleware, `[Authorize]`, productive CRUD or product UI is active.
- Confirm enabled=false, attempted=false, connected=false and no connection string returned/logged/persisted/cached.
- Confirm no `.env`, no real connection strings, no SQL Server compose, no EF runtime, no migrations, no schema changes, no DB/Auth/Portal runtime, no DELETE and no productive UI.
