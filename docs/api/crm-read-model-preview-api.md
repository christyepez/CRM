# CRM Read Model Preview API

P4 adds foundation read-model preview endpoints. They do not read from a database and are not production APIs.

## GET /api/crm/foundation/leads/read-model-preview

Returns Lead read model preview metadata and fictitious safe sample data.

## GET /api/crm/foundation/accounts/read-model-preview

Returns Account read model preview metadata and fictitious safe sample data.

## GET /api/crm/foundation/contacts/read-model-preview

Returns Contact read model preview metadata and fictitious safe sample data.

## GET /api/crm/foundation/read-model-status

Returns persistence strategy status and activation criteria.

## Common response policy

- module: CRM.
- foundationMode: true.
- source: FoundationMock.
- persistence: None.
- runtimeMode: NonProduction.
- warning: Read model preview only, not persisted.
