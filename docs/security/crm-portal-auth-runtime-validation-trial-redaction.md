# CRM Portal Auth Runtime Validation Trial Redaction

P4 never returns private Portal URLs, client secrets, tokens, Authorization headers or claims.

Sanitized metadata flags must remain false unless a later approved NonProduction gate explicitly changes behavior:
- PortalAuthUrlReturnedToApi.
- PortalClientSecretReturnedToApi.
- AuthHeaderRead.
- TokenRead.
- TokenStored.
- ClaimsMapped.

Logs, API responses, persisted state and caches must contain only boolean/status metadata.
