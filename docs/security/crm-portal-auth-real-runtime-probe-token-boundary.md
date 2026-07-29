# CRM Portal Auth Real Runtime Probe Token Boundary

P4 forbids token and header access.

CRM must not:

- read Authorization headers
- parse bearer tokens
- persist tokens
- log tokens
- return tokens by API
- validate JWTs with CRM-owned identity
- create cookie authentication
- create login/logout endpoints

Any future runtime activation must prove token redaction, nonproduction-only scope, Portal ownership, rollback and observability before implementation.
