# CRM Sprint 16 P1 - Account Management Functional Baseline and Backlog

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 15 S15-07 merge commit required
Branch: crm-sprint-16-p1-account-management-functional-baseline
Suggested commit: docs(crm): define account management functional baseline
PR title: CRM Sprint 16 P1 - Account Management Functional Baseline and Backlog

## Objective
Establish the foundation-only Account Management baseline and an implementation backlog using the existing Account domain evidence without activating Productive, Portal, Common DB or real data.

## Required inventory
- inspect `Account` domain entity, value objects, enums, existing foundation routes/stores and Contact/Opportunity references
- identify existing Account API/UI/runtime behavior and all deferred relationships
- define the smallest useful Account foundation slice
- define lifecycle/status rules, validation rules and explicit deferred behaviors
- produce S16-01..S16-07 story backlog and acceptance criteria

## Guardrails
- foundation-only synthetic data
- no Lead conversion or automatic Account creation
- no Productive `/api/crm/accounts`
- no DELETE
- no Portal Auth/token/user assignment runtime
- no Common DB/EF/migrations/schema/SQL/real data
- no external connectors
- no `crm-prod-sim` or real Production changes
