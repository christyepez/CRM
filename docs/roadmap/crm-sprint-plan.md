# CRM Sprint Plan

## Sprint 1 P2 - Core domain and contracts

- CREATE: pure CRM domain concepts.
- CREATE: read-only contract/catalog endpoints.
- CREATE: architecture and domain tests.
- BLOCKED/Deferred: persistence, commands, Portal runtime integration and Financiero integration.

| Sprint | Scope | Can advance isolated | Depends on Portal/SQL |
|---|---|---|---|
| Sprint 1 Foundation | Solution, API health, Angular shell, Docker, guardrails. | Yes | No real integration. |
| Sprint 2 CRM Core Domain | Entities/value objects/contracts. | Yes | DB decision for persistence. |
| Sprint 3 Leads/Accounts/Contacts | Core business use cases. | Partially | Auth/permissions before runtime. |
| Sprint 4 Opportunities/Pipeline | Opportunity workflow. | Partially | Configuration/menu later. |
| Sprint 5 Activities/Follow-up | Tasks, notes, interactions. | Partially | Audit/Notification later. |
| Sprint 6 Portal integration | Auth/Menu/Audit/Notification adapters. | No | Yes. |
| Sprint 7 Financial integration | Events/API with Financiero. | No | Financiero decoupling/PASS decision. |
| Sprint 8 Reporting/BI | Read models and dashboards. | No | Reporting strategy. |
| Sprint 9 Productization readiness | E2E, security, ops, release gates. | No | Full platform readiness. |

## P1 decision

CRM may proceed independently only as foundation and isolated domain work. Production and cross-system runtime integration remain blocked until Portal/SQL and governance decisions are accepted.
