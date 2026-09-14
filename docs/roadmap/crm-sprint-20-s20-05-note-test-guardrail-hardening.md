# CRM Sprint 20 S20-05 - Note Test and Guardrail Hardening

S2005Decision: Implemented

## Hardened behavior
- Exact Note text boundary remains 4000 characters.
- API rejects text above max with 400 and `TextTooLong`.
- Safe error responses do not expose exception or stack trace details.
- No-change updates remain successful with `Changed=false`.
- Repeated archive remains idempotent with `Changed=false`.
- Productive Note routes and DELETE remain unavailable.

## Safety
- Foundation-only runtime.
- No cross-entity mutation or Activity scheduling.
- No Portal-owned audit, notifications, files/content, users/roles or configuration duplication.
- No Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim, port 8094 or Production.