# CRM Logical Data Model

P2 candidate aggregates:

| Aggregate | Initial persistence status | Notes |
| --- | --- | --- |
| Lead | Candidate first | No real data; no productive CRUD yet. |
| Account | Candidate first | Customer relationship owner only. |
| Contact | Candidate first | No identity or auth data. |
| Opportunity | Deferred | Depends on Lead/Account/Contact seam. |

No EF model, DbContext, DbSet, migration or SQL object is created in P1.
