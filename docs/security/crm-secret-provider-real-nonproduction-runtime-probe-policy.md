# CRM Secret Provider Real NonProduction Runtime Probe Policy

The P2 policy is approval-gated and non-production only.

The implementation must not:
- read real secret values;
- materialize secret values;
- return values from API responses;
- log secret values;
- create `.env`;
- read `.env`;
- create a Key Vault runtime client by default;
- call Key Vault;
- use Azure secret SDK runtime calls;
- connect to DB or Portal runtime.

Future activation requires Security, Architecture and DevOps approval, redaction validation, rollback validation and observability validation.
