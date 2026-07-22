# CRM Persistence Strategy

Sprint 1 P4 designs future persistence without activating it.

## Decisions

- CRM will not create its own SQL Server container.
- CRM will not create migrations until database ownership is approved.
- CRM will use a dedicated logical database only after the shared local SQL strategy is approved.
- Lead, Account and Contact are future aggregate roots candidates.
- Account/customer master-data ownership must be clarified before production activation.

## First persistence candidates

1. Account.
2. Contact.
3. Lead.

Opportunity, Activity, Campaign and Segment remain conceptual until later sprints.

## GO criteria

- Shared local SQL and environment strategy approved.
- Portal Security and Audit adapters approved.
- Migration ownership and naming accepted.
- No duplicate customer master-data decision accepted.

## NO-GO criteria

- Any need for a CRM-owned SQL Server container.
- Missing Portal authorization/audit boundary.
- Unclear Account vs Customer ownership with Financiero.
- Unreviewed schema/migration strategy.
