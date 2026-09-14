# CRM Sprint 22 S22-06 - CRM Document Metadata Local Integration Validation

## Objective
Validate the complete CRM Document Metadata foundation slice over local HTTP.

## Required
- Use isolated localhost API/frontend ports; never 8094.
- Validate health, frontend route and proxy.
- Validate list/detail/create/update/no-change/400/404/archive/repeat archive/409.
- Confirm productive document route and DELETE unavailable.
- Confirm no binary storage, Portal Content/File runtime, Common DB, real data or Production.
- Capture real latency evidence.
- Stop only processes created for this validation.
