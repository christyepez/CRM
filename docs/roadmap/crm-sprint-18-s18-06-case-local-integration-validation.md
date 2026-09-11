# CRM Sprint 18 S18-06 - Case Local Integration Validation

S1806Decision: Passed
CaseLocalIntegrationStatus: Complete
FrontendApiRoutingMode: Proxy
LocalBackendUrl: http://127.0.0.1:8095
LocalFrontendUrl: http://127.0.0.1:4208

## Executed scenarios
- Backend health and frontend route: PASS.
- List/detail/create/update/no-change: PASS.
- Validation 400, missing 404, invalid transition 409: PASS.
- Start/resolve/close plus repeated idempotent transitions: PASS.
- Read-after-write consistency: PASS.
- Productive Case routes unavailable: PASS.
- Foundation/productive DELETE unavailable: PASS.

## Performance evidence
- Samples: 17.
- Minimum: 1 ms.
- Average: 26.24 ms.
- P95: 110 ms.
- Evidence file: `crm-sprint-18-s18-06-case-local-integration-result.json`.

## Safety
ProductiveCaseRouteEnabled: false
DeleteBehaviorAdded: false
PortalRuntimeEnabled: false
CommonDbRuntimeEnabled: false
RealDataDetected: false
SimulatedProductionTouched: false
