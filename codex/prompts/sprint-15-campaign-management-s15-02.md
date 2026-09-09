# CRM Sprint 15 S15-02 - Campaign Application Service and Foundation Store

Repository: https://github.com/christyepez/CRM
Base Main Commit: S15-01 merge commit required
Branch: crm-sprint-15-s15-02-campaign-application-service-foundation-store
Suggested commit: feat(crm): add campaign application service and foundation store
PR title: CRM Sprint 15 S15-02 - Campaign Application Service and Foundation Store

## Objective
Implement Application orchestration and FoundationOnly in-memory persistence for Campaign Management without adding API or UI.

## Required artifacts
- ICampaignManagementService
- CampaignManagementService
- CampaignManagement application result/contracts as required by repository conventions
- ICampaignFoundationStore
- InMemoryCampaignFoundationStore with deterministic synthetic seed

## Required operations
- List / detail
- Create
- Update Draft
- Activate
- Complete
- Cancel

All mutation decisions must invoke CampaignManagementPolicy. Do not duplicate domain rules in Application.
No-change outcomes must write zero times. Invalid/not-found/lifecycle-rejected operations must write zero times.
Use CancellationToken throughout async methods.

## Persistence
Classification: FoundationOnly / NonProductionSeam
Synthetic data only.
No EF, DbContext, SQL, migrations, schema changes, connection strings or Common DB.

## Explicitly deferred
- Campaign API routes
- Angular Campaign UI
- Lead/Opportunity attribution mutation
- Segment/channel/type/budget management
- Portal Auth/user context
- external marketing connectors

## Guardrails
No `/api/crm/campaigns` productive route.
No foundation Campaign API in S15-02.
No DELETE.
No Portal token/header runtime.
No Common DB/EF/SQL.
Do not touch `crm-prod-sim`.

## Validation
Run build, unit, architecture, full .NET tests, frontend build/test regression, guardrails, foundation verification, Sprint 15 P1 verifier and S15-01 verifier. Add S15-02 verifier and tests for service/store write-count behavior.

## Handoff
Prepare S15-03 Campaign Foundation API prompt and update next-task to S15-03 using `S15-02 merge commit required` without inventing a SHA.
Do not auto-merge.
