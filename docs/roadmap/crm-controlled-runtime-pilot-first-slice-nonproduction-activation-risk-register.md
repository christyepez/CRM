# CRM Controlled Runtime Pilot First Slice NonProduction Activation Risk Register

| Risk | Impact | Mitigation |
| --- | --- | --- |
| Activation plan mistaken for activation | Runtime could be enabled early | Keep `NonProductionActivationExecuted: false` and P17 as dry run |
| Unsafe environment values | Private URLs or secrets could leak | Use logical placeholders only and require secret provider review later |
| Rollback gap | Future activation may not be reversible | Require rollback rehearsal before any real activation |

## Markers

- FirstSliceNonProductionActivationEvidencePlanPrepared: true.
- FirstSliceNonProductionActivationRollbackPrepared: true.
