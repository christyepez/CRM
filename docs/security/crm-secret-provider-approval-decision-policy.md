# CRM Secret Provider Approval Decision Policy

The approval decision is planning-only.

Rules:

- No real secret read in Sprint 8 P1.
- No `.env` files.
- No sensitive environment variable reads.
- No secret values in source, docs, logs, API responses or persisted state.
- No runtime client, provider call or Azure Secret SDK usage.
- P2 may read only approved logical names in NonProduction after preserving redaction, rollback, observability and timeouts.
- Production activation remains `NoGo`.
