# CRM Explicit Approval DevOps and Rollback Checklist

- Rollback owner must be assigned before P27.
- Activation plan must identify feature flags to keep false by default.
- Compose must remain free of Portal services and CRM-owned SQL Server.
- A failed dry-run must be reversible by disabling the future P27 flag.

- FirstSliceNonProductionActivationExplicitApprovalDevOpsRollbackChecklistPrepared: true
- PortalServicesInCrmCompose: false
