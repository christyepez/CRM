# CRM Portal Consumer Contract Alignment Runbook

## Purpose

Validate Sprint 10 P3 as contract-only alignment between CRM and Portal Sprint 21.

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

## Expected result

- PortalConsumerContractAlignmentReadiness: AlignedContractOnly.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- PortalRuntimeCouplingEnabled: false.
- ProductivePortalNavigationEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.

## Fallback

If any guardrail fails, do not proceed to runtime design. Remove the unsafe artifact or request architecture/security review.
