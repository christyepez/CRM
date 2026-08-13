# CRM Controlled Runtime Pilot First Slice Design

## Design statement

The first future slice should prepare a disabled-by-default scaffold for CRM to Portal pilot readiness. It must not execute runtime calls, register routes, expose navigation or activate Common DB runtime.

## Boundaries

- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- ProductivePortalNavigationEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- PortalServicesInCrmCompose: false.
- CommonDbRuntimeEnabled: false.

## Markers

- FirstImplementationSliceDesignPrepared: true.
- FirstImplementationSliceDesignOnly: true.
- ConditionalFutureGoExecuted: false.
