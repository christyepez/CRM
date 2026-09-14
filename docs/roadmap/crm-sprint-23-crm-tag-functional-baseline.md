# CRM Sprint 23 - CRM Tag Functional Baseline

Status: Defined
P1Decision: ApprovedForFoundationImplementation

## Ownership
CRM owns tag metadata and structural tag-to-entity references.
Portal continues to own authentication, users, roles, generic configuration, audit, notifications and file management.

## Tag contract
- Id: foundation-generated non-empty GUID.
- Name: required, trimmed, max 80.
- Description: optional, trimmed, max 500.
- Status: Active or Archived.

## Assignment contract
- TagId: required structural reference.
- RelatedEntityType: Customer, Contact, Lead, Opportunity, Case.
- RelatedEntityId: required non-empty GUID structural reference.
- Assignment does not mutate the related entity.

## Lifecycle
- Create starts Active.
- Update only Active.
- Normalized no-change update returns Changed=false.
- Archive Active -> Archived.
- Repeated Archive returns Changed=false.
- Archived tags are read-only.
- No physical DELETE.

## Routes planned
- API: /api/crm/foundation/tags
- Angular: /foundation/tags
- Productive /api/crm/tags remains unavailable.

## Exclusions
No Portal users/roles/auth runtime; no generic Portal configuration/catalog duplication; no audit/notification runtime; no Common DB/EF/SQL; no real data/connectors; no crm-prod-sim, port 8094 or Production.

## Backlog
- S23-01 contracts/domain rules.
- S23-02 application service + synthetic foundation store.
- S23-03 foundation API.
- S23-04 Angular foundation page.
- S23-05 test/guardrail hardening.
- S23-06 local HTTP integration validation.
- S23-07 closure and next capability selection.
