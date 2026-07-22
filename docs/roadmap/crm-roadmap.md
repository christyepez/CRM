# CRM Roadmap

## Current status

Sprint 1 P2 adds a draft domain and API contract baseline. CRM is not production-ready and does not contain business CRUD, persistence or real integrations.

## Sprint 1 P2

- Draft CRM domain model.
- Draft API contract catalog.
- Portal and Financiero boundaries documented.
- No persistence or production commands.

## Sprint 1 P3

- Lead, Account and Contact foundation rules.
- Foundation-only preview services and endpoints.
- Persistence remains none.
- Productive CRM APIs remain blocked.

## Sprint 1 P4

- Persistence strategy documented.
- Read model contracts and preview endpoints added.
- Application ports prepared without productive activation.
- Database and migrations remain blocked.

## Roadmap

1. Repository foundation and architecture baseline.
2. CRM core domain model.
3. Leads, accounts and contacts.
4. Opportunities and pipeline.
5. Activities and follow-up.
6. Portal integration.
7. Financial integration.
8. Reporting/BI.
9. Productization readiness.

## Dependency posture

Sprints 1-5 can advance with isolated architecture and domain work. Sprints 6-9 require Portal readiness, shared DB strategy and governance approval.
# Roadmap Addendum - Portal Integration

After Sprint 1 P5, CRM remains disconnected from Portal at runtime. The next step is a controlled adapter sprint only after PortalCorporativo publishes stable consumer contracts and environment configuration.
