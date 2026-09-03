# CRM Sprint 12 S12-03 - Contact Foundation API Integration

## Summary

S12-03 wires the existing foundation Contact POST and PUT routes to the S12-02 Contact application service while preserving foundation-only routing and keeping productive Contact routes unavailable.

ContactManagementImplementationStatus: ApiFoundationIntegrated

ContactManagementDomain: Implemented

ContactManagementApplicationService: Implemented

ContactManagementApi: FoundationIntegrated

ContactManagementFrontend: NotImplemented

ProductiveContactRouteEnabled: false

DeleteBehaviorAdded: false

LeadContactRuntimeImplemented: false

PortalRuntimeEnabled: false

CommonDbRuntimeEnabled: false

FoundationContactApiBackwardCompatible: true

MassAssignmentRisk: Controlled

PiiLoggingAdded: false

S1203Decision: Implemented

## Existing route inventory

| Method | Route | Final behavior |
| --- | --- | --- |
| POST | `/api/crm/foundation/contacts/preview` | unchanged preview service |
| GET | `/api/crm/foundation/contacts` | unchanged foundation CRUD read |
| GET | `/api/crm/foundation/contacts/{id}` | unchanged foundation CRUD detail |
| POST | `/api/crm/foundation/contacts` | wired to `IContactManagementService.CreateAsync` |
| PUT | `/api/crm/foundation/contacts/{id}` | wired to `IContactManagementService.UpdateAsync` |
| GET | `/api/crm/foundation/contacts/read-model-preview` | unchanged read model preview |

## Write path

CurrentContactCreateWritePath: `FoundationContactCrudService.CreateAsync -> IContactFoundationStore.SavePreviewAsync`

CurrentContactUpdateWritePath: `FoundationContactCrudService.UpdateAsync -> IContactFoundationStore.SavePreviewAsync`

New create path:

`HTTP DTO -> ContactManagementApiResponse.ToApplicationRequest -> IContactManagementService.CreateAsync -> ContactManagementPolicy -> IContactFoundationStore.SavePreviewAsync -> ContactManagementApiResponse`

New update path:

`HTTP DTO + route id -> ContactManagementApiResponse.ToApplicationRequest -> IContactManagementService.UpdateAsync -> IContactFoundationStore.GetPreviewByIdAsync -> ContactManagementPolicy -> conditional IContactFoundationStore.SavePreviewAsync -> ContactManagementApiResponse`

## DTO mapping

ContactCreateApiRequest: `FoundationContactCreateRequest`

ContactUpdateApiRequest: `FoundationContactUpdateRequest`

ContactApiResponse: `ContactManagementApiResponse`

ExplicitDtoMapping: true

Mapping:

- `FirstName` + `LastName` -> `Name`
- `Email` -> `Email`
- `Phone` -> `Phone`
- `Title` -> `Role`
- AccountId is not exposed by the current foundation API DTO.
- PreferredContactMethod defaults to `NotSpecified` because the existing foundation DTO has no preference field.

## HTTP status matrix

| Scenario | Application result | HTTP status | Response |
| --- | --- | --- | --- |
| Valid create | `None`, `Changed=true` | 200 | normalized foundation Contact response |
| Invalid create | deterministic validation error | 400 | safe error code/message |
| Valid update | `None`, `Changed=true` | 200 | normalized foundation Contact response |
| No-change update | `None`, `Changed=false` | 200 | safe success with `changed=false` |
| Not-found update | `ContactNotFound` | 404 | safe error code/message |
| Invalid update | deterministic validation error | 400 | safe error code/message |

CreateSuccessStatusCode: 200

InvalidCreateStatusCode: 400

UpdateSuccessStatusCode: 200

NoChangeUpdateStatusCode: 200

ContactNotFoundStatusCode: 404

InvalidUpdateStatusCode: 400

NoChangeUpdateHttpSuccess: true

## Read-after-write

ReadAfterCreate: Supported

ReadAfterUpdate: Supported

The existing foundation GET route reads the same `IContactFoundationStore` seam used by `IContactManagementService`.

## Security review

SecurityReview: PASS

- Productive `/api/crm/contacts` remains unavailable/locked.
- No DELETE endpoint was added.
- No Portal Auth runtime was activated.
- No Authorization header or token storage was added.
- No Common DB, schema, migration or SQL Server was added.
- API uses explicit request mapping and does not bind directly to domain entities.
- No Contact PII logging was added.

## S12-04 entry criteria

- Contact foundation POST/PUT routes are application-service backed.
- GET/list/detail foundation routes remain compatible.
- Deterministic validation maps to 400.
- Not-found update maps to 404.
- No-change update remains HTTP success.
- Productive Contact route negative tests pass.
- Frontend can consume foundation list/detail/create/update in S12-04.
