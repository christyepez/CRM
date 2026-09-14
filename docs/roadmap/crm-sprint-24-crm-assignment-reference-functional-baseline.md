# CRM Sprint 24 - CRM Assignment Reference Functional Baseline

Status: Planned

## Scope
CRM owns assignment metadata/reference only. Portal Security remains authoritative for users, roles, identity and authorization.

## Contract
- Id: generated non-empty GUID.
- RelatedEntityType: Customer, Contact, Lead, Opportunity, Case.
- RelatedEntityId: required non-empty GUID structural reference.
- AssigneeReferenceId: required opaque reference, trimmed, max 200.
- AssignmentLabel: optional trimmed metadata, max 120.
- Status: Active or Archived.

## Lifecycle
- Create starts Active.
- Update allowed only while Active.
- Normalized no-change returns Changed=false with no persistence.
- Archive Active -> Archived.
- Repeated archive is idempotent with Changed=false.
- Archived assignments are read-only.

## Guardrails
- Foundation-only and synthetic data.
- No productive `/api/crm/assignments` routes.
- No DELETE.
- No Portal Security runtime, user lookup, role lookup or authorization mutation.
- No Common DB/EF/SQL, connectors, real data, port 8094 or Production.
- Assignment reference must not mutate the related CRM entity.

## Backlog
- S24-01: contracts and domain rules.
- S24-02: application service + foundation store.
- S24-03: foundation API.
- S24-04: Angular foundation page.
- S24-05: test/guardrail hardening.
- S24-06: local HTTP integration validation.
- S24-07: sprint closure and next capability selection.
