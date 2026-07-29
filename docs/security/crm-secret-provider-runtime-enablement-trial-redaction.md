# CRM Secret Provider Runtime Enablement Trial Redaction

The P2 trial must never expose values.

Redaction guarantees:
- No value is returned to API.
- No value is logged.
- No value is persisted.
- No value is cached.
- No connection string is exposed.
- Only logical secret names and sanitized categories are reported.

Any future provider must preserve this contract before P3 consumes availability metadata.
