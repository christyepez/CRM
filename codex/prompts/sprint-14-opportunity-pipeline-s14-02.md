# CRM Sprint 14 S14-02 - Opportunity Application Service and Foundation Store

Repository: https://github.com/christyepez/CRM
Task: Implement the foundation-only Opportunity application service and in-memory foundation store.
Base Main Commit: Sprint 14 S14-01 merge commit required
Branch: crm-sprint-14-s14-02-opportunity-application-service-foundation-store
Suggested commit: feat(crm): add opportunity application service and foundation store
PR title: CRM Sprint 14 S14-02 - Opportunity Application Service and Foundation Store

## Objective
Add Application orchestration and a synthetic foundation persistence seam around `OpportunityPipelinePolicy`, preserving deterministic domain ownership and zero production coupling.

## Scope
- Add `IOpportunityManagementService` and `OpportunityManagementService` in Application.
- Add `IOpportunityFoundationStore` abstraction in Application and `InMemoryOpportunityFoundationStore` in Infrastructure.
- Support read/list/create/update/progress/win/lose/cancel application operations.
- Invoke `OpportunityPipelinePolicy`; do not duplicate domain validation in Application.
- Suppress writes for invalid and no-change operations.
- Support CancellationToken throughout.
- Use synthetic foundation references only; no real Lead/Contact/Account/Activity lookup outside existing foundation seams unless strictly required.
- Add focused unit and architecture tests.
- Prepare S14-03 Opportunity Foundation API prompt.

## Guardrails
- No Opportunity API routes in S14-02.
- No Angular Opportunity UI.
- No productive `/api/crm/opportunities`.
- No DELETE.
- No Lead conversion or Account Management activation.
- No assignment/owner/Portal user runtime.
- No Portal Auth runtime or Authorization/token reads.
- No Common DB, EF, migrations, schema or SQL.
- No real data/secrets/external CRM connectors.
- Do not touch `crm-prod-sim` or real Production.

## Validation
Run build, unit/architecture/full tests, frontend build/test regressions, guardrails, foundation verifier, S14-P1 verifier, S14-01 verifier and S14-02 verifier.
