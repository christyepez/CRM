# CRM Foundation CRUD Non-Production Policy

Foundation CRUD is preview-only and non-durable.

Rules:

- Uses in-memory FoundationStore adapters.
- No database.
- No EF Core.
- No migrations.
- No SQL Server.
- No connection strings.
- No real personal data.
- No productive CRM CRUD.

The feature is enabled only as `Foundation CRUD: Enabled` while `ProductiveCrudEnabled=false` and `DurablePersistence=false`.

Next gate: `Sprint2P5IntegrationReadinessReview`.
