# CRM Common DB Probe Secret Dependency

Common DB probe activation depends on the Sprint 5 P2 Secret Provider contract. P3 does not connect the provider and does not read secrets.

Required before future activation:

- Secret Provider runtime approved.
- Secret reads explicitly approved for non-production only.
- Logical secret names remain names only in repository files.
- No `.env` file.
- No real connection string in repository files.
- No sensitive value in API responses, logs, tests or documentation.
- Least privilege, rotation, rollback and synthetic data approvals completed.
