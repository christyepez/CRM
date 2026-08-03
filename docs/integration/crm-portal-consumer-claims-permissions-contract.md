# CRM Portal Consumer Claims and Permissions Contract

CRM must consume Portal identity and permission decisions. CRM does not create login, Identity, SSO/OIDC production configuration or duplicated permission storage.

## CRM permission keys

- `crm.leads.read`
- `crm.leads.manage`
- `crm.accounts.read`
- `crm.accounts.manage`
- `crm.contacts.read`
- `crm.contacts.manage`
- `crm.opportunities.read`
- `crm.opportunities.manage`
- `crm.activities.read`
- `crm.activities.manage`

## Claims expected from Portal contract

- user identifier.
- tenant identifier.
- display name metadata.
- permission set.
- correlation identifier.

## Markers

- CrmPortalClaimsPermissionsContractPrepared: true.
- PortalAuthDuplicated: false.
- PortalPermissionsDuplicated: false.
- SsoOidcProductionConfigured: false.
- BrowserTokenStorageDetected: false.
