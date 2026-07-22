# CRM Foundation Permission Simulation

`CrmFoundationPermissionGuard` is a foundation-only guard for checking fictitious Portal permissions during preview flows.

It is not productive authorization. It does not return productive 401/403 responses, does not use framework auth policies and does not store credentials.

The clear-preview endpoint uses the simulated permission `crm.foundation.preview.clear` and returns the permission simulation result in the payload.

Warning returned by APIs: `Portal authorization simulation only; no real Portal runtime configured`.
