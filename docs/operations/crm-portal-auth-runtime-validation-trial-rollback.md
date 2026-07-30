# CRM Portal Auth Runtime Validation Trial Rollback

Rollback is immediate because the trial is disabled by default.

Steps:
1. Set `Crm:RuntimeTrials:PortalAuthValidationEnabled=false`.
2. Redeploy CRM.
3. Confirm status returns `PortalAuthRuntimeValidationTrialEnabled=false`.
4. Confirm probe returns `423 Locked`.

No database rollback is required. P4 creates no schema, migrations, login/logout, token storage or Identity runtime.
