# CRM Common DB Probe Optional Activation Policy

The Common DB probe may be defined before activation, but it remains off by default.

Rules:

- No database connection in Sprint 5 P3.
- No real connection string in repository files.
- No EF runtime activation.
- No migrations.
- No CRM-owned SQL Server container.
- No API dependency on database availability.
- Future activation is non-production only.
- Synthetic data is mandatory before any future probe execution.
- Rollback, observability and negative route checks are mandatory.
- Secret Provider runtime must be approved before any future connection.
