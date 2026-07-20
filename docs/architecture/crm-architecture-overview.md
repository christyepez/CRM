# CRM Architecture Overview

CRM is an independent business module for customer relationship management. It uses Clean Architecture + DDD and is prepared to integrate with PortalCorporativo and Financiero without direct coupling.

## Current state

- Runtime mode: `NonProduction`.
- Status: `ReadyForFoundationOnly`.
- Backend: .NET 8.
- Frontend: Angular.
- Containers: Docker Compose with `crm-api` only.
- Database: not created in Sprint 1 P1.

## Foundation responsibilities

- Expose health/readiness endpoints.
- Establish solution structure.
- Establish frontend shell foundation.
- Preserve architecture guardrails.
- Prepare future Portal and Financiero integration seams.

## Out of scope

- CRM CRUD.
- CRM database and migrations.
- Login/Identity/roles propios.
- Gateway or Portal Shell duplication.
- Real Financiero integration.
- Production activation.
