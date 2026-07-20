# CRM Business Rules

## Sprint 1 P2 rules

- Lead can be qualified only while active.
- Opportunity can be marked won only while open.
- Marking an opportunity won sets probability to 100.
- Activity can be completed only when scheduled.
- Email, phone, person name, company name, money, probability and date range validate basic invariants.

## Not implemented

- No persistence.
- No production CRM CRUD.
- No customer conversion workflow.
- No Portal permission registration.
- No Financiero integration.
- No external CRM connectors.

## Risks

- Business rules are intentionally minimal and may change during domain discovery.
- Customer/account ownership and deduplication are deferred.
- Portal Security/Menu/Audit/Notification integration is planned but not active.
