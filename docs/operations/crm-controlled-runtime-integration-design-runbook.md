# CRM Controlled Runtime Integration Design Runbook

## Purpose

Validate Sprint 10 P4 as a design-only gate for future controlled NonProduction runtime integration.

## Commands

1. `git diff --check`
2. `dotnet build CRM.sln`
3. `dotnet test CRM.sln`
4. `docker compose --env-file .env.example config`
5. `npm run test`
6. `npm run build`
7. `npm run lint` only if the script exists
8. `powershell -File tools/check-crm-guardrails.ps1`
9. `powershell -File tools/verify-crm-foundation.ps1`
10. `powershell -File tools/check-crm-common-db-controlled-activation-guardrails.ps1`
11. `powershell -File tools/verify-crm-common-db-controlled-activation-plan.ps1`
12. `powershell -File tools/check-crm-portal-consumer-contract-alignment-guardrails.ps1`
13. `powershell -File tools/verify-crm-portal-consumer-contract-alignment.ps1`
14. `powershell -File tools/check-crm-controlled-runtime-integration-design-guardrails.ps1`
15. `powershell -File tools/verify-crm-controlled-runtime-integration-design.ps1`

## Expected result

- ControlledRuntimeIntegrationDesignReadiness: DesignedContractOnly.
- ProductionActivationDecision: NoGo.
- RuntimePortalCouplingEnabled: false.
- CommonDbRuntimeEnabled: false.
- NextGate: CrmSprint10P5ControlledRuntimePilotScaffold.
