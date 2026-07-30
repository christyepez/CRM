# CRM Common DB Runtime Connectivity Trial Runbook

Default run:

- Keep `Crm:RuntimeTrials:CommonDbConnectivityEnabled=false`.
- Validate status endpoint.
- Probe endpoint must return 423 when disabled.

Controlled NonProduction trial:

- Validate Sprint 9 P2 Secret Provider metadata-only boundary first.
- Enable only the explicit NonProduction flag.
- Probe only `crm-common-db-connection`.
- Capture sanitized status, category and elapsed time.
- Do not create schema, run migrations or enable EF/productive CRUD.

Production is blocked.
