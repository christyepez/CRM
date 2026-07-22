# CRM Connection String Policy

Policy: `NoRealValuesInRepository`.

Rules:
- Real connection strings are not allowed in the repository.
- Passwords are not allowed in the repository.
- Private server names or URLs are not allowed in source.
- Only logical placeholders such as `CrmDb` may be documented before runtime approval.
- No `.env` file may be required or committed.

Sprint 3 P2 does not configure runtime connection values.
