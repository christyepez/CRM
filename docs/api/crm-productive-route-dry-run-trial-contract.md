# CRM Productive Route Dry Run Trial Contract

Configuration flag:
- `Crm:RuntimeTrials:ProductiveRouteDryRunEnabled=false`

Status endpoint:
- `GET /api/crm/foundation/sprint-9/productive-route-dry-run-trial`
- Returns the static foundation status and gate evidence.

Probe endpoint:
- `POST /api/crm/foundation/sprint-9/productive-route-dry-run-trial/probe`
- Request metadata:
  - `route`
  - `method`
- Response metadata:
  - `ProductiveRouteDryRunAttempted`
  - `ProductiveRouteDryRunAllowed`
  - `ProductiveRouteDryRunDecisionReturned`
  - `ProductiveRouteDryRunStatusCode`
  - `ProductiveCrudEnabled`
  - `ProductiveDomainExecutionEnabled`
  - `ProductivePersistenceEnabled`
  - `DatabaseWriteAttempted`
  - `SideEffectsAllowed`
  - `DeleteEndpointsEnabled`
  - `DbRuntimeEnabled`
  - `EfRuntimeEnabled`
  - `MigrationsEnabled`
  - `SchemaChangeAllowed`
  - `AuthHeaderRead`
  - `TokenRead`
  - `TokenStored`
  - `AuthAttributeEnabled`

Default result:
- Status code: `423`
- No side effects.
- No productive route registration.
