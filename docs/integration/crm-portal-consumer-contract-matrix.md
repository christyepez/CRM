# CRM Portal Consumer Contract Matrix

| Portal capability | CRM consumer need | CRM contract status | Runtime status |
| --- | --- | --- | --- |
| Auth | Validate future user context and tenant context | Contract prepared | Disabled |
| Permissions | Map CRM resources to Portal permissions | Contract prepared | Disabled |
| Menu | Register CRM navigation metadata | Contract prepared | Disabled |
| Audit | Publish CRM audit events to Portal Audit | Contract prepared | Disabled |
| Notification | Publish CRM notification requests to Portal Notification | Contract prepared | Disabled |
| Configuration | Resolve CRM feature/config metadata from Portal | Contract prepared | Disabled |
| Health/Observability | Expose safe CRM readiness for future pilot | Contract prepared | Metadata only |

## Decision markers

- CrmPortalConsumerContractMatrixPrepared: true.
- PortalRuntimeCouplingEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- PortalServicesInCrmCompose: false.
