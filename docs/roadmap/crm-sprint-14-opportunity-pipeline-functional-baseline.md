# CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline

Repository: christyepez/CRM

Task: CRM Sprint 14 P1 - Opportunity Pipeline Functional Baseline and Backlog

S1307PullRequest: #185

S1307MergeCommit: b1a92f84016136ce2462a74ea2377245aec367d0

Sprint14P1BaseMainCommit: b1a92f84016136ce2462a74ea2377245aec367d0

BaseMainCommit: b1a92f84016136ce2462a74ea2377245aec367d0

## Executive Summary

Sprint 14 starts with a foundation-only Opportunity Pipeline baseline. Sprint 11 Lead Qualification, Sprint 12 Contact Management and Sprint 13 Activity / Follow-Up are closed as synthetic foundation slices, so Opportunity Pipeline is the next coherent sales-flow capability. This P1 slice creates no runtime Opportunity API, no productive API, no UI behavior and no persistence. It records actual implementation evidence, defines terminology and lifecycle expectations, selects the first executable story and preserves all production boundaries.

SelectedSliceId: S14-OPPORTUNITY-PIPELINE

SelectedSliceName: Opportunity Pipeline Foundation

Sprint14P1Decision: ReadyForS1401OpportunityPipelineContractsAndDomainRules

FirstImplementationStoryId: S14-01

FirstImplementationStoryName: Opportunity Pipeline Contracts and Domain Rules

ProductiveOpportunityRouteEnabled: false

FoundationOpportunityRouteEnabledByP1: false

DeleteBehaviorAdded: false

LeadConversionImplemented: false

AccountManagementActivatedBySprint14P1: false

AssignmentRuntimeActivatedBySprint14P1: false

PortalRuntimeEnabled: false

CommonDbRuntimeEnabled: false

EfRuntimeEnabled: false

MigrationsCreated: false

SchemaChangesDetected: false

RealDataDetected: false

SimulatedProductionTouched: false

CrmProdSimTouched: false

## Actual Inventory

### Opportunity

- `src/CRM.Domain/Entities/Opportunity.cs`: contains a thin `Opportunity` entity with `Id`, `AccountName`, `ExpectedValue`, `Probability`, `Status` and `DomainEvents`.
- `src/CRM.Domain/Entities/Opportunity.cs`: `Opportunity.Create(...)` creates an `Open` opportunity.
- `src/CRM.Domain/Entities/Opportunity.cs`: `Opportunity.MarkWon(...)` is allowed only while status is `Open`, sets probability to `100` and raises `OpportunityWonDomainEvent`.
- `src/CRM.Domain/ValueObjects/BusinessValueObjects.cs`: `MoneyAmount.From(...)` rejects negative values and normalizes a 3-letter currency code.
- `src/CRM.Domain/ValueObjects/BusinessValueObjects.cs`: `Probability.From(...)` enforces `0..100`.
- `src/CRM.Application/Contracts/CrmDomainCatalogService.cs`: lists `Opportunity` with fields `Id`, `AccountName`, `ExpectedValue`, `Status` and behavior `MarkWon`.

OpportunityDomainStatus: ThinExistingEntity

OpportunityApplicationStatus: NotStarted

OpportunityPersistenceArchitecture: NotStarted; target FoundationOnly / NonProductionSeam

OpportunityFoundationStoreStatus: NotStarted

OpportunityApiStatus: NotStarted

OpportunityFrontendStatus: NotStarted

### Pipeline And PipelineStage

- `src/CRM.Domain/Entities/ConceptualEntities.cs`: contains conceptual records `Pipeline(CrmId Id, string Name, IReadOnlyCollection<PipelineStage> Stages)` and `PipelineStage(CrmId Id, string Name, int Order)`.
- `src/CRM.Application/Contracts/CrmDomainCatalogService.cs`: lists `Pipeline` and `PipelineStage` as draft catalog entities and the draft relationship `Pipeline -> PipelineStage`.
- `docs/domain/crm-domain-model.md`: documents Pipeline as a sales process and PipelineStage as an ordered stage.

PipelineDomainStatus: CatalogOnly

PipelineStageDomainStatus: CatalogOnly

PipelineManagementPolicyExists: false

PipelineStageTransitionModelExists: false

### Targeted Absence Verification

No `IOpportunityManagementService`, `OpportunityManagementService`, `IOpportunityFoundationStore`, `InMemoryOpportunityFoundationStore`, foundation Opportunity API route, productive Opportunity API route, Angular Opportunity route or Opportunity frontend service exists in targeted verification.

OpportunityManagementServiceExists: false

OpportunityFoundationStoreExists: false

FoundationOpportunityApiRouteExists: false

ProductiveOpportunityApiRouteExists: false

AngularOpportunityFoundationRouteExists: false

OpportunityFrontendServiceExists: false

## Dependency Summary

| Dependency | Evidence | Sprint 14 P1 decision |
| --- | --- | --- |
| Lead Qualification | Sprint 11 is closed successfully; foundation-only and synthetic. | Reference only through optional synthetic LeadId later; no Lead conversion. |
| Contact Management | Sprint 12 is closed successfully; foundation-only and synthetic. | Reference only through optional synthetic ContactId later. |
| Activity / Follow-Up | Sprint 13 is closed successfully; foundation route remains `/api/crm/foundation/activities`. | Relationship stays deferred until Opportunity foundation API/UI stories. |
| Account Management | Existing account foundation concepts exist, but Account Management activation is outside Sprint 14 P1. | Capture `AccountName` first; synthetic AccountId remains deferred. |
| Portal users / assignment | Portal Auth runtime and user context are not active. | No owner, salesperson, assignee or notification runtime. |

LeadQualificationClosed: true

ContactManagementClosed: true

ActivityFollowUpClosed: true

AllClosedDependenciesRemainFoundationOnly: true

## Foundation Terminology And Lifecycle Proposal

CanonicalOpportunityTerm: Opportunity

CanonicalPipelineTerm: Pipeline

CanonicalPipelineStageTerm: PipelineStage

PipelineStageOrderingDecision: Stages must have deterministic positive order values, unique within a pipeline.

PipelineDefaultStageProposal: Prospecting, Qualification, Proposal, Negotiation, Won, Lost

OpportunityLifecycleProposal: DraftOrOpen -> ProgressedThroughOrderedOpenStages -> WonOrLost

OpportunityWinRuleProposal: Only open opportunities can be won; win sets probability to 100 and emits a domain event.

OpportunityLossRuleProposal: Only open opportunities can be lost; loss should close the opportunity without DELETE.

OpportunityProgressRuleProposal: Progression must follow configured stage order unless an explicit foundation rule allows a skipped transition.

OpportunityUpdateRuleProposal: Updates may change account name, expected value, probability and stage while preserving terminal-state immutability.

TerminalStateMutationRuleProposal: Won and Lost opportunities cannot be edited or progressed in the first foundation story.

## Foundation Commands Proposed For S14

- Create Opportunity.
- Update Opportunity.
- Progress Opportunity Stage.
- Mark Opportunity Won.
- Mark Opportunity Lost.
- List and read foundation Opportunity snapshots in later stories.

No DELETE command is proposed.

## Relationship Strategy

Opportunity foundation starts with CRM-owned commercial fields and synthetic references only. `AccountName` remains the required low-dependency account signal because the existing entity already supports it. Optional future `LeadId`, `ContactId`, `AccountId` and `ActivityId` references must be structural string/CRM IDs against foundation stores only, with no real data, no Lead conversion, no Account Management activation and no Portal user dependency.

LeadRelationshipDecision: SyntheticReferenceOnlyLater

ContactRelationshipDecision: SyntheticReferenceOnlyLater

AccountRelationshipDecision: CaptureAccountNameNow; SyntheticAccountIdDeferred

ActivityRelationshipDecision: DeferredUntilFoundationApiOrUiStory

LeadConversionDecision: Deferred

## Portal Capability Classification

| Capability | Classification | Sprint 14 P1 decision |
| --- | --- | --- |
| Security/Auth | REUSE | Portal owns Auth; no runtime activation. |
| Permissions | EXTEND later | Future Opportunity permissions must extend Portal permission catalog. |
| Menu | EXTEND later | Future Opportunity page must be registered through Portal menu configuration. |
| Configuration | EXTEND later | Pipeline stages, grids and forms should become configurable. |
| Catalog | EXTEND later / CREATE CRM-specific | Pipeline terms may be CRM-specific while global catalogs remain Portal-owned. |
| Audit | ADAPT later | Won/lost/progress events can adapt to Portal Audit later. |
| Notification | ADAPT later | Assignment, reminders and win/loss notifications remain deferred. |
| Opportunity domain | CREATE | CRM owns deterministic Opportunity Pipeline domain rules. |

## Definition Of Ready For S14-01

| Readiness item | Status |
| --- | --- |
| S13-07 merge base confirmed at `b1a92f84016136ce2462a74ea2377245aec367d0` | PASS |
| Opportunity thin entity inventoried | PASS |
| Pipeline/PipelineStage conceptual evidence inventoried | PASS |
| No Opportunity service/store/API/UI found | PASS |
| Lead, Contact and Activity closure dependencies recorded | PASS |
| Productive Opportunity route remains unavailable | PASS |
| DELETE remains prohibited | PASS |
| Portal Auth runtime remains disabled | PASS |
| Common DB/EF/migrations/schema changes remain prohibited | PASS |
| First implementation story selected | PASS |

## S14-01 Definition Of Done Matrix

| DoD item | Required for S14-01 |
| --- | --- |
| Domain contracts | Opportunity Pipeline command/result/error/snapshot contracts exist. |
| Pipeline policy | Deterministic stage ordering and transition policy exists. |
| Lifecycle | Create, update, progress, win and loss rules are explicit. |
| Terminal states | Won/Lost state mutation is guarded. |
| Relationships | Synthetic reference contract is defined without Lead conversion or Account activation. |
| Tests | Unit and architecture tests cover policy and forbidden dependencies. |
| Runtime boundaries | No service/store/API/UI/productive route/DELETE/DB/Portal runtime activation. |

## Backlog Sequence

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

FirstImplementationStoryRationale: The repository has only a thin `Opportunity` entity plus conceptual `Pipeline` and `PipelineStage` records/catalog entries. There is no authoritative Opportunity Pipeline policy, stage transition model, application service, foundation store, API or UI. S14-01 must create deterministic domain contracts and rules before any runtime surface.

FirstImplementationPrompt: codex/prompts/sprint-14-opportunity-pipeline-s14-01.md

ExpectedArchitectureChanges: CRM.Domain OpportunityPipeline contracts and policy; optional Opportunity entity extension only when required by deterministic rules.

## Guardrails And Production Boundaries

- No productive Opportunity API activation.
- No `/api/crm/opportunities` route.
- No foundation Opportunity route in P1.
- No DELETE.
- No Lead conversion.
- No Account Management activation.
- No assignment, owner, salesperson, Portal user or notification runtime activation.
- No Portal Auth runtime, Authorization header reads, tokens or CRM-owned Identity/login.
- No Common DB runtime, EF runtime, migrations, schema changes or SQL scripts.
- No real data, secrets, `.env`, tokens, certificates or external CRM connectors.
- Do not deploy production.
- Do not reopen Sprint 10 production deployment gates.
- Do not touch `crm-prod-sim`.

## Residual Risks And Dependencies

| ID | Risk | Status | Mitigation |
| --- | --- | --- | --- |
| S14-R1 | Existing `Opportunity` lacks stage, loss and update semantics. | AcceptedForS14Planning | Implement deterministic domain contracts first in S14-01. |
| S14-R2 | Pipeline/PipelineStage are conceptual only and may be over-modeled too early. | Controlled | Keep first story domain-only and bounded to foundation policy. |
| S14-R3 | Account identity is not productized, so Opportunity relationships could force Account activation. | Deferred | Use `AccountName` and synthetic references only until a separate Account story is approved. |
| S14-R4 | Lead conversion is tempting after Lead Qualification closure. | Deferred | Explicitly prohibit conversion in S14 stories until a separate conversion baseline exists. |
| S14-R5 | Assignment and notifications need Portal Auth/user and Notification runtime. | Deferred | Keep owner/salesperson/reminder fields out until Portal runtime is approved. |

ResidualRisksRecorded: true

CriticalResidualRisks: 0

## P1 Outcome

Sprint 14 P1 creates a foundation-only planning baseline and backlog. It does not activate Opportunity runtime behavior. The next executable task is S14-01 Opportunity Pipeline Contracts and Domain Rules.

NextTaskUpdated: true

NextTaskPhase: CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules

NextTaskPromptFile: codex/prompts/sprint-14-opportunity-pipeline-s14-01.md

NextGate: CRM Sprint 14 S14-01 - Opportunity Pipeline Contracts and Domain Rules
