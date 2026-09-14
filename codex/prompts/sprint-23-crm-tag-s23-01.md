# CRM Sprint 23 S23-01 - CRM Tag Contracts and Domain Rules

## Objective
Implement deterministic Foundation-only CRM Tag contracts and policy.

## Required
- Create/Update/Archive operations.
- Active/Archived statuses.
- Name required trimmed max 80.
- Description optional trimmed max 500.
- Structural tag assignment references only.
- RelatedEntityType: Customer, Contact, Lead, Opportunity, Case.
- RelatedEntityId required non-empty GUID.
- No-change update returns Changed=false.
- Repeat archive idempotent.
- Archived tags read-only.

## Guardrails
No API/frontend yet; no DELETE/productive routes; no related-entity mutation; no Portal users/roles/auth/config runtime; no Common DB/EF/SQL/real data/connectors/Production; never touch port 8094.
