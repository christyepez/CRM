# CRM Sprint 7 P2 Secret Provider Real NonProduction Runtime Probe

Sprint 7 P2 prepares a controlled runtime probe for a real Secret Provider in NonProduction. The probe exists, but it is skipped by default because approval is not granted.

Default status:
- Runtime probe exists: true.
- Approval granted: false.
- Probe enabled: false.
- Probe attempted: false.
- Runtime connected: false.
- Real secret read attempted: false.
- Secret value returned to API: false.
- Probe skipped because approval not granted: true.

Allowed logical names only:
- `crm-common-db-connection`
- `crm-portal-auth-base-url`
- `crm-portal-auth-client-id`
- `crm-portal-auth-client-secret`
- `crm-observability-endpoint`

No production activation is allowed in this sprint.
