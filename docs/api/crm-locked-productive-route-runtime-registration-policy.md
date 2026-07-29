# CRM Locked Productive Route Runtime Registration Policy

P5 allows only side-effect-free route registration for NonProduction validation.

Rules:

- `Crm:ProductiveRoutes:LockedRegistrationEnabled=false` by default.
- Productive routes are not registered by default.
- When explicitly enabled in NonProduction, locked route stubs return `423`.
- `423` means the route is known but intentionally unavailable for productive execution.
- `423` must not call domain services, foundation stores, DB, EF, Portal Auth runtime or token/header readers.
- Production fails closed by not registering the locked routes.
- No `DELETE` routes are permitted.

Future transition from `423` to productive runtime requires Secret Provider, Common DB, Portal Auth, authorization policy, persistence strategy, observability, rollback, security sign-off and QA sign-off.
