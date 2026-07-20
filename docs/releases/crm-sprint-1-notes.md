# CRM Sprint 1 P1 Release Notes

Date: 2026-07-20
Phase: Repository Foundation and Architecture Baseline

## P2 - Core Domain Discovery and API Contract Baseline

- Added pure CRM domain concepts, value objects, statuses and conceptual domain events.
- Added draft application contract catalog.
- Added read-only API contract endpoints for domain catalog, contracts and integration boundaries.
- Added docs for domain model, business rules, API contracts and Portal/Financiero boundaries.
- Added unit and architecture tests for domain behavior and guardrails.
- Still no DB, migrations, CRUD, Identity, token storage or real integrations.

## Summary

CRM repository foundation starts with .NET 8, Angular, Docker Compose and architecture guardrails.

## Added

- `CRM.sln`.
- Clean Architecture projects.
- Health/readiness API endpoints.
- Angular foundation shell.
- Docker Compose without SQL Server.
- Verification tools.
- Architecture, security and roadmap documentation.

## Not added

- CRM CRUD.
- CRM database/migrations.
- Login, Identity, roles, token storage.
- Gateway or Portal Shell.
- Real Financiero integration.
- Production activation.
