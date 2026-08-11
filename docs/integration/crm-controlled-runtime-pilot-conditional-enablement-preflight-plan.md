# CRM Controlled Runtime Pilot Conditional Enablement Preflight Plan

## Required future preflight

- Confirm P9 evidence is still valid.
- Confirm approval owners accepted the pilot window.
- Confirm placeholders are still placeholders in repository content.
- Confirm no real secret, token, certificate, private endpoint or real data is committed.
- Confirm docker compose has no Portal service and no CRM-owned SQL Server.
- Confirm runtime flags remain disabled until the approved implementation package.

## Markers

- ConditionalEnablementPreflightPlanPrepared: true.
- ConditionalFutureGoDefined: true.
- ConditionalFutureGoExecuted: false.
- PortalServicesInCrmCompose: false.
