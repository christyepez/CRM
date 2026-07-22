# CRM DbContext Prototype Design

The P3 prototype is intentionally minimal:

- Name: `CrmDbContextPrototype`.
- Runtime active: `false`.
- Inherits real EF context: `false`.
- Provider configured: `false`.
- Productive CRUD enabled: `false`.

It is not a replacement for foundation stores. It does not model full tables, relationships, indexes or migrations yet.

Future design work must map CRM aggregate ownership, tenant boundaries, audit/outbox reuse and Portal authorization before enabling durable persistence.
