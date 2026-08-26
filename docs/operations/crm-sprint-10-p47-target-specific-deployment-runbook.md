# CRM Sprint 10 P47 - Target-Specific Deployment Runbook

TargetSpecificDeploymentRunbookExists: true
ProductionExecutionAllowed: false
P45RetryAuthorized: false

This runbook is not executable until P47R resolves the production target and rollback baseline.

## Preflight

1. Confirm `origin/main` contains the approved retry commit.
2. Confirm candidate image id and digest match the frozen packet.
3. Confirm target manifest hash.
4. Confirm rollback baseline hash.
5. Confirm new explicit human approval for packet V4 or later.
6. Confirm production monitoring target is available.

## Target validation

- Verify deployment platform.
- Verify Docker context or controlled executor.
- Verify service name and runtime identifier.
- Verify routing/DNS/load-balancer target.
- Verify configuration source and secret provider metadata.

## Deployment

No deployment command is authorized by P47.

## Abort

Abort before production if any manifest hash, image identity, target, rollback, monitoring, or approval evidence differs.

## Rollback

Rollback must use the frozen rollback baseline generated after external target resolution.

## Post-rollback validation

- Health/readiness reflect the expected baseline.
- Traffic state matches rollback target.
- No CRM production data changes occurred.
- Logs show no secrets or tokens.

