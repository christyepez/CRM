# CRM Secret Provider Controlled Read Architecture Decision

Decision: approve planning for controlled NonProduction read in Sprint 8 P2.

Architecture boundaries:

- CRM does not own secrets.
- Secret values remain outside the repository.
- P1 adds no runtime provider client.
- P1 adds no DB, Portal Auth or productive route activation.
- P2 must use approved logical names only and expose status metadata only.
- Productive activation remains `NoGo`.
