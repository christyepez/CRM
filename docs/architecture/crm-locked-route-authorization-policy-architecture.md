# CRM Locked Route Authorization Policy Architecture

P5 introduces a pure application evaluator:

- Input: route, method, locked registration flag, locked authorization policy flag, NonProduction state.
- Output: sanitized decision metadata.
- Default decision: `NotEvaluatedBecauseDisabled`.
- Explicit locked NonProduction decision: `BlockedBecauseRouteLocked`.

The evaluator has no dependencies on HTTP context, Portal clients, DB connections, stores, domain services, EF, secrets, or configuration files.

The API registrar consumes the evaluator only for locked 423 responses and only after explicit NonProduction registration. It does not register DELETE and does not activate productive CRUD.
