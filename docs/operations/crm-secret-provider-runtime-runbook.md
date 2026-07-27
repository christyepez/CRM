# CRM Secret Provider Runtime Runbook

Sprint 5 P2 runbook:

1. Confirm branch is based on GitHub `main`.
2. Confirm no `.env`, certificates, keys, tokens, passwords or real values exist.
3. Confirm `/api/crm/foundation/sprint-5/secret-provider-runtime-contract` returns status `SecretProviderRuntimeContractValidation`.
4. Confirm `secretProviderRuntimeConnected=false`.
5. Confirm `secretProviderReadsEnabled=false`.
6. Confirm `secretReadAttemptedByRuntime=false`.
7. Confirm no DB/Auth/Portal runtime, productive routes or DELETE endpoints are active.

Rollback: no runtime rollback is needed in P2 because no secret provider runtime is connected. If any future probe reads a secret unexpectedly, disable the probe flag and return CRM to foundation-only endpoints.
