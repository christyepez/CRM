# CRM Productive API Contract Draft

## Sprint 4 P4 locked stub validation

Productive route shapes remain draft/documented only. `LockedStubsStrategy=DocumentOnlyPreferred`, `ProductiveRoutesRegistered=false` and `LockedStubsRegistered=false`.

Future productive routes, not registered in P5:

| Method | Route | Resource | P5 status |
| --- | --- | --- | --- |
| GET | `/api/crm/leads` | Lead | DraftOnly |
| GET | `/api/crm/leads/{id}` | Lead | DraftOnly |
| POST | `/api/crm/leads` | Lead | DraftOnly |
| PUT | `/api/crm/leads/{id}` | Lead | DraftOnly |
| GET | `/api/crm/accounts` | Account | DraftOnly |
| GET | `/api/crm/accounts/{id}` | Account | DraftOnly |
| POST | `/api/crm/accounts` | Account | DraftOnly |
| PUT | `/api/crm/accounts/{id}` | Account | DraftOnly |
| GET | `/api/crm/contacts` | Contact | DraftOnly |
| GET | `/api/crm/contacts/{id}` | Contact | DraftOnly |
| POST | `/api/crm/contacts` | Contact | DraftOnly |
| PUT | `/api/crm/contacts/{id}` | Contact | DraftOnly |

DELETE remains NO-GO.
## Sprint 5 P5 Locked Stub Trial

The productive API contract remains draft-only. P5 does not register productive routes. Future explicit non-production stubs, if approved, must return 423 Locked and must not execute domain logic.
