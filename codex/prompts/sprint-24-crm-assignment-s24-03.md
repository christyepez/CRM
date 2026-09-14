# Sprint 24 S24-03 - CRM Assignment Reference Foundation API

Implement GET list/detail, POST create, PUT update and POST archive under `/api/crm/foundation/assignments`.
Map 400/404/409 safely. Keep `/api/crm/assignments` unavailable and DELETE unavailable.
Expose explicit safety flags: FoundationMode=true, ProductiveCrudEnabled=false, PortalIdentityRuntimeEnabled=false, CrossEntityMutationEnabled=false.
Do not resolve assignee references through Portal Security.
Add Unit/Architecture tests and hand off to S24-04 frontend.