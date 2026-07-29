# CRM Secret Provider Controlled Real Read Policy

La lectura real controlada está autorizada solo para NonProduction y solo cuando un operador habilite el flag explícito fuera del repositorio.

## Reglas

- No `.env`.
- No secretos reales en appsettings.
- No tokens ni connection strings reales en Git.
- No lectura fuera del allow-list.
- No SDK productivo por defecto.
- No Production.
- No DB/Auth/Portal runtime en P2.

Si falta cualquier condición, el runtime debe responder Locked, Skipped o Blocked sin intentar lectura.
