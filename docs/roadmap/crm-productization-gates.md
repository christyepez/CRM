# CRM Productization Gates

Productization remains `NotReady` until:

- Persistence design is approved.
- Portal authorization integration is approved.
- API versioning and security are approved.
- UI shell avoids local login/token storage.
- Integration contracts are stable.
- Testing strategy includes regression, architecture and security checks.
- Docker build is validated outside `BLOCKED_EXTERNAL_REGISTRY` conditions.

No production activation may bypass these gates.
