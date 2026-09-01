# CRM Sprint 11 S11-06 - Lead Qualification Local Integration Validation

## Summary

S11-06 validated the Lead Qualification foundation workflow locally between the Angular foundation page and the CRM API foundation endpoints.

The validation used only Development/NonProduction local processes, synthetic foundation leads and foundation routes. It did not touch `crm-prod-sim`, Docker simulated Production, Common DB runtime, Portal Auth runtime, productive CRM routes or real data.

## Base

- S11-05 PR: #149
- S11-05 merge commit: `1f376d3eaeffcef61dc498f4e35f1a4b72cc260c`
- S11-06 base main commit: `1f376d3eaeffcef61dc498f4e35f1a4b72cc260c`
- Branch: `crm-sprint-11-s11-06-lead-qualification-local-integration-validation`

## Local Runtime

- Backend URL: `http://localhost:8093`
- Frontend URL: `http://127.0.0.1:4200`
- Frontend route: `/foundation/leads/qualification`
- Frontend API routing mode: same-origin `/api` proxy to CRM API
- Foundation lead source route: `GET /api/crm/foundation/leads`
- Foundation qualification route: `POST /api/crm/foundation/leads/{leadId}/qualification`

## Local Integration Result

- Backend `/health`: PASS, HTTP 200
- Backend `/health/live`: PASS, HTTP 200
- Backend `/health/ready`: PASS, HTTP 200
- Frontend route `/foundation/leads/qualification`: PASS, HTTP 200
- Frontend-to-API proxy `/api/crm/foundation/leads`: PASS, HTTP 200
- Existing synthetic lead: `lead-preview-001`
- Existing lead final status: `Qualified`
- Synthetic leads created through foundation seam only.
- Latency samples: 19
- Minimum latency: 2.08 ms
- Average latency: 15.41 ms
- P95 latency: 64.92 ms

## Scenarios Validated

| Scenario | Route | Expected | Result |
| --- | --- | ---: | --- |
| Health | `GET /health` | 200 | PASS |
| Live | `GET /health/live` | 200 | PASS |
| Ready | `GET /health/ready` | 200 | PASS |
| Frontend page | `GET /foundation/leads/qualification` | 200 | PASS |
| Lead source | `GET /api/crm/foundation/leads` | 200 | PASS |
| Qualify | `POST /api/crm/foundation/leads/lead-preview-001/qualification` | 200 | PASS |
| Idempotent qualify | `POST /api/crm/foundation/leads/lead-preview-001/qualification` | 200 | PASS |
| Disqualify | `POST /api/crm/foundation/leads/{syntheticId}/qualification` | 200 | PASS |
| Other reason | `POST /api/crm/foundation/leads/{syntheticId}/qualification` | 200 | PASS |
| Validation error | `POST /api/crm/foundation/leads/{syntheticId}/qualification` | 400 | PASS |
| Lead not found | `POST /api/crm/foundation/leads/s11-06-missing/qualification` | 404 | PASS |
| Invalid transition | `POST /api/crm/foundation/leads/{syntheticId}/qualification` | 409 | PASS |
| Productive route negative | `POST /api/crm/leads/lead-preview-001/qualification` | 404 | PASS |
| Read after write | `GET /api/crm/foundation/leads/lead-preview-001` | 200 | PASS |

## Local Integration Adjustment

The Angular build is valid, but `ng serve` is not reliable in this OneDrive/sandbox workspace because the dev-server attempts to inspect parent directories and fails with access denied. S11-06 therefore adds a small dependency-free local integration server that:

- Serves the built Angular artifact from `frontend/crm-web/dist/crm-web/browser`.
- Preserves SPA fallback to `index.html`.
- Proxies `/api/*` to `http://localhost:8093`.
- Keeps local execution explicit through `npm start`.

This is a local integration utility only; it does not change CRM runtime behavior.

## Guardrails

- SimulatedProductionTouched: false
- DockerChanged: false
- `crm-prod-sim` restart/redeploy/rollback: not executed
- ProductiveQualificationRouteAvailable: false
- Productive route status: 404
- PortalRuntimeObserved: false
- PortalAuthClientAdded: false
- AuthorizationHeaderRequired: false
- TokenRuntimeObserved: false
- CommonDbRuntimeObserved: false
- CommonDbReadAttempted: false
- CommonDbWriteAttempted: false
- SchemaChangesDetected: false
- SecretsAdded: false
- RealDataDetected: false

## Evidence Commands

```powershell
$env:ASPNETCORE_URLS='http://localhost:8093'
$env:ASPNETCORE_ENVIRONMENT='Development'
dotnet run --project src\CRM.Api\CRM.Api.csproj --no-build

cd frontend\crm-web
npm run build
npm start

powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools\run-crm-sprint-11-s11-06-local-integration.ps1
```

## Decision

S11-06 is ready for release after repository validations pass.

Next gate: CRM Sprint 11 S11-07 - Lead Qualification Sprint Closure.
