# CRM Controlled Runtime Pilot Conditional Implementation Rollback

## Future rollback plan

Rollback must disable the master pilot flag first, then disable client, gateway, navigation, Common DB and smoke flags. No data rollback is expected because the future pilot must remain non-destructive.

## Markers

- ConditionalImplementationRollbackPrepared: true.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- ProductivePortalNavigationEnabled: false.
