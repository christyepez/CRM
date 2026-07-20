# CRM Accounts Foundation

Sprint 1 P3 defines Account behavior in foundation mode only.

## Rules

- Account requires company name.
- Tax identifier is optional and conceptual; no tributary validation is implemented.
- Industry and segment are optional conceptual metadata.
- Account can be marked active or inactive.
- Account can reference contacts by CRM id.
- Duplicate contact references are rejected.

## Not production

- No database table.
- No migration.
- No productive account endpoint.
- No shared database with Portal or Financiero.
