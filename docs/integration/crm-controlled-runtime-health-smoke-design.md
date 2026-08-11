# CRM Controlled Runtime Health and Smoke Design

The future pilot should validate only safe readiness signals before any productive behavior.

## Future health checks

- CRM API health.
- CRM readiness endpoint.
- Portal contract metadata availability.
- Common DB metadata readiness without a real connection by default.
- Productive route lock status.

## Future smoke flow

1. Call CRM health.
2. Call CRM foundation status.
3. Validate Portal consumer contract status.
4. Validate locked routes stay locked or absent.
5. Validate no DELETE and no side effects.

## Markers

- ControlledRuntimeHealthSmokeDesignPrepared: true.
- RuntimePortalCouplingEnabled: false.
- CommonDbRuntimeEnabled: false.
