# CRM Sprint 17 S17-04 - Segment Frontend Foundation Page

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 17 S17-03 merge commit required
Branch: crm-sprint-17-s17-04-segment-frontend-foundation-page
Suggested commit: feat(crm): add segment frontend foundation page
PR title: CRM Sprint 17 S17-04 - Segment Frontend Foundation Page

## Objective
Add the Angular foundation-only Segment management page using the existing S17-03 API.

## Required UI
- Route `/foundation/segments`.
- List and select Segment records.
- Create and update Name/CriteriaSummary.
- Show Draft/Active/Inactive status.
- Activate and deactivate actions with duplicate-submit protection.
- Surface safe validation/not-found/conflict errors.
- Keep CriteriaSummary descriptive only; never execute it.

## Guardrails
- Use `/api/crm/foundation/segments` only.
- No productive route, DELETE, criteria execution, targeting, auto-classification, Portal/Common DB/real data/Production.

## Handoff
Prepare S17-05 Segment Test and Guardrail Hardening and run full backend/frontend regression before PR/merge.
