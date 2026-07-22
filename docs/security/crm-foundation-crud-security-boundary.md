# CRM Foundation CRUD Security Boundary

P4 uses `CrmFoundationPermissionGuard` with Portal authorization simulation only.

This is not productive security:

- No login.
- No Identity.
- No JWT or cookie auth.
- No stored credentials.
- No real users or persisted roles.
- No Portal runtime HTTP calls.

Simulated permissions:

- Read: `crm.foundation.read`
- Write: `crm.foundation.preview.write`
- Clear: `crm.foundation.preview.clear`

Denied simulation results are returned in the payload; the sprint does not introduce productive 401/403 behavior.
