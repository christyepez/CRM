# CRM Sprint 16 S16-01 - Account Contracts and Domain Rules

S1601Decision: Implemented
Sprint16P1Base: 69fbe0e3d365059bb181d048d8936044a3cbe438

## Implemented
- Dedicated `AccountManagementOperation`, `AccountManagementCommand`, `AccountManagementSnapshot`, `AccountManagementErrorCode`, `AccountManagementRuleResult`, and `AccountManagementPolicy`.
- Name required, trimmed, max 160.
- TaxId optional, trimmed/upper-normalized, max 64.
- Industry optional, trimmed, max 120.
- Segment optional, trimmed, max 80.
- Create returns Draft.
- Profile update allowed in Draft/Active/Inactive with explicit no-change detection.
- Activate Draft/Inactive -> Active; repeat Active is idempotent.
- Deactivate Active -> Inactive; repeat Inactive is idempotent.
- Non-create operations require a valid GUID and matching existing snapshot.

## Validation
- 22 focused Account unit cases PASS.
- 3 focused Account architecture cases PASS.
- Domain remains independent of Application/Infrastructure/API/EF/HTTP.

## Guardrails
ProductiveAccountRouteEnabled: false
DeleteBehaviorAdded: false
LeadConversionEnabled: false
AutomaticAccountCreationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
ExternalConnectorRuntimeEnabled: false
RealDataEnabled: false
SimulatedProductionTouched: false

S16-02 will modernize the Application service and foundation store around these domain contracts without changing the API surface yet.
