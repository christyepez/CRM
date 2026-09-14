# CRM Sprint 25 - Customer 360 Read Model Functional Baseline

Status: Defined
P1Decision: GoFoundationOnly

## Purpose
Provide a deterministic, read-only Customer 360 foundation view without introducing a duplicate Customer CRUD aggregate.

## Ownership
- Account remains the CRM organization/customer container.
- Customer 360 is a derived read model only.
- Portal continues to own generic identity, authorization, audit, notifications and configuration.

## Read model contract
- CustomerId: non-empty GUID structural reference to the Account/customer container.
- DisplayName: required, trimmed, max 160.
- ContactCount: non-negative integer.
- OpenOpportunityCount: non-negative integer.
- OpenCaseCount: non-negative integer.
- InteractionCount: non-negative integer.
- NoteCount: non-negative integer.
- DocumentCount: non-negative integer.
- TagCount: non-negative integer.
- AssignmentCount: non-negative integer.

## Runtime boundaries
- Deterministic synthetic foundation data only.
- Read-only list/detail behavior.
- No create/update/archive/delete operations.
- No cross-entity mutation.
- No productive `/api/crm/customer360` route.
- No Common DB, Portal runtime, external connectors or real data.
- No simulated production.

## Backlog
- S25-01: contracts and read-model validation rules.
- S25-02: application service and deterministic synthetic source.
- S25-03: foundation API.
- S25-04: Angular read-only foundation page.
- S25-05: test and guardrail hardening.
- S25-06: local HTTP integration validation.
- S25-07: sprint closure and next capability selection.
