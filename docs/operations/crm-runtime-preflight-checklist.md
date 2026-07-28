# CRM Runtime Preflight Checklist

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
