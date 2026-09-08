# CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules

Repository:
https://github.com/christyepez/CRM

Task:
Implement Opportunity Pipeline foundation-only domain contracts and deterministic rules.

Base Main Commit:
Sprint 14 P1 merge commit required.

Expected branch:
crm-sprint-14-s14-01-opportunity-pipeline-contracts-domain-rules

Suggested commit:
feat(crm): add opportunity pipeline contracts and domain rules

PR title:
CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules

## Objective

Create the first functional Opportunity Pipeline foundation increment by defining domain contracts and rules for Opportunity identity, lifecycle, ordered pipeline stages and create/update/progress/win/loss behavior.

## Mandatory Reading

Read only task-relevant files:

- `AGENTS.md`
- `README.md`
- `codex/COORDINADOR_SOLUCION.md`
- `codex/INSTRUCTIONS.md`
- `codex/ARCHITECTURE_RULES.md`
- `codex/PORTAL_INTEGRATION_CONTRACTS.md`
- `codex/TASKS.md`
- `docs/roadmap/crm-sprint-14-opportunity-pipeline-functional-baseline.md`
- `src/CRM.Domain/Entities/Opportunity.cs`
- `src/CRM.Domain/Entities/ConceptualEntities.cs`
- `src/CRM.Domain/ValueObjects/BusinessValueObjects.cs`
- `src/CRM.Application/Contracts/CrmDomainCatalogService.cs`
- targeted `Opportunity`, `Pipeline` and `PipelineStage` references only where needed

Do not re-run broad repository inventory searches.

## Scope

- Add Opportunity Pipeline command/result/error/snapshot contracts in `CRM.Domain`.
- Add deterministic Opportunity Pipeline policy.
- Reuse existing `Opportunity` entity where safe.
- Reuse `MoneyAmount` and `Probability` value objects.
- Define canonical terms: Opportunity, Pipeline and PipelineStage.
- Define ordered stage rules with unique positive stage order values.
- Define foundation create/update/progress/win/loss rule evaluation.
- Preserve `Opportunity.MarkWon(...)` semantics or extend only through explicit domain rules.
- Define loss behavior without DELETE.
- Define terminal state guards for Won and Lost opportunities.
- Define synthetic relationship contract for Lead, Contact, Account and Activity references without activating those dependencies.
- Add focused unit tests for domain rules.
- Add or update architecture guardrails if needed.
- Update documentation and the next S14 task handoff.

## Guardrails

- Do not add Opportunity application service yet.
- Do not add `IOpportunityManagementService`.
- Do not add `OpportunityManagementService`.
- Do not add foundation store yet.
- Do not add `IOpportunityFoundationStore`.
- Do not add `InMemoryOpportunityFoundationStore`.
- Do not add API routes yet.
- Do not add foundation Opportunity API routes in S14-01.
- Do not add Angular UI yet.
- Do not add productive `/api/crm/opportunities`.
- Do not add `/api/crm/foundation/opportunities`.
- Do not add DELETE.
- Do not implement Lead conversion.
- Do not activate Account Management.
- Do not add assignment, owner, salesperson, Portal user or notification runtime behavior.
- Do not activate Portal Auth runtime.
- Do not read Authorization headers or tokens.
- Do not activate Common DB runtime.
- Do not activate EF runtime, migrations, schema changes or SQL scripts.
- Do not add CRM-owned Identity/login.
- Do not touch simulated Production, real Production or `crm-prod-sim`.
- Do not touch `crm-prod-sim`.
- No secrets, `.env`, tokens, certificates, external CRM connectors or real data.

## Required Validation

- `git diff --check`
- `dotnet build CRM.sln`
- `dotnet test tests/CRM.UnitTests/CRM.UnitTests.csproj --no-build`
- `dotnet test tests/CRM.ArchitectureTests/CRM.ArchitectureTests.csproj --no-build`
- `dotnet test CRM.sln --no-build`
- `npm run build` from `frontend/crm-web`
- `npm run test` from `frontend/crm-web`
- `tools/check-crm-guardrails.ps1`
- `tools/verify-crm-foundation.ps1`
- `tools/verify-crm-sprint-14-p1.ps1`
- S14-01 verifier if created

## Expected Closure

- Opportunity Pipeline domain contracts and deterministic rules implemented.
- Focused tests added.
- No service, store, API route, UI, productive route or DELETE added.
- Lead conversion, Account Management, assignment, Portal Auth runtime, Common DB runtime, EF, migrations, schema changes, real data and `crm-prod-sim` remain untouched.
- Next Sprint 14 story prepared: S14-02 Opportunity Application Service and Foundation Store.
