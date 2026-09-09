# CRM Sprint 16 P1 - Account Management Functional Baseline

Sprint16P1BaseMainCommit: 90feb498ab4254abe9a3a2a755fb179035147f9e
SelectedSliceId: S16-ACCOUNT-MGMT
SelectedSlice: Account Management Foundation
Sprint16P1Decision: ReadyForS1601AccountContractsAndDomainRules
FirstImplementationStoryId: S16-01

## Existing repository evidence
- `Account` domain class already exists with Name, TaxId, Industry, Segment, Status and ContactReferences.
- `AccountStatus` already defines Draft, Active and Inactive.
- Generic foundation CRUD already exists through `FoundationAccountCrudService` and `IAccountFoundationStore`.
- Foundation routes already exist for GET list/detail, POST create and PUT update under `/api/crm/foundation/accounts`.
- Existing foundation persistence is `InMemoryAccountFoundationStore` with synthetic preview data.
- Existing Account CRUD uses free-form foundation contracts/status strings and does not yet enforce a dedicated Account domain policy.
- No dedicated Account Management Angular page is present.
- Productive `/api/crm/accounts` runtime remains unavailable.

## Minimum coherent foundation slice
Account fields:
- Id
- Name (required, trimmed, max 160)
- TaxId (optional, bounded, normalized)
- Industry (optional, bounded)
- Segment (optional, bounded)
- Status: Draft | Active | Inactive

Lifecycle:
- Create -> Draft
- Activate: Draft or Inactive -> Active
- Deactivate: Active -> Inactive
- Repeated Activate/Deactivate on the same target status is idempotent
- Profile update is allowed in Draft, Active and Inactive and must suppress no-change writes

Relationship boundary:
- Existing ContactReferences are inventory evidence only for Sprint 16 P1.
- Contact linking/unlinking is deferred until a dedicated relationship story is explicitly selected.
- Lead conversion and automatic Account creation remain deferred.

## Sprint 16 backlog
- S16-01 Account Contracts and Domain Rules
- S16-02 Account Application Service and Foundation Store Modernization
- S16-03 Account Foundation API Modernization
- S16-04 Account Management Frontend Foundation Page
- S16-05 Account Test and Guardrail Hardening
- S16-06 Account Local Integration Validation
- S16-07 Account Management Sprint Closure

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

No `/api/crm/accounts`, DELETE, Portal-owned identity, user assignment, Common DB, EF, migrations, SQL, real data, external connectors, `crm-prod-sim` or real Production changes are authorized by P1.
