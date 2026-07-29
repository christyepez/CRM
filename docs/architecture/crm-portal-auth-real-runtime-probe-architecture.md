# CRM Portal Auth Real Runtime Probe Architecture

P4 prepares the shape of a future Portal Auth runtime probe without activating it.

Flow:

`API foundation endpoint -> Application status service -> contract-only infrastructure placeholder`

The placeholder performs no external I/O. It does not create HTTP clients, read headers, read tokens, resolve secrets, call Portal, connect to a database or activate authorization middleware.

Ownership:

- PortalCorporativo owns Auth, Identity, roles and permissions.
- CRM owns only domain behavior and safe consumer-side readiness metadata.

Next architectural gate: locked productive route runtime registration with `423 Locked` behavior.
