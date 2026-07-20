# CRM Leads Foundation

Sprint 1 P3 strengthens Lead rules in foundation mode only.

## Rules

- Lead requires person name and valid email.
- Source and campaign are conceptual metadata.
- Allowed transitions:
  - New to Contacted.
  - Contacted to Qualified.
  - Qualified to Converted.
  - New, Contacted or Qualified to Disqualified.
- Invalid transitions throw controlled domain errors.
- Disqualification requires a reason.

## Not production

- No persistence.
- No productive lead CRUD endpoint.
- No Portal or Financiero runtime integration.
- Preview responses must state: `Preview only, not persisted`.
