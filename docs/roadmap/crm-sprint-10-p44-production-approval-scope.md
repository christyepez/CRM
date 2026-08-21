# P44 Production Approval Scope

| Component | Scope | Notes |
| --- | --- | --- |
| CRM API | Included | first-slice health/readiness/API shell only |
| Portal integration | Excluded | no Portal runtime calls |
| Common DB | Excluded | no DB runtime activation |
| Routes | Conditional | productive routes stay locked unless a future gate expands scope |
| Navigation | Excluded | no Portal navigation activation |
| Configuration | Included | approved manifest only, no secrets |
| Data changes | Excluded | no migrations, schema changes or writes |
| External dependencies | Excluded | no productive external calls |
| Monitoring | Included | required observation and alert plan |
| Deployment infrastructure | Conditional | P45 must revalidate target infrastructure |

PortalIncludedInProductionExecution: false
CommonDbIncludedInProductionExecution: false
ProductionDataChangesApproved: false
ApprovedProductionExternalDependencies: None

RuntimePortalCallsEnabled: false
PortalRoutesActivated: false
PortalNavigationActivated: false
CommonDbRuntimeEnabled: false
