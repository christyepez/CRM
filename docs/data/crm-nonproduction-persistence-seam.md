# CRM Non-Production Persistence Seam

CRM Sprint 2 P2 activates a controlled `NonProductionSeam` for foundation previews only.

This seam is:

- Application ports for Lead, Account and Contact foundation stores.
- Infrastructure in-memory adapters.
- Non-durable and replaceable.
- Safe for local smoke and contract checks.

This seam is not:

- Productive CRUD.
- Database activation.
- EF Core, DbContext, DbSet or migrations.
- SQL Server ownership by CRM.
- Runtime Portal, Financiero or BI integration.

Runtime flags remain safe: `CRM_FOUNDATION_MODE=true`, `CRM_PERSISTENCE_SEAM_ENABLED=true`, `CRM_PERSISTENCE_ENABLED=false`, `CRM_PRODUCTIVE_CRUD_ENABLED=false`, `CRM_DURABLE_PERSISTENCE_ENABLED=false`.

Next gate: `Sprint2P3PortalAuthorizationAdapterSimulation`.

P4 uses the same in-memory seam for controlled foundation CRUD previews. Durable persistence remains disabled.
