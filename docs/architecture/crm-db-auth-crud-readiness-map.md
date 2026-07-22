# CRM DB/Auth/CRUD Readiness Map

DB, Auth and productive CRUD must be activated together only after gates are approved.

- DB requires migration governance, connection management, secrets, backup/restore, retention and rollback.
- Auth requires Portal runtime, tenant context, permission decisions, audit and no CRM-owned login.
- Productive CRUD requires DB + Auth + audit + observability + PII handling + test data policy.

P5 recommendation: keep all three in review and proceed to P6 gate decision.
