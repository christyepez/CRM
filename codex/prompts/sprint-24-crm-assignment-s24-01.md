# CRM Sprint 24 S24-01 - Assignment Reference Contracts and Domain Rules

Implement deterministic domain contracts/policy for assignment references.

Required: generated GUID id, related entity type/id validation, required opaque AssigneeReferenceId max 200, optional AssignmentLabel max 120, Active/Archived lifecycle, normalized no-change detection and archive idempotency.

Do not resolve assignee against Portal Security. No user/role/permission model, API, connector or runtime integration.

Add focused unit tests and keep all existing guardrails green.
