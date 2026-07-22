# CRM Portal Authorization Simulation

Sprint 2 P3 adds `FoundationSimulation` for future Portal authorization integration.

CRM does not own authentication, users, tenants, roles, permissions or SSO. Ownership remains in PortalCorporativo.

The simulation provides fictitious scenarios:

- `CrmAdminCanPreviewFoundationData`
- `CrmSalesCanPreviewAssignedLeads`
- `CrmReadOnlyCanViewReadiness`
- `CrmUnauthorizedCannotUseFoundationMutation`

Foundation permissions:

- `crm.foundation.read`
- `crm.foundation.preview.write`
- `crm.foundation.preview.clear`
- `crm.readiness.read`

This is not productive security. It uses no real Portal runtime, no external calls, no credentials, no token storage and no persisted roles.

Next gate: `Sprint2P4ControlledCrudBehindFoundationFlag`.

P4 consumes this simulation for foundation CRUD permission checks only. It still does not activate real Portal runtime authorization.
