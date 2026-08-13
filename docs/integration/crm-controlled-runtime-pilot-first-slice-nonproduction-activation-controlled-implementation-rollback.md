# CRM Controlled NonProduction Activation Implementation Rollback

Rollback posture:

- P24 introduces no runtime activation.
- Disable or remove the P24 endpoint registration if the scaffold must be reverted.
- Keep P21 scaffold endpoint intact.
- No data rollback is required because no DB, table, migration or external call is added.

Marker: FirstSliceNonProductionActivationControlledImplementationRollbackPrepared: true.
