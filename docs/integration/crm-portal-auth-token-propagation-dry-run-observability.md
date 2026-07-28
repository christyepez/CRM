# CRM Portal Auth Token Propagation Dry-Run Observability

Required observability signals for P4:

- Dry-run endpoint is reachable.
- `tokenReadAttempted=false`.
- `headerReadAttempted=false`.
- `portalHttpAttempted=false`.
- `realTokenUsed=false`.
- `realHeadersRead=false`.
- `productiveAuthorizationEnabled=false`.
- Synthetic references are visible and never converted into real tokens.

Runtime logs must not contain secrets, bearer values, Authorization values, user tokens, cookies or real Portal URLs.

Before any future real propagation, the team must add approved telemetry for token propagation attempts, failures, fallback behavior, rollback status and Portal dependency health.
