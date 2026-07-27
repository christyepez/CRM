# CRM E2E Pilot Safety Boundary

The P5 pilot is foundation-only.

Allowed:

- Health/readiness checks.
- Sprint 3/4 foundation status endpoints.
- Negative checks for productive route absence.
- Synthetic data only.

Not allowed:

- Productive routes.
- DELETE operations.
- Real DB or durable persistence.
- Auth runtime, token reads or Portal HTTP.
- Login/logout, Identity or token storage.
- Real customer data.
