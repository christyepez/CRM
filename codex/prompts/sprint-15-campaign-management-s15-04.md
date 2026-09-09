# CRM Sprint 15 S15-04 - Campaign Frontend Foundation Page

Repository: https://github.com/christyepez/CRM
Base Main Commit: S15-03 merge commit required
Branch: crm-sprint-15-s15-04-campaign-frontend-foundation-page
Suggested commit: feat(crm): add campaign frontend foundation page
PR title: CRM Sprint 15 S15-04 - Campaign Frontend Foundation Page

## Objective
Add a business-facing Angular foundation page at `/foundation/campaigns` consuming only the S15-03 foundation Campaign API.

## Required UX
- list/select/detail Campaigns
- create Draft Campaign
- edit Draft Campaign only
- activate Draft
- complete Active
- cancel Draft or Active
- Completed/Cancelled read-only
- fields: Name, StartDate, EndDate, Status
- loading, empty, success, validation, not-found and lifecycle-conflict states
- duplicate-submit protection
- accessible labels, keyboard-friendly controls, responsive layout

## Guardrails
Use only `/api/crm/foundation/campaigns`. No productive route, DELETE, Portal Auth/token storage, Common DB, external marketing connector, Lead/Opportunity attribution mutation or simulated Production changes.

## Validation
Run frontend build/test, backend regression, guardrails, Foundation and Sprint 15 verifiers. Add S15-04 verifier and prepare S15-05 hardening prompt.
