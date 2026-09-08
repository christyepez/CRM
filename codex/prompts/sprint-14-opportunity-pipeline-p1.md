# CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog

Repository:
https://github.com/christyepez/CRM

Task:
Start CRM Sprint 14 by creating the Opportunity Pipeline functional baseline and implementation backlog after Sprint 13 Activity / Follow-Up closure is merged.

Base:
S13-07 merge commit required.

Expected branch:
crm-sprint-14-p1-opportunity-pipeline-functional-baseline

Suggested commit:
docs(crm): baseline opportunity pipeline foundation sprint

PR title:
CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog

Prompt File:
codex/prompts/sprint-14-opportunity-pipeline-p1.md

## Objective

Create a foundation-only Sprint 14 Opportunity Pipeline baseline and backlog. Inventory the actual Opportunity/Pipeline/PipelineStage implementation evidence, choose the first implementation story from that evidence, and prepare the next executable prompt. This is a planning/baseline slice only; do not activate runtime Opportunity API or UI behavior yet.

## Mandatory Reading

Read only the task-relevant coordination and evidence files:

- `AGENTS.md`
- `README.md`
- `codex/COORDINADOR_SOLUCION.md`
- `codex/INSTRUCTIONS.md`
- `codex/ARCHITECTURE_RULES.md`
- `codex/PORTAL_INTEGRATION_CONTRACTS.md`
- `codex/TASKS.md`
- `docs/roadmap/crm-sprint-13-activity-follow-up-closure.md`
- `src/CRM.Domain/Entities/Opportunity.cs`
- `src/CRM.Domain/ValueObjects/BusinessValueObjects.cs`
- `src/CRM.Application/Contracts/CrmDomainCatalogService.cs`
- targeted `Opportunity`, `Pipeline` and `PipelineStage` references only where needed

Do not re-run broad repository inventory searches.

## Current Evidence To Verify

Opportunity / Pipeline:

- `src/CRM.Domain/Entities/Opportunity.cs` contains a thin `Opportunity` entity.
- `Opportunity` has `Id`, `AccountName`, `ExpectedValue`, `Probability`, `Status` and `DomainEvents`.
- `Opportunity.Create(...)` creates an `Open` opportunity.
- `Opportunity.MarkWon(...)` is allowed only while status is `Open`, sets probability to `100`, and raises `OpportunityWonDomainEvent`.
- `MoneyAmount.From(...)` rejects negative values and normalizes 3-letter currency.
- `Probability.From(...)` enforces `0..100`.
- `CrmDomainCatalogService` lists `Opportunity`, `Pipeline`, `PipelineStage`, the `Lead -> Opportunity`, `Pipeline -> PipelineStage` and `Opportunity -> Activity` draft relationships, and `OpportunityWonDomainEvent`.
- Pipeline and PipelineStage appear as catalog/documentation concepts only; no concrete domain entities were found in source.
- No `IOpportunityManagementService`, `OpportunityManagementService`, `IOpportunityFoundationStore`, `InMemoryOpportunityFoundationStore`, foundation Opportunity API route, productive Opportunity API route, Angular Opportunity route or Opportunity frontend service should exist before P1 unless newly discovered by targeted verification.

Closed foundation dependencies:

- Lead Qualification is closed from Sprint 11.
- Contact Management is closed from Sprint 12.
- Activity / Follow-Up is closed from Sprint 13.
- All three remain foundation-only and synthetic.

## Capability Decision

SelectedSliceId: S14-OPPORTUNITY-PIPELINE
SelectedSliceName: Opportunity Pipeline Foundation
DecisionRationale:
Lead Qualification, Contact Management and Activity / Follow-Up are closed; Opportunity already exists as a thin domain entity; Pipeline/PipelineStage are documented catalog concepts; Opportunity Pipeline is the most coherent sales-flow continuation after follow-up.

## Foundation-Only Scope

Define the Sprint 14 foundation baseline for:

- Opportunity identity and lifecycle.
- Pipeline and PipelineStage terminology.
- Stage ordering and transition rules.
- Opportunity create/update/progress/win/loss foundation commands.
- Relationship strategy to Lead, Contact, Account and Activity using synthetic references only.
- Foundation-only API and frontend backlog for later S14 stories.
- Tests and guardrails needed before local integration.

Keep this P1 slice documentation-only except task handoff files and verifiers if needed.

## Out Of Scope

- No productive Opportunity API activation.
- Do not activate productive Opportunity APIs.
- No `/api/crm/opportunities` route.
- No DELETE.
- Do not add DELETE.
- No Lead conversion implementation.
- Do not implement Lead conversion.
- No Account Management activation as part of Sprint 14 P1.
- Do not activate Account Management as part of Sprint 14 P1.
- No assignment, owner, salesperson, Portal user or notification runtime activation.
- No Portal Auth runtime.
- Do not activate Portal Auth runtime.
- No Common DB runtime.
- Do not activate Common DB runtime.
- No EF runtime.
- No migrations.
- No schema changes.
- No SQL scripts.
- No real data.
- No secrets, `.env`, tokens, certificates or external CRM connectors.
- Do not deploy production.
- Do not reopen Sprint 10 production deployment gates.
- Do not touch `crm-prod-sim`.

## Required P1 Deliverables

1. Create `docs/roadmap/crm-sprint-14-opportunity-pipeline-functional-baseline.md`.
2. Create `codex/prompts/sprint-14-opportunity-pipeline-s14-01.md`.
3. Update `codex/next-task.md` to point to S14-01.
4. Update `codex/TASKS.md` with Sprint 14 P1 evidence and next story.
5. Update `README.md` and `docs/coordination` minimally.
6. Add or update a narrow verifier for Sprint 14 P1 if the repo pattern requires it.

## Required Baseline Content

The Sprint 14 P1 baseline document must include:

- Executive summary.
- Actual Opportunity/Pipeline/PipelineStage inventory with file references.
- Lead, Contact and Activity dependency summary.
- Foundation terminology and lifecycle proposal.
- Relationship strategy.
- Portal capability classification: Security REUSE, permissions/menu/config EXTEND later, audit/notification ADAPT later, Opportunity domain CREATE.
- DoD/Definition of Ready matrix for S14-01.
- Backlog sequence.
- First implementation story selection.
- Guardrails and production boundaries.
- Residual risks and dependencies.

## Backlog Sequence To Propose

Use this default sequence unless targeted evidence proves a smaller first story is safer:

- S14-01 Opportunity Pipeline Contracts and Domain Rules.
- S14-02 Opportunity Application Service and Foundation Store.
- S14-03 Opportunity Foundation API.
- S14-04 Opportunity Pipeline Frontend Foundation Page.
- S14-05 Opportunity Pipeline Test and Guardrail Hardening.
- S14-06 Opportunity Pipeline Local Integration Validation.
- S14-07 Opportunity Pipeline Sprint Closure.

## First Implementation Story

FirstImplementationStoryId: S14-01
FirstImplementationStoryName: Opportunity Pipeline Contracts and Domain Rules
FirstImplementationStoryRationale:
The repo has only a thin `Opportunity` entity and catalog-level Pipeline/PipelineStage concepts. There is no authoritative Opportunity Pipeline policy, no stage transition model, no application service, no foundation store, no API and no UI. Therefore the first implementation story must create deterministic domain contracts and rules before any service, route or frontend work.

The S14-01 prompt must instruct Codex to implement foundation-only domain/contracts work and continue to prohibit productive API, DELETE, Portal Auth runtime, Common DB runtime, real data, EF, migrations, Account Management activation, Lead conversion, assignment and `crm-prod-sim` changes.

## Required Validations

Run before completion:

- `git diff --check`
- `dotnet build CRM.sln`
- `dotnet test tests/CRM.UnitTests/CRM.UnitTests.csproj --no-build`
- `dotnet test tests/CRM.ArchitectureTests/CRM.ArchitectureTests.csproj --no-build`
- `dotnet test CRM.sln --no-build`
- `npm run build` from `frontend/crm-web`
- `npm run test` from `frontend/crm-web`
- `tools/check-crm-guardrails.ps1`
- `tools/verify-crm-foundation.ps1`
- Sprint 14 P1 verifier if created

## Expected Output Format

Return:

```text
Agent:
Task:
Portal Capability Checked:
Reuse Classification:
Portal Components Reused:
Portal Components Extended:
New CRM Components Created:
Files Created:
Files Modified:
Tests Added:
Risks:
Next Step:
```
