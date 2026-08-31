# CRM Sprint 11 S11-01 - Lead Qualification Contracts and Domain Rules

## Purpose

S11-01 implements the first functional CRM Sprint 11 building block for `S11-LEAD-QUAL`: explicit lead qualification contracts and deterministic domain rules.

## Domain model

Qualification is modeled as a domain policy over the existing `LeadStatus` model. No parallel Lead aggregate, persistence model, controller, database schema or productive route is introduced.

## Qualification states

- `New`
- `Contacted`
- `Qualified`
- `Disqualified`
- `Converted`

## Disqualification reasons

- `InvalidContactInformation`
- `Duplicate`
- `NoInterest`
- `OutOfTarget`
- `Unreachable`
- `Other`

`Other` requires a bounded explanation.

## Transition matrix

| CurrentState | Decision | Allowed | NewState | Changed | ReasonRequired | ErrorIfRejected |
| --- | --- | --- | --- | --- | --- | --- |
| New | Qualify | Yes | Qualified | true | No | None |
| Contacted | Qualify | Yes | Qualified | true | No | None |
| Qualified | Qualify | Yes | Qualified | false | No | None |
| Disqualified | Qualify | No | Disqualified | false | No | InvalidTransition |
| Converted | Qualify | No | Converted | false | No | InvalidTransition |
| New | Disqualify | Yes | Disqualified | true | Yes | None |
| Contacted | Disqualify | Yes | Disqualified | true | Yes | None |
| Qualified | Disqualify | Yes | Disqualified | true | Yes | None |
| Disqualified | Disqualify | Yes | Disqualified | false | Yes | None |
| Converted | Disqualify | No | Converted | false | Yes | InvalidTransition |

## Validation rules

- Lead id is required.
- Qualification decision must be a defined enum value.
- Disqualification reason is required for `Disqualify`.
- Disqualification reason is not allowed for `Qualify`.
- `Other` requires an explanation.
- Other reason explanation is bounded to 250 characters.
- Comment is bounded to 500 characters.

## Idempotency

Repeating a same-state qualification request returns `Allowed=true` and `Changed=false`; it must not imply a future write or duplicate side effect.

## Error classifications

- `LeadIdRequired`
- `InvalidQualificationDecision`
- `InvalidTransition`
- `DisqualificationReasonRequired`
- `DisqualificationReasonNotAllowed`
- `OtherReasonExplanationRequired`
- `OtherReasonExplanationTooLong`
- `CommentTooLong`

## Security boundaries

Contracts do not carry tokens, raw claims, Authorization headers, connection strings, database metadata or environment-specific secret values.

## Portal/Common DB boundaries

- `PortalDependency`: none.
- `CommonDbDependency`: none.
- Portal runtime remains disabled.
- Common DB runtime remains disabled.
- Productive `/api/crm/leads` remains locked/not registered.

## Out of scope

- Application service implementation.
- API endpoint implementation.
- Angular frontend implementation.
- Portal Auth runtime.
- Common DB runtime, EF Core, migrations or schema.
- Docker changes.
- Simulated Production interaction.

## S11-02 entry criteria

- Contracts stable.
- Domain rules tested.
- Transition matrix documented.
- Architecture tests green.
- Guardrails pass.
- Productive routes still locked.

