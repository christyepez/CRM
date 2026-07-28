# CRM Common DB Real Connectivity NonProduction Probe Safety Boundary

Allowed:
- probe metadata;
- boolean gate status;
- synthetic reference `mock://crm/common-db`;
- warning and next gate.

Forbidden:
- real connection strings;
- connection string logs;
- database connections;
- EF runtime activation;
- migrations or schema changes;
- SQL Server compose ownership by CRM;
- productive CRM routes.

P3 does not change data and does not require data rollback.
