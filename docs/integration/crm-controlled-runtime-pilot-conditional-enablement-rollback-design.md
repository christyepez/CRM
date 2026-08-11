# CRM Controlled Runtime Pilot Conditional Enablement Rollback Design

## Rollback principle

Rollback must be flag-first and configuration-first. Because P10 does not activate runtime, the current rollback is to keep all pilot flags disabled.

## Future rollback sequence

1. Disable the master pilot flag.
2. Disable Portal client, route, navigation, Common DB and smoke flags.
3. Confirm no productive route or navigation remains exposed.
4. Confirm no external calls are made.
5. Preserve sanitized evidence.

## Markers

- ConditionalEnablementRollbackDesignPrepared: true.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- ProductivePortalNavigationEnabled: false.
