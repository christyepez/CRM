# CRM Persistence Seam Risk Register

| Risk | Status | Mitigation |
| --- | --- | --- |
| Seam mistaken for production storage | Open | API and docs return explicit non-production warning. |
| Productive CRUD introduced early | Guarded | Architecture tests block `/api/crm/leads`, `/api/crm/accounts`, `/api/crm/contacts` and DELETE. |
| Database activated before approval | Guarded | Tests and verifier block DB markers, migrations and SQL Server compose ownership. |
| In-memory preview loss surprises users | Accepted | Documented as non-durable; data is fictitious preview only. |
| Portal authorization skipped later | Open | Next gate is Portal authorization adapter simulation before productive CRUD. |
