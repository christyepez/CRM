# CRM Sprint 15 S15-06 - Campaign Local Integration Validation

Repository: https://github.com/christyepez/CRM
Base Main Commit: S15-05 merge commit required
Branch: crm-sprint-15-s15-06-campaign-local-integration-validation
Suggested commit: test(crm): validate campaign local integration
PR title: CRM Sprint 15 S15-06 - Campaign Local Integration Validation

## Objective
Validate the real local foundation workflow Angular -> Campaign API -> Application -> Domain -> InMemoryCampaignFoundationStore and back to Angular.

## Required scenarios
- health/live/ready
- frontend `/foundation/campaigns`
- list/detail/create/update/no-change
- name trim and date range validation
- Draft -> Active -> Completed
- Draft/Active -> Cancelled
- repeat lifecycle actions idempotent where defined
- terminal update/action conflicts safe
- 400/404/409 behavior
- productive `/api/crm/campaigns` unavailable
- DELETE unavailable
- read-after-write consistency
- no Portal/Common DB/external connectors/real data
- persistence remains FoundationOnly / NonProductionSeam

Use synthetic data only. Do not touch `crm-prod-sim`. Run full backend/frontend regressions and all Sprint 15 verifiers. Prepare S15-07 Sprint Closure and update next-task. Do not auto-merge.
