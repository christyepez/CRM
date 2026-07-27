# CRM Common DB Probe Optional Activation Runbook

Sprint 5 P3 runbook:

1. Confirm `/api/crm/foundation/sprint-5/common-db-probe-optional-activation` returns `CommonDbProbeOptionalActivation`.
2. Confirm `commonDbProbeActivationApproved=false`.
3. Confirm `commonDbProbeEnabled=false`.
4. Confirm `commonDbConnectionAttempted=false`.
5. Confirm `secretProviderRuntimeConnected=false`.
6. Confirm `secretReadsEnabled=false`.
7. Confirm no database runtime, EF runtime, migrations, productive routes or DELETE endpoints are active.

Future activation requires explicit non-production approval, synthetic data, secret provider approval, rollback and observability evidence.
