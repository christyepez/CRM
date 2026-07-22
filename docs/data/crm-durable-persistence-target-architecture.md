# CRM Durable Persistence Target Architecture

Target:
- CRM must not own a SQL Server container.
- CRM will use an approved common SQL Server environment only after gates pass.
- CRM must use its own logical database and schema ownership.
- CRM must not share tables with Financiero.
- CRM must not duplicate Portal user, tenant or permission data.

Sprint 3 P1 remains design-only. Runtime persistence remains `NonProductionSeam` with in-memory stores.
