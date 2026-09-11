# CRM Sprint 20 - Note Management Functional Baseline

Sprint20P1Decision: Defined
RecommendedSliceId: S20-NOTE
RuntimeStatus: PlanningOnly

## Repository evidence
- `Note` exists only as conceptual `Note(CrmId Id, CrmId RelatedEntityId, string Text)`.
- No dedicated Note domain policy, application service, store, API or Angular page exists.
- Note is CRM-owned content metadata; generic files, users, audit, notifications and configuration remain Portal-owned.

## Canonical P1 contract
- Id: CRM-generated GUID.
- RelatedEntityType: Customer | Contact | Lead | Opportunity | Case.
- RelatedEntityId: required non-empty GUID; structural reference only.
- Text: required, trimmed, max 4000 characters.
- Status: Active | Archived.

## Deterministic lifecycle
- Create starts `Active`.
- Update is allowed only while `Active`.
- Archive changes `Active -> Archived`.
- Repeated Archive on `Archived` is successful with `Changed=false`.
- Archived notes are otherwise read-only.
- No DELETE and no cross-entity mutation.
## Sprint 20 backlog
- S20-01: Note contracts and domain rules.
- S20-02: Application service + typed in-memory foundation store.
- S20-03: Foundation API list/detail/create/update/archive.
- S20-04: Angular foundation page.
- S20-05: Test and guardrail hardening.
- S20-06: Local HTTP integration validation.
- S20-07: Sprint closure and next capability selection.

## Ownership boundaries
PortalOwnedCapabilities: audit, notifications, files/content, users/roles, configuration, menus, auth.
CrossEntityMutationEnabled: false
ActivitySchedulingEnabled: false
ProductiveNoteRouteEnabled: false
DeleteBehaviorAdded: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataEnabled: false
ExternalConnectorsEnabled: false
SimulatedProductionTouched: false
Port8094Touched: false