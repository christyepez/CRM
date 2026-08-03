# CRM Sprint 9 Gate Decision Runbook

1. Confirm `main` contains the approved Sprint 9 P5 merge.
2. Validate `GET /api/crm/foundation/sprint-9/gate-decision`.
3. Confirm P2/P3/P4/P5 probes still return 423 by default.
4. Confirm productive routes still return 404 by default.
5. Confirm there is no SQL Server service owned by CRM.
6. Do not enable production flags from this gate.

Rollback is simple: revert the P6 documentation/status endpoint commit. No runtime state or database state is changed by P6.
