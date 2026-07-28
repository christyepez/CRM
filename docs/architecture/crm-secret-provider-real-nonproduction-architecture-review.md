# CRM Secret Provider Real NonProduction Architecture Review

Architecture decision for P1: approval package only.

CRM remains a consumer of a future external secret provider boundary. CRM does not implement or own secret infrastructure.

Approved in P1:

- Logical names.
- Approval gates.
- Evidence checklist.
- Foundation endpoint.
- Contract-only placeholder.

Not approved in P1:

- Runtime secret provider connection.
- Secret value read.
- DB runtime.
- Portal Auth runtime.
- Productive routes.
- DELETE.
- Productive UI.
