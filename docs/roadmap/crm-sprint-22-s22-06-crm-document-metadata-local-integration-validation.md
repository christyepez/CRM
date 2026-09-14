# CRM Sprint 22 S22-06 - Document Metadata Local Integration Validation

Status: Implemented
S2206Decision: Implemented

## Local runtime
- API: http://127.0.0.1:8099
- Angular: http://127.0.0.1:4212
- Routing mode: Angular proxy

## Scenarios
- Health: PASS
- Frontend route: PASS
- Frontend to Document API: PASS
- Create/read/update/no-change: PASS
- Invalid create 400: PASS
- Missing detail 404: PASS
- Archive/repeat archive: PASS
- Archived update 409: PASS
- Productive document route unavailable: PASS
- Foundation DELETE unavailable: PASS

## Safety
BinaryStorageObserved: false
PortalContentRuntimeObserved: false
CommonDbRuntimeObserved: false
ExternalConnectorRuntimeObserved: false
RealDataDetected: false
SimulatedProductionTouched: false
