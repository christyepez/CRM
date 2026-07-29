# CRM Portal Auth Controlled Runtime Validation Contract

Status: `PortalAuthControlledRealRuntimeValidation`.

Default contract values:

- `portalAuthControlledRealRuntimeValidationExists=true`
- `portalAuthControlledRealRuntimeValidationApproved=true`
- `portalAuthControlledRealRuntimeValidationEnabled=false`
- `portalAuthRuntimeValidationAttempted=false`
- `portalAuthRuntimeConnected=false`
- `secretProviderAvailabilityMetadataUsed=true`
- `portalAuthBaseUrlResolved=false`
- `portalAuthBaseUrlMaterializedInPublicContract=false`
- `portalAuthBaseUrlLogged=false`
- `portalAuthBaseUrlReturnedToApi=false`
- `portalHttpClientCreated=false`
- `portalHttpCallAttempted=false`
- `tokenReadAttempted=false`
- `headerReadAttempted=false`
- `authorizationHeaderReadAttempted=false`
- `realTokenMaterialized=false`
- `realTokenLogged=false`
- `tokenReturnedToApi=false`
- `productiveAuthorizationEnabled=false`
- `apiRequiresPortalAuth=false`
- `nonProductionOnly=true`
- `failClosedByDefault=true`

Probe responses are sanitized metadata only and never include URL, token or secret values.
