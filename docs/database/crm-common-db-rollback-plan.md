# CRM Common DB Controlled Activation Rollback Plan

P2 is documentation and guardrails only. Rollback is a code/doc revert of this branch or PR.

## Future runtime rollback principles

- Disable the explicit NonProduction Common DB flag.
- Stop CRM connectivity probes.
- Keep CRM productive routes locked.
- Do not drop Portal databases or platform SQL containers.
- Do not remove shared platform data.
- Revert only CRM-owned future schema objects after explicit approval.

## Markers

- CommonDbRollbackPlanPrepared: true.
- CommonDbRuntimeEnabled: false.
- CrossDomainMigrationsPresent: false.
- PortalDatabaseDirectAccessEnabled: false.
