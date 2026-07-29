# CRM Portal Auth Controlled Runtime Validation Architecture

P4 adds a foundation-only seam:

- Application status contracts and service.
- Infrastructure `IPortalAuthRuntimeValidationProbe`.
- Default `DisabledPortalAuthRuntimeValidationProbe`.
- Controlled NonProduction placeholder probe.
- In-memory test probe.
- GET status endpoint and POST probe endpoint under `/api/crm/foundation/sprint-8`.

Architecture boundaries:

- PortalCorporativo owns Auth/Security.
- CRM consumes only sanitized readiness metadata.
- CRM does not implement Identity, login, logout, role persistence or permission persistence.
- CRM does not activate auth middleware or productive routes.
- Database runtime and EF remain disabled.

Next gate: `Sprint8P5LockedRouteAuthorizationPolicyIntegration`.
