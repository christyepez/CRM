# CRM Portal Consumer Navigation Contract

CRM will provide navigation metadata for Portal to render only after a future explicit activation gate. P3 does not register productive Portal navigation.

## Logical navigation entries

- CRM dashboard.
- Leads.
- Accounts.
- Contacts.
- Opportunities.
- Activities.

## Contract fields

- module key: `crm`.
- route template: relative placeholder only, for example `/crm/<area>`.
- required permission: Portal permission key.
- visibility: controlled by Portal Menu and Permissions.

## Markers

- CrmPortalNavigationContractPrepared: true.
- ProductivePortalNavigationEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- PortalMenuDuplicated: false.
