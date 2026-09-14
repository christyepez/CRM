# CRM Sprint 23 S23-05 - CRM Tag Test and Guardrail Hardening

Status: Implemented
S2305Decision: Implemented

Hardened:
- Name 80 accepted / 81 rejected.
- Description 500 accepted / 501 rejected.
- Invalid assignment reference returns 400.
- API no-change returns Changed=false.
- Assignment-only update returns Changed=true.
- Archive idempotency and archived read-only retained.
- Productive route/DELETE absent.
- Portal identity/config runtime and related-entity mutation remain disabled.
