# CRM Sprint 15 P1 - Campaign Management Functional Baseline

Repository: christyepez/CRM
Task: CRM Sprint 15 P1 - Campaign Management Functional Baseline and Backlog
Sprint15P1BaseMainCommit: e0bad427f0cb482bd784399345b6f45e4f230d9d
SelectedSliceId: S15-CAMPAIGN
SelectedSliceName: Campaign Management Foundation
Sprint15P1Decision: ReadyForS1501CampaignContractsAndDomainRules
FirstImplementationStoryId: S15-01
FirstImplementationStoryName: Campaign Contracts and Domain Rules

## Actual inventory
- `Campaign` currently exists only as `Campaign(CrmId Id, string Name, DateRange ActiveRange)` in `ConceptualEntities.cs`.
- `DateRange` is an existing value object with `DateOnly Start`, `DateOnly End` and the invariant `End >= Start`.
- `Lead` already contains optional `CampaignId`, but there is no Campaign attribution workflow, service, store, API or UI.
- `CrmDomainCatalogService` lists Campaign as a conceptual/read-contract item only.
- No Campaign status enum, management policy, Application service, Foundation store, API surface or Angular route is currently active.

CampaignDomainStatus: ConceptualRecordOnly
CampaignApplicationStatus: NotStarted
CampaignPersistenceStatus: NotStarted
CampaignApiStatus: NotStarted
CampaignFrontendStatus: NotStarted
LeadCampaignAttributionStatus: StructuralFieldOnly

## Canonical foundation contract
Campaign fields for the first foundation slice:
- Id: CrmId
- Name: required, trim, maximum 160 characters
- StartDate: DateOnly, required
- EndDate: DateOnly, required, must be greater than or equal to StartDate
- Status: Draft | Active | Completed | Cancelled

DateSemantics: CalendarDateOnly
AutomaticDateDrivenTransitions: false
ChannelCatalogIncluded: false
CampaignTypeIncluded: false
AudienceSegmentIncluded: false
BudgetIncluded: false
LeadAttributionIncluded: false
OpportunityAttributionIncluded: false
ExternalMarketingConnectorIncluded: false

## Lifecycle
- Create -> Draft
- Draft -> Active
- Draft -> Cancelled
- Active -> Completed
- Active -> Cancelled
- Repeating the same terminal/action request should be idempotent where appropriate and return Changed=false.
- Completed and Cancelled are terminal and read-only in the first foundation slice.
- Dates never mutate Status automatically; activation/completion/cancellation are explicit commands.

## Foundation operations
- Create Campaign
- Update Draft Campaign
- Activate Campaign
- Complete Campaign
- Cancel Campaign
- List/read Campaign in later stories
- No DELETE

## Dependency decisions
| Dependency | Decision |
| --- | --- |
| Lead CampaignId | Existing structural field only; attribution workflow deferred. |
| Opportunity | No attribution in Sprint 15 foundation. |
| Segment | Deferred; conceptual only. |
| Channel/source/type catalogs | Deferred; avoid premature catalog ownership. |
| Portal Auth/permissions/menu | REUSE/EXTEND later; no runtime activation. |
| Common DB | Deferred; FoundationOnly / NonProductionSeam. |
| External marketing platforms | Deferred; no Meta/Salesforce/Dynamics connector activation. |

## Sprint 15 backlog
- S15-01 Campaign Contracts and Domain Rules
- S15-02 Campaign Application Service and Foundation Store
- S15-03 Campaign Foundation API
- S15-04 Campaign Management Frontend Foundation Page
- S15-05 Campaign Test and Guardrail Hardening
- S15-06 Campaign Local Integration Validation
- S15-07 Campaign Management Sprint Closure

## Definition of Ready for S15-01
- Sprint 14 closure merge base confirmed: PASS
- Existing Campaign concept inventoried: PASS
- DateRange invariant inventoried: PASS
- Existing Lead.CampaignId recorded without activating attribution: PASS
- Canonical fields/lifecycle selected: PASS
- First implementation story selected: PASS

## Guardrails
ProductiveCampaignRouteEnabled: false
FoundationCampaignRouteEnabledByP1: false
DeleteBehaviorAdded: false
LeadAttributionRuntimeEnabled: false
OpportunityAttributionRuntimeEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
EfRuntimeEnabled: false
MigrationsCreated: false
SchemaChangesDetected: false
RealDataDetected: false
ExternalConnectorRuntimeEnabled: false
SimulatedProductionTouched: false
CrmProdSimTouched: false

## Validation state
No runtime code is changed by P1. The last canonical executable regression before this planning slice is Sprint 14 closure: 383 Unit + 120 Architecture = 503 tests PASS, frontend build/test PASS, guardrails PASS. P1 does not claim a new executable test run while the connected execution machine is unavailable.

FirstImplementationPrompt: codex/prompts/sprint-15-campaign-management-s15-01.md
NextGate: CRM Sprint 15 S15-01 - Campaign Contracts and Domain Rules
