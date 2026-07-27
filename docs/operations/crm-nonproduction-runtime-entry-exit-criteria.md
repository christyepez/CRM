# CRM NonProduction Runtime Entry and Exit Criteria

Entry criteria before any future runtime trial:

- Sprint 5 P6 is merged.
- Synthetic data is approved.
- Rollback is approved.
- Observability is approved.
- Security review is complete.
- Architecture review is complete.
- Capability-specific approval is granted in its own sprint gate.

Exit criteria for Sprint 6 P1:

- Approval package documentation exists.
- GET-only foundation endpoint reports all runtime approvals as false.
- Tools validate docs, endpoint and warning.
- Negative routes remain 404.
- No secrets, DB, Portal HTTP, token/header reads, locked stubs runtime, DELETE or productive UI are enabled.
