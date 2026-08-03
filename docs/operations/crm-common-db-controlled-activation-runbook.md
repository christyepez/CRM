# CRM Common DB Controlled Activation Runbook

## Purpose

Validate that Sprint 10 P2 prepared a Common DB activation plan without enabling runtime.

## Commands

1. `git diff --check`
2. `dotnet build`
3. `dotnet test`
4. `docker compose --env-file .env.example config`
5. `npm run lint`, `npm run test`, `npm run build` when frontend tooling is available
6. `powershell -File tools/check-crm-guardrails.ps1`
7. `powershell -File tools/verify-crm-foundation.ps1`
8. `powershell -File tools/check-crm-common-db-controlled-activation-guardrails.ps1`
9. `powershell -File tools/verify-crm-common-db-controlled-activation-plan.ps1`

## Expected result

- CommonDbControlledActivationReadiness: PlanPreparedContractOnly.
- ProductionActivationDecision: NoGo.
- CommonDbRuntimeEnabled: false.
- RealCommonDbConnectionConfigured: false.
- PortalDatabaseDirectAccessEnabled: false.

## Fallback

If a guardrail fails, do not activate runtime. Remove the unsafe change or stop and request architecture review.
