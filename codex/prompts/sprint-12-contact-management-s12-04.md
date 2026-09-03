# CRM Sprint 12 S12-04 - Contact Management Frontend Foundation Page

Repository:
https://github.com/christyepez/CRM

Objective:
Implement an Angular 18 foundation Contact Management page consuming only the foundation Contact API.

Expected route:

- `/foundation/contacts`

Expected UI scope:

- Contact list.
- Contact detail.
- Create Contact.
- Edit Contact.
- Preferred contact controls only if supported by the current API contract.
- Safe validation and error display.
- Foundation-only warnings.

Guardrails:

- Do not unlock productive `/api/crm/contacts`.
- Do not add DELETE.
- Do not implement Lead conversion.
- Do not activate Portal Auth runtime.
- Do not activate Common DB runtime.
- Do not add schema, migrations, EF runtime or SQL Server.
- Do not touch simulated Production.
- Do not add secrets, tokens, real connection strings or real data.

Acceptance:

- Angular build/test pass.
- Backend tests/guardrails remain green.
- Contact frontend consumes foundation API only.
- Productive Contact routes remain unavailable.
