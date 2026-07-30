# CRM Portal Auth Runtime Validation Trial Contract

The public contract exposes metadata flags only.

Required default values:
- `PortalAuthRuntimeValidationTrialEnabled=false`
- `PortalAuthValidationAttempted=false`
- `PortalAuthValidated=false`
- `PortalHttpAttempted=false`
- `PortalAuthUrlReturnedToApi=false`
- `PortalClientSecretReturnedToApi=false`
- `AuthHeaderRead=false`
- `TokenRead=false`
- `TokenStored=false`
- `ProductiveAuthEnabled=false`
- `IdentityRuntimeEnabled=false`
- `AuthAttributeEnabled=false`

Probe input uses approved logical names only:
- `crm-portal-auth-base-url`
- `crm-portal-auth-client-id`
- `crm-portal-auth-client-secret`

No real URLs, client secrets, tokens or claims may be returned.
