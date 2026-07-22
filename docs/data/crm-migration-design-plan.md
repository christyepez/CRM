# CRM Migration Design Plan

P1 does not create migrations.

Future migration design should define:

- Naming convention.
- Idempotency expectations.
- Rollback approach.
- Non-production seed separation.
- No shared database with Financiero.
- No Portal-owned data duplicated in CRM tables.

Sprint 2 P2 may create a persistence seam only after approval.
