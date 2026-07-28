# CRM Secret Provider Real NonProduction Secret Boundary

CRM may reference logical secret names only. Values remain outside the repository and outside P1 runtime.

Boundary:

- CRM does not own the secret store.
- CRM does not persist secret values.
- CRM does not log secret values.
- CRM does not read `.env` or files.
- CRM does not create a real secret client in P1.
- CRM does not resolve real database or Portal Auth configuration in P1.

P2 may request a controlled runtime probe only after explicit approval.
