# CRM Sprint 19 P1 - Interaction Management Functional Baseline

S19P1Decision: Defined
CapabilityOwner: CRM
FoundationOnly: true

## Boundary
Interaction records an occurred CRM touchpoint. Activity remains the planned/follow-up work capability; Interaction must not duplicate Activity scheduling or completion behavior.
Related CRM identifiers are structural references only and never mutate the related entity.

## Canonical contract
- Id: foundation-generated GUID.
- RelatedEntityType: Customer, Contact, Lead, Opportunity or Case.
- RelatedEntityId: required GUID structural reference.
- Channel: Email, Phone, Meeting, Chat or Other.
- Direction: Inbound or Outbound.
- Subject: required, trimmed, max 160.
- Summary: required, trimmed, max 2000.
- OccurredAtUtc: required UTC timestamp, not in the future.
- Status: Recorded or Voided.

## Lifecycle
- Create starts Recorded.
- Recorded interactions may be corrected with Update; no-change update succeeds with Changed=false.
- Void transitions Recorded -> Voided; repeated Void is idempotent Changed=false.
- Voided interactions are read-only.
- No DELETE.

## Portal First
UsersRolesOwnership: Portal REUSE/EXTEND
AuditOwnership: Portal ADAPT
NotificationOwnership: Portal ADAPT
FilesOwnership: Portal ADAPT
ConfigurationOwnership: Portal EXTEND
InteractionDomainOwnership: CRM CREATE
No Portal runtime is activated in Sprint 19.

## Routes and persistence seam
Foundation route family: `/api/crm/foundation/interactions`.
Frontend route: `/foundation/interactions`.
Persistence: deterministic in-memory foundation store only.
Productive `/api/crm/interactions` remains unavailable.

## Backlog
- S19-01 Interaction contracts and domain policy.
- S19-02 Application service and typed in-memory foundation store.
- S19-03 Foundation API with explicit transport DTOs.
- S19-04 Angular foundation page.
- S19-05 tests and cross-layer guardrail hardening.
- S19-06 real local HTTP integration validation.
- S19-07 sprint closure and next bounded capability selection.

## Guardrails
ProductiveInteractionRouteEnabled: false
DeleteBehaviorAdded: false
ActivitySchedulingDuplicated: false
CrossEntityMutationEnabled: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
ExternalConnectorRuntimeEnabled: false
RealDataEnabled: false
SimulatedProductionTouched: false
Port8094Touched: false
