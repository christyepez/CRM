# CRM Portal Auth Controlled Runtime Validation Runbook

Default local behavior:

1. Keep the explicit runtime validation flag disabled.
2. Start CRM normally with Docker.
3. Check `GET /api/crm/foundation/sprint-8/portal-auth-controlled-real-runtime-validation`.
4. Optional probe should return `423 Locked` while disabled.

To evaluate a future real NonProduction provider, require security approval, approved logical secret names, short timeout, sanitized logs and rollback approval. Production remains NoGo.
