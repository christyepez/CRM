# CRM Secret Provider Controlled Real Read Redaction

La redacción es obligatoria. El valor leído no puede salir del runtime boundary.

## Permitido

- Estado sanitizado.
- Nombre lógico aprobado.
- Fingerprint irreversible truncado para correlación no sensible.

## Prohibido

- Valor secreto completo o parcial.
- Logs con valores.
- DTOs públicos con valores.
- Persistencia o cache de valores.
- Reintentos que impriman valores en errores.
