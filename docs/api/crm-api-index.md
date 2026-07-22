# CRM API Index

## Draft endpoints

- `GET /health`
- `GET /health/live`
- `GET /health/ready`
- `GET /api/crm/readiness`
- `GET /api/crm/domain-catalog`
- `GET /api/crm/contracts`
- `GET /api/crm/integration-boundaries`
- `POST /api/crm/foundation/leads/preview`
- `POST /api/crm/foundation/accounts/preview`
- `POST /api/crm/foundation/contacts/preview`
- `GET /api/crm/foundation/leads/read-model-preview`
- `GET /api/crm/foundation/accounts/read-model-preview`
- `GET /api/crm/foundation/contacts/read-model-preview`
- `GET /api/crm/foundation/read-model-status`

## Runtime

- Port: 8093.
- Mode: NonProduction.
- Contract status: Draft.
- Database: none.
- Foundation previews: enabled, not persisted.
- Read models: PreviewOnly.

## Next API work

Future sprints may add read models and controlled command endpoints only after Portal Security, Audit and Configuration boundaries are wired.
# CRM API Index - P5 Addendum

Portal foundation readiness endpoints:

- `GET /api/crm/foundation/portal-integration/status`
- `GET /api/crm/foundation/portal-integration/contracts`
- `GET /api/crm/foundation/portal-integration/required-capabilities`

These endpoints are contract/readiness only and must not be treated as productive CRM domain APIs.

# CRM API Index - P6 Addendum

Financial foundation readiness endpoints:

- `GET /api/crm/foundation/financial-integration/status`
- `GET /api/crm/foundation/financial-integration/contracts`
- `GET /api/crm/foundation/financial-integration/required-capabilities`
- `GET /api/crm/foundation/financial-integration/events`

These endpoints are contract/readiness only and must not be treated as productive CRM or financial APIs.
