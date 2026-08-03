# CRM Portal Consumer Audit Contract

CRM will publish audit events to Portal Audit through a future adapter. P3 does not create local audit persistence or runtime publication.

## Event families

- Lead lifecycle events.
- Account lifecycle events.
- Contact lifecycle events.
- Opportunity lifecycle events.
- Activity lifecycle events.
- Runtime integration probe events for future NonProduction pilots.

## Minimum fields

- event name.
- aggregate type.
- aggregate identifier.
- tenant identifier.
- actor identifier.
- correlation identifier.
- occurred at UTC.
- sanitized metadata.

## Markers

- CrmPortalAuditContractPrepared: true.
- PortalAuditDuplicated: false.
- PortalRuntimeCouplingEnabled: false.
