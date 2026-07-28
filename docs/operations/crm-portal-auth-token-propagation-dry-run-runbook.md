# CRM Portal Auth Token Propagation Dry-Run Runbook

Runbook for Sprint 6 P4:

1. Confirm GitHub `main` contains Sprint 6 P3.
2. Run `dotnet test CRM.sln`.
3. Run frontend foundation verification.
4. Run `tools/preflight-crm-local.ps1`.
5. Run `tools/check-crm-guardrails.ps1`.
6. Run `tools/verify-crm-foundation.ps1`.
7. Start Docker with `docker compose up -d --build`.
8. Validate `/api/crm/foundation/sprint-6/portal-auth-token-propagation-dry-run`.
9. Confirm negative routes `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` stay inactive.

Rollback:

- Disable or remove the P4 endpoint registration.
- Remove the P4 service and placeholder.
- Keep PortalCorporativo as the only owner of Auth, SSO, user, tenant and permissions.

No secret cleanup is required because P4 must not read or store secrets.
