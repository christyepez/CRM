# CRM Secret Provider Real NonProduction Runtime Probe Redaction

P2 allows only metadata and logical names in responses or logs.

Allowed:
- module name;
- status;
- logical secret names;
- boolean gates;
- skipped reason;
- next gate.

Forbidden:
- secret values;
- tokens;
- certificates;
- connection strings;
- passwords;
- provider responses containing values.

The default implementation logs no secret values and returns no secret values.
