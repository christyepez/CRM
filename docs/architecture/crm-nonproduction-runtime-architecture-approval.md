# CRM NonProduction Runtime Architecture Approval

Architecture approval is required and not granted in Sprint 6 P1.

Future approval must prove:

- CRM does not own SQL Server.
- CRM uses the shared local SQL Server only after explicit dry-run approval.
- CRM does not duplicate Portal Security/Auth, Audit, Configuration, Notification or Gateway.
- Portal Auth token propagation remains contract-bound.
- Productive CRM routes are introduced only behind explicit gates.

Sprint 6 P1 is an approval package; no runtime activation is included.
