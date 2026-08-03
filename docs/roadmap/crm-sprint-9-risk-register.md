# CRM Sprint 9 Risk Register

| Risk | Mitigation |
| --- | --- |
| Sprint 10 planning is mistaken for production approval. | P6 records production activation as `NoGo`. |
| NonProduction trials are enabled without explicit flags. | Trials remain disabled/fail-closed by default. |
| Productive routes become visible by default. | Productive routes remain 404 by default. |
| Secrets, connection strings or tokens leak through status endpoints. | Status contracts expose sanitized metadata only. |
| DB runtime starts before productization approval. | DB writes, EF runtime, migrations and schema changes remain NoGo. |
