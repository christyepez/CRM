# CRM Controlled Runtime Auth, Claims and Permissions Boundary

Portal owns Auth, claims and permissions. CRM consumes future approved claims and permission decisions without creating Identity or SSO/OIDC production configuration.

## Future claims boundary

- user identifier.
- tenant identifier.
- permission set.
- correlation identifier.
- display metadata.

## Rules

- No CRM login/logout.
- No duplicated Portal Auth.
- No token storage in browser.
- No production SSO/OIDC configuration.

## Markers

- AuthClaimsPermissionsBoundaryPrepared: true.
- PortalAuthDuplicated: false.
- PortalPermissionsDuplicated: false.
- SsoOidcProductionConfigured: false.
- BrowserTokenStorageDetected: false.
