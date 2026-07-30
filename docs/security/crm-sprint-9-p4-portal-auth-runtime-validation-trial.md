# CRM Sprint 9 P4 - Portal Auth Runtime Validation Trial

Status: Implemented as a controlled NonProduction trial.

The trial prepares Portal Auth validation without enabling CRM-owned authentication. It is disabled and fail-closed by default through `Crm:RuntimeTrials:PortalAuthValidationEnabled=false`.

Default evidence:
- PortalAuthRuntimeValidationTrialExists: true.
- PortalAuthRuntimeValidationTrialApproved: true.
- PortalAuthRuntimeValidationTrialEnabled: false.
- PortalAuthValidationAttempted: false.
- PortalAuthValidated: false.
- PortalHttpAttempted: false.
- PortalHttpConfigured: false.
- PortalAuthUrlResolved: false.
- PortalAuthUrlReturnedToApi: false.
- PortalClientSecretResolved: false.
- PortalClientSecretReturnedToApi: false.
- AuthHeaderRead: false.
- TokenRead: false.
- TokenStored: false.
- ClaimsMapped: false.
- ProductiveAuthEnabled: false.
- LoginEndpointCreated: false.
- LogoutEndpointCreated: false.
- IdentityRuntimeEnabled: false.
- AuthAttributeEnabled: false.
- SecretProviderMetadataDependencyValidated: true.
- CommonDbMetadataDependencyValidated: true.
- NonProductionOnly: true.
- ProductionBlocked: true.
- FailClosedByDefault: true.
- ObservabilityMetadataOnly: true.
- NextGate: Sprint9P5ProductiveRouteDryRunTrial.

Endpoints:
- `GET /api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial`
- `POST /api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial/probe`

The probe returns `423 Locked` by default and returns sanitized metadata only.
