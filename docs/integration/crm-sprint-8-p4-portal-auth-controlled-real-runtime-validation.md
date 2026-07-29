# CRM Sprint 8 P4 - Portal Auth Controlled Real Runtime Validation

Sprint 8 P4 prepares a controlled NonProduction-only validation seam for Portal Auth availability. It is disabled by default and fail-closed.

The package does not implement CRM login, logout, Identity, JWT/cookie auth, productive authorization middleware, `[Authorize]`, token storage, request header reads, Portal HTTP by default, productive CRUD or DELETE routes.

Approved logical secret names are `crm-portal-auth-base-url`, `crm-portal-auth-client-id` and `crm-portal-auth-client-secret`. Secret Provider metadata may be used only as sanitized availability metadata; values, tokens and private Portal URLs must never be returned, logged, persisted or cached.

Default endpoint:

- `GET /api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation`

Optional probe endpoint:

- `POST /api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation/probe`

The probe returns `423 Locked` by default. The next gate is `Sprint8P5LockedRouteAuthorizationPolicyIntegration`.
