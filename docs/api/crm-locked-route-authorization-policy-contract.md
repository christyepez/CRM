# CRM Locked Route Authorization Policy Contract

Foundation endpoint:

- `GET /api/crm/foundation/sprint-8/locked-route-authorization-policy-integration`

Default response includes:

- `status=LockedRouteAuthorizationPolicyIntegration`
- `lockedRouteAuthorizationPolicyIntegrationExists=true`
- `lockedRouteAuthorizationPolicyIntegrationApproved=true`
- `lockedRouteAuthorizationPolicyIntegrationEnabled=false`
- `authorizationPolicyEvaluated=false`
- `authorizationPolicyDecision=NotEvaluatedBecauseDisabled`
- `portalAuthMetadataUsed=true`
- `portalAuthRuntimeRequired=false`
- `portalAuthRuntimeConnected=false`
- `tokenReadAttempted=false`
- `headerReadAttempted=false`
- `authorizationHeaderReadAttempted=false`
- `portalHttpCallAttempted=false`
- `productiveRoutesRegisteredByDefault=false`
- `defaultNegativeRouteStatus=404`
- `lockedRoutesEnabledOnlyWithExplicitNonProductionFlag=true`
- `lockedRouteStatus=423`
- `productiveCrudEnabled=false`
- `productiveDomainExecutionEnabled=false`
- `productivePersistenceEnabled=false`
- `deleteEndpointsEnabled=false`
- `sideEffectsAllowed=false`
- `dbRuntimeEnabled=false`
- `efRuntimeEnabled=false`
- `nonProductionOnly=true`
- `failClosedByDefault=true`
- `nextGate=Sprint8P6Sprint8GateDecision`

Locked response metadata is safe and never returns tokens, headers, secrets, connection strings, or private Portal URLs.
