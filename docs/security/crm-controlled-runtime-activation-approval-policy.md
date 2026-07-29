# CRM Controlled Runtime Activation Approval Policy

Sprint 9 P1 approves NonProduction trials only. No runtime trial is enabled now.

Each later trial must prove:
- NonProduction-only execution.
- Explicit flag required.
- Fail-closed default.
- No secret/token/header value is returned, logged or persisted.
- Security approval is recorded before execution.

Production activation remains NoGo.
