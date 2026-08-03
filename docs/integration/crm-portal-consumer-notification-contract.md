# CRM Portal Consumer Notification Contract

CRM will request notifications through Portal Notification after a future explicit activation gate. CRM does not create a separate notification provider.

## Candidate notification intents

- Lead assignment.
- Opportunity stage change.
- Follow-up reminder.
- Account ownership change.
- Integration failure notification for controlled NonProduction pilots.

## Minimum fields

- notification type.
- recipient reference.
- tenant identifier.
- correlation identifier.
- template key.
- sanitized payload metadata.

## Markers

- CrmPortalNotificationContractPrepared: true.
- PortalNotificationDuplicated: false.
- RealNotificationProviderConfigured: false.
- PortalRuntimeCouplingEnabled: false.
