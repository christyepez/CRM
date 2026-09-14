# Sprint 25 S25-01 - Customer 360 Read Model Contracts and Validation Rules

Implement read-only Customer 360 domain contracts and deterministic validation/normalization.

Requirements:
- CustomerId is a non-empty GUID structural Account/customer-container reference.
- DisplayName required, trimmed, max 160.
- All aggregate counts are non-negative.
- No mutation/lifecycle operations.
- No Account duplication, Portal runtime, Common DB, external connectors or real data.
- Add unit tests and architecture guardrails.
