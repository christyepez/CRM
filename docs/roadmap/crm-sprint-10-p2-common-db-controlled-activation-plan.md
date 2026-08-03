# CRM Sprint 10 P2 - Common DB Controlled Activation Plan

Status: prepared contract only.

This package prepares the Common DB activation path for CRM while preserving the Sprint 10 P1 decision: production remains NoGo and CRM is not production ready. The plan is aligned to the future Portal Sprint 21 consumer/runtime contract and does not activate a real database connection.

## Decision

- CrmSprint10P2CommonDbControlledActivationPlanExists: true.
- CrmBaseFrozenReviewed: true.
- PortalSprint21ContractAlignmentReviewed: true.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- CrmProductionReady: false.
- CommonDbControlledActivationPlanAttempted: true.
- CommonDbControlledActivationReadiness: PlanPreparedContractOnly.
- NextGate: CrmSprint10P3PortalConsumerContractAlignment.

## Scope

- Prepare controlled NonProduction DB activation prerequisites.
- Define CRM-owned logical database boundaries.
- Define no-go checks for shared Portal table access, cross-domain migrations and direct Portal DB reads.
- Keep runtime DB activation disabled.
- Keep all connection material as logical placeholders only.

## Out of scope

Production activation, runtime DB connections, connection string materialization, EF runtime, schema creation, migrations, writes, shared tables with Portal, direct Portal database access, SQL Server ownership by CRM and real data.
