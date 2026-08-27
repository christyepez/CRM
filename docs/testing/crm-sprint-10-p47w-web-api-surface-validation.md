# CRM P47W Web/API Surface Validation

ApplicationType: APIOnly
WebUISourcePresent: false
StaticFilesConfigured: false
SwaggerConfigured: false
SwaggerEnabledInSimulatedProduction: false
RootRouteConfigured: false
FrontendProjectPresent: true
FrontendProjectPath: frontend/crm-web
FrontendIncludedInCurrentProductionScope: false

Results:

| Endpoint | Status | Classification |
| --- | --- | --- |
| `/` | 404 | Expected for API-only slice |
| `/swagger` | 404 | Expected; Swagger not configured/enabled in Production |
| `/swagger/index.html` | 404 | Expected; Swagger not configured/enabled in Production |
| `/health` | 200 | Healthy |
| `/health/live` | 200 | Healthy |
| `/health/ready` | 200 | Healthy |
| `/api/crm/readiness` | 200 | ReadyForFoundationOnly |
| productive route dry-run probe | 423 | Locked as expected |

WebAccessStatus: ExpectedBehavior
CriticalProductionBlockers: 0
