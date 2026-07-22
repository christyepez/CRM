# CRM Migration and Rollback Strategy

Status: `PlannedOnly`.

Future migrations must be:
- reviewed before execution;
- idempotent where applicable;
- paired with rollback scripts;
- preceded by backup/restore validation;
- executed first against non-production data;
- tracked with evidence.

Sprint 3 P1 creates no real migrations, no EF migration files and no database changes.
