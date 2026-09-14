# CRM Sprint 25 S25-01 - Customer 360 Contracts and Validation Rules

S2501Decision: Implemented
- Read-only `Customer360Snapshot` contract.
- Customer/Account structural id is a non-empty GUID and is normalized to D format.
- Display name is trimmed, required and limited to 160 characters.
- Aggregate counts must be non-negative.
- No commands, lifecycle mutation, CRUD, Portal runtime, Common DB or external connectors.
