# CRM Sprint 24 S24-06 - Assignment Reference Local Integration Validation

Status: Implemented

## Topology
- Backend: http://127.0.0.1:8101 (Development)
- Frontend: http://127.0.0.1:4214
- Frontend API routing: Angular proxy

## Evidence
- List/detail/create/update/no-change/archive/repeat archive/conflict: PASS.
- Invalid create: 400 PASS.
- Missing assignment: 404 PASS.
- Productive assignment routes unavailable.
- Foundation DELETE unavailable.
- Portal Identity/Security runtime not observed.
- Common DB, external connectors and real data not observed.
- Simulated production untouched.
- Latency samples: 11; average 25.09 ms; P95 92 ms.
