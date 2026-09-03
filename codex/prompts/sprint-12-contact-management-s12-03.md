# CRM Sprint 12 S12-03 - Contact Foundation API Integration

Repository:
https://github.com/christyepez/CRM

Objective:
Wire the existing foundation Contact POST/PUT routes through `IContactManagementService` where safe, preserving existing contracts and foundation-only behavior.

Guardrails:

- Do not unlock productive `/api/crm/contacts`.
- Do not add DELETE.
- Do not implement Lead conversion.
- Do not activate Portal Auth runtime.
- Do not activate Common DB runtime.
- Do not add schema, migrations, EF runtime or SQL Server.
- Do not touch simulated Production.
- Do not add secrets, tokens, real connection strings or real data.

Expected work:

- Inspect existing foundation Contact routes and DTOs.
- Map API DTOs explicitly to Contact application requests.
- Invoke `IContactManagementService` for create/update if compatible.
- Preserve existing GET/preview/read-model behavior.
- Map deterministic application errors to safe HTTP responses.
- Keep productive route negative tests passing.
- Add API regression tests for valid create/update, invalid input, not-found and no-change behavior where route semantics support it.
- Update docs, verifier and next-task for S12-04.

Acceptance:

- Existing foundation Contact API remains backward compatible.
- POST/PUT route behavior is controlled by application service where wired.
- Productive Contact route remains unavailable.
- No Portal/Common DB/Productive runtime activation occurs.
