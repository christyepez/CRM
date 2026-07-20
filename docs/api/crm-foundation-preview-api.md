# CRM Foundation Preview API

These endpoints validate contracts and rules without persistence. They are not productive CRM APIs.

## Common response

Every preview response includes:

- module: CRM.
- foundationMode: true.
- persistence: None.
- runtimeMode: NonProduction.
- warning: Preview only, not persisted.

## POST /api/crm/foundation/leads/preview

Validates lead name, email, optional source/company metadata and previews the New to Contacted transition.

## POST /api/crm/foundation/accounts/preview

Validates account company name and optional conceptual metadata, then previews Active status.

## POST /api/crm/foundation/contacts/preview

Validates contact name, optional email/phone and preferred contact method rules.

## Not allowed

- No productive `/api/crm/leads`.
- No productive `/api/crm/accounts`.
- No productive `/api/crm/contacts`.
- No PUT, PATCH or DELETE.
- No database, migrations or runtime integration.
