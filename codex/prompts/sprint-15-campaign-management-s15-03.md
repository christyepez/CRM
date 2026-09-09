# CRM Sprint 15 S15-03 - Campaign Foundation API

Repository: https://github.com/christyepez/CRM
Base Main Commit: S15-02 merge commit required
Branch: crm-sprint-15-s15-03-campaign-foundation-api
Suggested commit: feat(crm): add campaign foundation api
PR title: CRM Sprint 15 S15-03 - Campaign Foundation API

## Objective
Expose Campaign Management through foundation-only HTTP routes using ICampaignManagementService.

## Required routes
GET /api/crm/foundation/campaigns
GET /api/crm/foundation/campaigns/{id}
POST /api/crm/foundation/campaigns
PUT /api/crm/foundation/campaigns/{id}
POST /api/crm/foundation/campaigns/{id}/activate
POST /api/crm/foundation/campaigns/{id}/complete
POST /api/crm/foundation/campaigns/{id}/cancel

Use explicit DTOs and safe status mapping. Route-id is authoritative. No DELETE.
No productive /api/crm/campaigns route.
No Portal Auth/token runtime. No Common DB/EF/SQL. No external connectors.
Register ICampaignManagementService and ICampaignFoundationStore via DI.

## Validation
Add endpoint tests and architecture guardrails. Run full backend/frontend regressions, guardrails, Foundation, P1/S15-01/S15-02/S15-03 verifiers.

## Handoff
Prepare S15-04 Campaign Frontend Foundation Page prompt. Update next-task to S15-04 using S15-03 merge commit required. Do not auto-merge.