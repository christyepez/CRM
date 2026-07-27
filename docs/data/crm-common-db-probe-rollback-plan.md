# CRM Common DB Probe Rollback Plan

No runtime rollback is needed in Sprint 5 P3 because the probe is not enabled.

Future rollback requirements:

1. Disable the probe flag immediately if any unexpected database access occurs.
2. Return CRM to foundation-only endpoints.
3. Keep `/api/crm/leads`, `/api/crm/accounts` and `/api/crm/contacts` inactive.
4. Keep health/readiness green.
5. Confirm no secret values appear in logs or responses.

Rollback is required before any future non-production activation.
