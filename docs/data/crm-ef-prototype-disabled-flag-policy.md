# CRM EF Prototype Disabled Flag Policy

Policy: EF prototype code may exist only behind disabled flags.

Required disabled flags:
- `CRM_EF_PROTOTYPE_EXISTS=true`.
- `CRM_EF_RUNTIME_ENABLED=false`.
- `CRM_DBCONTEXT_RUNTIME_ACTIVE=false`.
- `CRM_MIGRATIONS_ENABLED=false`.
- `CRM_DURABLE_PERSISTENCE_ENABLED=false`.
- `CRM_PRODUCTIVE_CRUD_ENABLED=false`.

The prototype must not be registered in dependency injection as a runtime persistence component. Foundation in-memory stores remain the active non-production persistence seam.

Activation requires a separate sprint, explicit approval and a new pull request.
