# CRM Runtime Probe Rollback Plan

Rollback is required before any future probe activation.

Rollback triggers:

- A probe attempts DB, Portal or route runtime access outside its approval gate.
- Health/readiness regresses.
- Any productive CRM route returns success before approval.
- Logs contain secrets, tokens, connection strings or personal data.

Rollback actions:

- Disable the probe flag.
- Return traffic to foundation-only endpoints.
- Re-run health, guardrails and negative route checks.
- Preserve evidence for gate review.

No rollback execution is needed in Sprint 5 P1 because no runtime activation occurs.
