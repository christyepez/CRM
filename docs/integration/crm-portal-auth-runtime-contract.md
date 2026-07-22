# CRM Portal Auth Runtime Contract

CRM expects PortalCorporativo to provide the future runtime boundary for:

- User identity.
- Tenant context.
- Normalized permission/capability result.
- Normalized claims.
- Correlation id.
- Audit-ready policy decision metadata.

CRM does not own Auth. It must not implement login, Identity, JWT/cookie auth, credential storage, persisted roles or permission persistence.

Minimum capabilities:
- `crm.foundation.read`.
- `crm.foundation.preview.write`.
- `crm.foundation.preview.clear`.
- `crm.readiness.read`.

Future productive capabilities:
- `crm.leads.read`.
- `crm.leads.write`.
- `crm.accounts.read`.
- `crm.accounts.write`.
- `crm.contacts.read`.
- `crm.contacts.write`.

P4 is contract-only. Runtime activation moves to later gates.
