# CRM Sprint 15 S15-01 - Campaign Contracts and Domain Rules

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 15 P1 merge commit required
Branch: crm-sprint-15-s15-01-campaign-contracts-domain-rules
Suggested commit: feat(crm): add campaign contracts and domain rules
PR title: CRM Sprint 15 S15-01 - Campaign Contracts and Domain Rules

## Objective
Implement the authoritative Campaign foundation domain contracts and deterministic lifecycle rules only.

## Canonical contract
Fields:
- Id: CrmId
- Name: required, trimmed, max 160
- StartDate: DateOnly, required
- EndDate: DateOnly, required, EndDate >= StartDate
- Status: Draft | Active | Completed | Cancelled

Date semantics:
- DateOnly/calendar date
- no timezone conversion in domain
- no automatic status transition based on current date/time

Lifecycle:
- Create -> Draft
- Draft -> Active
- Draft -> Cancelled
- Active -> Completed
- Active -> Cancelled
- Completed and Cancelled terminal/read-only
- repeated same action should be deterministic and no-change where applicable

## Required domain artifacts
Use repository conventions analogous to recent Activity/Opportunity slices. Add under a CampaignManagement domain namespace:
- CampaignManagementOperation
- CampaignManagementCommand
- CampaignManagementSnapshot
- CampaignManagementErrorCode
- CampaignManagementRuleResult
- CampaignManagementPolicy
- CampaignStatus enum if no suitable existing enum exists

Safely extend or replace the thin conceptual Campaign record only as necessary to support the authoritative policy. Do not duplicate business rules in Application/API.

## Required scenarios
Cover with unit tests:
- valid create
- name required/trim/max length
- invalid date range
- update Draft changed/no-change
- update non-Draft rejected
- activate Draft
- repeat activate no-change if already Active
- complete Active
- repeat complete no-change if Completed
- cancel Draft
- cancel Active
- repeat cancel no-change if Cancelled
- activate Completed/Cancelled rejected
- complete Draft/Cancelled rejected
- terminal update rejected

## Architecture tests
Verify Domain remains independent from Application/Infrastructure/API/frontend and no persistence/network/auth dependencies are introduced.

## Explicitly deferred
- ICampaignManagementService / Application orchestration
- Foundation store
- API routes
- Angular UI
- Lead Campaign attribution mutation
- Opportunity attribution
- Segment management
- channel/source/type catalogs
- budget
- external marketing connectors

## Hard guardrails
ProductiveCampaignRouteEnabled: false
DeleteBehaviorAdded: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
EfRuntimeEnabled: false
MigrationsCreated: false
SchemaChangesDetected: false
RealDataDetected: false
ExternalConnectorRuntimeEnabled: false
SimulatedProductionTouched: false

Do not add `/api/crm/campaigns` or foundation Campaign routes in S15-01.
Do not add DELETE.
Do not activate Portal Auth/token/header runtime.
Do not activate Common DB/SQL/EF.
Do not touch `crm-prod-sim`.

## Validation
Run:
- git diff --check
- dotnet build CRM.sln
- unit tests
- architecture tests
- full .NET tests
- frontend build/test regression
- tools/check-crm-guardrails.ps1
- tools/verify-crm-foundation.ps1
- tools/verify-crm-sprint-15-p1.ps1
- create/run tools/verify-crm-sprint-15-s15-01.ps1

Record before/after test counts; never invent results.

## Documentation / handoff
Create:
- docs/roadmap/crm-sprint-15-s15-01-campaign-contracts-domain-rules.md
- codex/prompts/sprint-15-campaign-management-s15-02.md
- tools/verify-crm-sprint-15-s15-01.ps1

Update codex/next-task.md to:
CRM Sprint 15 S15-02 - Campaign Application Service and Foundation Store
Base: S15-01 merge commit required

Update codex/TASKS.md accordingly.

Do not auto-merge.
