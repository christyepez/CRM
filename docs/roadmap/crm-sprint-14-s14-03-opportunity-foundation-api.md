# CRM Sprint 14 S14-03 - Opportunity Foundation API

S1402PullRequest: #191
S1402MergeCommit: 680db4b65b07e826385abf4acb74a2acaa114217
S1403BaseMainCommit: 680db4b65b07e826385abf4acb74a2acaa114217

## Decision
S1403Decision: Implemented
OpportunityFoundationApi: Implemented
OpportunityApiRoutesAdded: true
ProductiveOpportunityRouteEnabled: false
DeleteBehaviorAdded: false

## Routes
- GET `/api/crm/foundation/opportunities`
- GET `/api/crm/foundation/opportunities/{id}`
- POST `/api/crm/foundation/opportunities`
- PUT `/api/crm/foundation/opportunities/{id}`
- POST `/api/crm/foundation/opportunities/{id}/progress`
- POST `/api/crm/foundation/opportunities/{id}/win`
- POST `/api/crm/foundation/opportunities/{id}/lose`
- POST `/api/crm/foundation/opportunities/{id}/cancel`

## Contracts and status mapping
The API uses explicit `FoundationOpportunity*Request` DTOs and maps only through `IOpportunityManagementService`. Domain entities are never bound directly. Success and no-change return 200, validation returns 400, and missing Opportunity returns 404. Null stage collections are converted to an empty collection and rejected safely by the domain policy rather than leaking exceptions.

## Boundaries
PersistenceClassification: FoundationOnly / NonProductionSeam
MassAssignmentRisk: Controlled
PiiLoggingAdded: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
SchemaChangesDetected: false
SimulatedProductionTouched: false

No productive `/api/crm/opportunities` routes were added. No DELETE, Lead conversion, Account Management runtime, assignment/owner runtime, Portal Auth, Common DB, EF runtime, migration, schema, SQL or real data was activated.

## Next task
NextTaskPhase: CRM Sprint 14 S14-04 - Opportunity Pipeline Frontend Foundation Page
