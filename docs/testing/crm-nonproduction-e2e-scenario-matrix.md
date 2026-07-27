# CRM Non-Production E2E Scenario Matrix

Foundation-only scenarios:

| Scenario | Method | Endpoint | Expected |
| --- | --- | --- | --- |
| Health | GET | `/health` | 200 |
| Liveness | GET | `/health/live` | 200 |
| Readiness | GET | `/health/ready` | 200 |
| CRM readiness | GET | `/api/crm/readiness` | 200 |
| Sprint 3 productization review | GET | `/api/crm/foundation/sprint-3/productization-review` | 200 |
| Sprint 4 runtime readiness | GET | `/api/crm/foundation/sprint-4/runtime-readiness` | 200 |
| Common DB runtime probe | GET | `/api/crm/foundation/sprint-4/common-db-runtime-probe` | 200 |
| Portal Auth runtime probe | GET | `/api/crm/foundation/sprint-4/portal-auth-runtime-probe` | 200 |
| Productive routes locked stub | GET | `/api/crm/foundation/sprint-4/productive-routes-locked-stub` | 200 |
| Non-production E2E pilot readiness | GET | `/api/crm/foundation/sprint-4/nonproduction-e2e-pilot-readiness` | 200 |

Negative route validation:

- `GET /api/crm/leads` must not be active.
- `GET /api/crm/accounts` must not be active.
- `GET /api/crm/contacts` must not be active.
- DELETE is not allowed.
