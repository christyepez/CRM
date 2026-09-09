# CRM Sprint 14 S14-06 - Opportunity Pipeline Local Integration Validation

Repository: https://github.com/christyepez/CRM
Base Main Commit: S14-05 merge commit required
Branch: crm-sprint-14-s14-06-opportunity-pipeline-local-integration-validation
Suggested commit: test(crm): validate opportunity pipeline local integration
PR title: CRM Sprint 14 S14-06 - Opportunity Pipeline Local Integration Validation

## Objective
Validate end-to-end locally: Angular `/foundation/opportunities` -> real HTTP -> Opportunity foundation API -> Application -> Domain -> InMemoryOpportunityFoundationStore -> Angular.

## Scenarios
- health/live/ready and frontend route
- list/detail/create/update/no-change
- progress exactly one stage and reject same/skip
- win/lose/cancel and repeat terminal idempotency
- terminal update/progress conflicts
- validation/not-found safety
- read-after-write/list consistency
- productive `/api/crm/opportunities` unavailable
- DELETE unavailable
- frontend uses only foundation API
- no Portal/token/Common DB/EF/SQL/real data
- synthetic data only and `crm-prod-sim` untouched
- capture local latency smoke, logs and cleanup

## Deliverables
Integration runner/evidence, `tools/verify-crm-sprint-14-s14-06.ps1`, S14-07 closure prompt, TASKS/next-task updates, full backend/frontend/guardrail regression.
