# CRM Sprint 11 S11-02 - Lead Qualification Application Service

## Purpose

S11-02 implements the application orchestration for Lead Qualification Foundation. It reuses the S11-01 domain policy and contracts without exposing an API endpoint or activating productive runtime dependencies.

## Application flow

`LeadQualificationRequest` -> `LeadQualificationService` -> `ILeadFoundationStore` -> `LeadQualificationPolicy` -> optional foundation seam save -> `LeadQualificationResult`

## Service responsibilities

- Validate contract-level decision input.
- Locate the lead in the existing foundation/nonproduction seam.
- Return deterministic `LeadNotFound` when the lead is absent.
- Invoke `LeadQualificationPolicy` for validation and transition decisions.
- Persist only when the policy returns `Allowed=true` and `Changed=true`.
- Return foundation metadata showing that productive routes, Portal runtime and Common DB runtime are disabled.

## Repository seam

The service uses `ILeadFoundationStore`, backed in development by the existing in-memory foundation store. This remains `NonProductionSeam` only.

## Policy invocation

Business transition rules stay in `CRM.Domain.LeadQualification.LeadQualificationPolicy`. The application service does not duplicate transition matrices or create controller-level business logic.

## Idempotency and write behavior

| Scenario | Expected write count |
| --- | ---: |
| Valid changed transition | 1 |
| Lead not found | 0 |
| Validation failure | 0 |
| Rejected transition | 0 |
| Same-state idempotent request | 0 |

## Error behavior

The service returns safe deterministic errors using `LeadQualificationErrorCode`, including `LeadNotFound`, without stack traces, file paths, connection details or infrastructure implementation names.

## Security boundaries

No tokens, Authorization headers, claims, Portal Auth runtime, CRM Identity, login/logout or secret values are introduced.

## Persistence classification

`NonProductionSeam`. No Common DB runtime, EF Core runtime, migrations, SQL, schema, connection strings or durable production persistence are introduced.

## Out of scope

- API endpoint registration.
- Productive `/api/crm/leads` route unlock.
- Angular frontend.
- Portal Auth runtime.
- Common DB runtime.
- Docker or SimulatedProduction changes.

## S11-03 entry conditions

- `LeadQualificationService` is implemented.
- S11-01 policy remains authoritative.
- Foundation repository seam works.
- Unit and architecture tests are green.
- Guardrails and foundation verification pass.
- API contract is ready to map safely in a foundation-only endpoint.

