# CRM Portal Auth Runtime Validation Trial Runbook

Default operation:
1. Keep `Crm:RuntimeTrials:PortalAuthValidationEnabled=false`.
2. Start CRM normally.
3. Validate `GET /api/crm/foundation/sprint-9/portal-auth-runtime-validation-trial`.
4. Validate the probe returns `423 Locked` by default.

Activation requires a future explicit NonProduction approval. Do not configure private Portal URLs, client secrets or tokens in repository files.

If unexpected behavior appears, disable the flag and redeploy.
