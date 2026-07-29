# CRM Portal Auth Real Runtime Probe Runbook

1. Confirm branch is based on GitHub `main`.
2. Run build, tests, frontend verification and guardrails.
3. Start Docker with CRM API only.
4. Verify `/api/crm/foundation/sprint-7/portal-auth-real-runtime-probe`.
5. Confirm all runtime markers remain false.
6. Confirm synthetic references are returned.
7. Confirm `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` remain 404.

Do not enable the real probe without Security, Architecture, DevOps and Portal owner approval.
