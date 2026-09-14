# CRM Sprint 20 S20-06 - Note Local Integration Validation

## Objective
Validate the Note foundation slice end-to-end through a temporary local API and Angular proxy.

## Required
- Use isolated non-production ports only; do not use 8094.
- Validate health, frontend route and frontend-to-API proxy.
- Validate list/detail/create/update/no-change/archive/repeat archive.
- Validate 400, 404 and 409 behavior.
- Confirm productive Note routes and DELETE are unavailable.
- Persist JSON evidence with latency metrics and safety markers.

## Guardrails
Foundation-only, synthetic data only, no Portal runtime, Common DB/EF/SQL, real data, connectors, crm-prod-sim or Production.