# CRM Sprint 18 S18-06 - Case Local Integration Validation

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 18 S18-05 merge commit required
Branch: crm-sprint-18-s18-06-case-local-integration-validation
Suggested commit: test(crm): validate case local integration
PR title: CRM Sprint 18 S18-06 - Case Local Integration Validation

## Objective
Validate the Case Management foundation flow end-to-end through real local HTTP using the Angular development proxy and CRM API.

## Required scenarios
- Health and frontend `/foundation/cases` availability.
- Foundation Case list/detail/create/update/no-change.
- Start/resolve/close and repeated idempotent transitions.
- Validation 400, missing 404 and invalid transition 409.
- Read-after-write consistency.
- Productive `/api/crm/cases` unavailable.
- Foundation/productive DELETE unavailable.
- Capture latency samples and persisted JSON evidence.

## Guardrails
Synthetic local data only. No Portal runtime/token, Common DB/EF/SQL, real data, connectors, `crm-prod-sim`, port 8094 or Production.

## Handoff
Prepare S18-07 Sprint Closure only after local integration evidence passes.
