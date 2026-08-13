# CRM Controlled Runtime Pilot First Slice NonProduction Activation Environment Separation

## Separation

The future activation must be NonProduction-only. Production activation remains blocked. CRM must not add Portal services or CRM-owned SQL Server containers to its compose file.

## Markers

- FirstSliceNonProductionActivationEnvironmentSeparationPrepared: true.
- PortalServicesInCrmCompose: false.
- CommonDbRuntimeEnabled: false.
