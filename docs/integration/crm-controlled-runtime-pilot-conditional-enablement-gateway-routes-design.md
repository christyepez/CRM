# CRM Controlled Runtime Pilot Conditional Enablement Gateway Routes Design

## Design

Gateway route enablement remains conceptual in P10. No productive route is registered and no Portal gateway configuration is changed.

## Future route rules

- Routes must be enabled only in NonProduction.
- Routes must remain locked until approval and smoke evidence are complete.
- Route ownership remains with Portal Gateway governance.
- CRM must not duplicate Portal Gateway capabilities.

## Markers

- ConditionalEnablementGatewayRoutesDesignPrepared: true.
- ProductivePortalGatewayRoutesEnabled: false.
- PortalServicesInCrmCompose: false.
- PortalConfigurationDuplicated: false.
