# CRM Sprint 12 S12-02 - Contact Application Service

Repository:
https://github.com/christyepez/CRM

Objective:
Implement Contact application orchestration that invokes the S12-01 Contact Management domain policy while reusing the existing foundation Contact store and foundation contracts where possible.

Guardrails:

- Do not rebuild Contact CRUD from scratch.
- Do not unlock productive `/api/crm/contacts`.
- Do not implement DELETE.
- Do not implement Lead conversion.
- Do not activate Portal Auth runtime.
- Do not activate Common DB runtime.
- Do not add schema, migrations or EF runtime.
- Do not touch simulated Production.
- Do not add secrets, tokens, certificates, real connection strings or real data.

Expected work:

- Add an application-level Contact Management service if missing.
- Reuse existing foundation store/repository seams.
- Invoke `ContactManagementPolicy` for create/update validation.
- Map deterministic domain errors to safe application results.
- Suppress persistence when update returns `Changed=false`.
- Preserve foundation API compatibility.
- Keep productive routes locked.
- Add focused application tests.
- Keep all existing unit, architecture, frontend and guardrail checks green.

Acceptance:

- S12-01 domain rules are used by the application service.
- Existing foundation CRUD behavior remains compatible.
- No-change update does not perform unnecessary persistence.
- Not-found handling is explicit.
- No Portal/Common DB/Productive route activation occurs.
