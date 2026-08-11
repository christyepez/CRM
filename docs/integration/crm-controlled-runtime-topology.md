# CRM Controlled Runtime Topology

The future pilot topology keeps CRM and Portal as separate bounded systems. CRM consumes Portal capabilities through approved contracts and does not host Portal services inside CRM compose.

## Future components

- CRM API exposes CRM-owned endpoints only after an explicit pilot scaffold.
- Portal Gateway remains Portal-owned.
- Portal Auth, Menu, Permissions, Audit, Configuration and Notification remain Portal-owned.
- Common SQL Server remains environment-owned; CRM uses a CRM logical database only after a future gate.

## Runtime status

- ControlledRuntimeTopologyPrepared: true.
- RuntimePortalCouplingEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- PortalServicesInCrmCompose: false.
- CommonDbRuntimeEnabled: false.
