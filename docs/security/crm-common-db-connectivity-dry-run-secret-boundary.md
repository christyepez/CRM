# CRM Common DB Connectivity Dry-Run Secret Boundary

Sprint 6 P3 may only use Secret Provider safe mock metadata.

The only permitted reference is:

- `mock://crm/common-db`

This is not a real connection string and must not be used to connect to any database.

Forbidden in P3:

- `.env`
- environment variable reads
- file reads
- Key Vault
- Azure secret SDKs
- real connection strings
- logging secret values
- DB connection attempts
