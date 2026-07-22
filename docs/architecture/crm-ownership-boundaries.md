# CRM Ownership Boundaries

CRM owns customer relationship domain concepts, foundation API contracts and non-production readiness metadata.

PortalCorporativo owns authentication, authorization, permissions, menu, audit, notification, configuration, gateway and shared correlation context.

Financiero owns customer financial references, account status, invoices, payment signals, collections signals and tax/SRI processes.

Reporting/BI activation depends on Portal authorization and a governed BI platform. CRM owns only metadata contracts until approved.

No module may share database tables or bypass an approved adapter contract.
## Sprint 2 P3 authorization boundary

Auth, SSO, users, tenants and permissions are owned by PortalCorporativo. CRM P3 only simulates the future adapter contract with `FoundationSimulation`; it does not own Auth and does not persist roles or permissions.
