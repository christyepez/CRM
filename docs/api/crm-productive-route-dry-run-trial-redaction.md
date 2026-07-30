# CRM Productive Route Dry Run Trial Redaction

The P5 dry-run returns only sanitized metadata.

Never return:
- secrets
- tokens
- connection strings
- private Portal/Auth URLs
- client secrets
- certificate material
- user credentials
- real CRM data

Allowed response content:
- boolean safety flags
- sanitized status names
- locked status code
- non-sensitive route/method decision metadata
- next gate name

The implementation does not log, persist or cache secrets/tokens and does not inspect request auth material by default.
