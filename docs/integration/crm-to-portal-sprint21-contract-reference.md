# CRM to Portal Sprint 21 Contract Reference

This file records CRM expectations for Portal Sprint 21 without enabling runtime coupling.

## Expected Portal capabilities

- Security and permission contract for CRM resources.
- Menu/navigation registration contract for CRM modules.
- Audit append-only publication contract.
- Notification publication contract.
- Configuration contract for CRM feature flags.
- Consumer onboarding and deployment boundary.

## CRM constraints

- PortalSprint21ContractReferencePrepared: true.
- PortalSprint21ContractAlignmentReviewed: true.
- PortalRuntimeCouplingEnabled: false.
- ProductivePortalNavigationEnabled: false.
- PortalAuthDuplicated: false.
- PortalMenuDuplicated: false.
- PortalPermissionsDuplicated: false.
- PortalAuditDuplicated: false.
- PortalNotificationDuplicated: false.
- PortalConfigurationDuplicated: false.

## Next gate

`CrmSprint10P3PortalConsumerContractAlignment` must validate the final Portal Sprint 21 contract before any CRM runtime activation.
