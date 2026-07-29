# CRM Portal Auth Controlled Runtime Validation Rollback

Rollback is disabling the explicit NonProduction validation flag.

Rollback guarantees:

- No Portal HTTP call by default.
- No token/header reads.
- No CRM auth middleware.
- No productive route activation.
- No persisted roles or permissions.
- No database schema changes.

If a future provider fails validation, restore disabled mode and keep endpoint metadata fail-closed.
