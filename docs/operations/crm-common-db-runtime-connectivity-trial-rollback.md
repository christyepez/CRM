# CRM Common DB Runtime Connectivity Trial Rollback

Rollback:

1. Set `Crm:RuntimeTrials:CommonDbConnectivityEnabled=false`.
2. Restart CRM API if required by the deployment mechanism.
3. Confirm the probe returns 423.
4. Confirm status reports `CommonDbRuntimeConnectivityTrialEnabled=false`.
5. Re-run guardrails and health checks.

No data cleanup is required because P3 does not create schema, execute migrations, persist data or cache connection strings.
