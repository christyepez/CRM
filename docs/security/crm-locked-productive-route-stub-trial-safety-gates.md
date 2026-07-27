# CRM Locked Productive Route Stub Trial Safety Gates

P5 does not approve productive runtime activation.

Safety gates:

1. Productive route registration approval remains false.
2. Runtime flag default remains false.
3. DELETE remains prohibited.
4. Auth runtime remains disabled.
5. Portal HTTP remains disabled.
6. Token/header reads remain prohibited.
7. DB/EF/migrations/connection strings remain prohibited.
8. Negative route checks for leads, accounts and contacts remain 404 by default.
