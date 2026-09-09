# CRM Sprint 15 S15-01 - Campaign Contracts and Domain Rules

Status: Implemented
BaseMainCommit: 8875629ecaac2fa4efc5356386dcb01d47ca487a
Branch: crm-sprint-15-s15-01-campaign-contracts-domain-rules
S1501Decision: Implemented
CampaignManagementDomain: Implemented
CampaignManagementPolicy: Implemented
CampaignApplicationService: NotImplemented
CampaignFoundationStore: NotImplemented
CampaignApi: NotImplemented
CampaignFrontend: NotImplemented

## Domain contract
- CampaignStatus: Draft, Active, Completed, Cancelled.
- Name: required, trimmed, max 160.
- StartDate/EndDate: DateOnly; both required; EndDate >= StartDate.
- Create results in Draft.
- Only Draft can be edited.
- Draft -> Active.
- Active -> Completed.
- Draft/Active -> Cancelled.
- Repeated Activate/Complete/Cancel is no-change when already in requested state.
- Completed/Cancelled remain terminal.
- No automatic date-driven lifecycle changes.

## Artifacts
- CampaignManagementOperation
- CampaignManagementCommand / CampaignManagementSnapshot
- CampaignManagementErrorCode
- CampaignManagementRuleResult
- CampaignManagementPolicy
- CampaignStatus
- CampaignManagementPolicyTests
- CampaignManagementArchitectureTests

## Guardrails
ProductiveCampaignRouteEnabled: false
FoundationCampaignRouteEnabled: false
DeleteBehaviorAdded: false
LeadAttributionRuntimeEnabled: false
OpportunityAttributionRuntimeEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
EfRuntimeEnabled: false
MigrationsCreated: false
SchemaChangesDetected: false
ExternalConnectorRuntimeEnabled: false
RealDataDetected: false
SimulatedProductionTouched: false

UnitTestsAfter: 409
ArchitectureTestsAfter: 122
NextTaskPhase: CRM Sprint 15 S15-02 - Campaign Application Service and Foundation Store
NextTaskPromptFile: codex/prompts/sprint-15-campaign-management-s15-02.md
