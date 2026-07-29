# CRM Secret Provider Redaction Approval

Redaction approval requires:

- Do not log secret values.
- Do not return secret values through API responses.
- Do not persist secret values.
- Do not cache secret values beyond the immediate controlled read scope.
- Log only logical name, result state, correlation data and sanitized error category.
- Treat provider URLs, client identifiers and credentials as sensitive unless explicitly classified otherwise.
