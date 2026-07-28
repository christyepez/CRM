# CRM Common DB Real Connectivity NonProduction Probe Policy

P3 is gated and non-production only.

The implementation must not:
- resolve real connection strings;
- materialize connection strings;
- log connection strings;
- return connection strings through API;
- open SQL or DB connections;
- enable EF runtime;
- create migrations;
- add SQL Server compose services;
- activate productive CRM routes.

Future activation requires Security, Architecture, DevOps, Secret Provider real runtime approval, `crm-common-db-connection` approval, rollback validation, timeout policy and redacted observability.
