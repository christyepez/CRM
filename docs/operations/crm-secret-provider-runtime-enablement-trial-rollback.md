# CRM Secret Provider Runtime Enablement Trial Rollback

Rollback:
1. Set `Crm:RuntimeTrials:SecretProviderEnabled=false`.
2. Restart the CRM API if required by deployment mechanism.
3. Confirm probe returns 423.
4. Confirm status reports `SecretProviderRuntimeEnablementTrialEnabled=false`.
5. Re-run guardrails and health checks.

No data cleanup is required because P2 does not persist or cache values.
