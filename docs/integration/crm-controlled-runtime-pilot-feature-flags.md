# CRM Controlled Runtime Pilot Feature Flags

## Prepared flags

These flags are names only. They are not wired to runtime in P5.

| Flag | Default | Purpose |
| --- | --- | --- |
| Crm:ControlledRuntimePilot:Enabled | false | Future umbrella pilot flag. |
| Crm:ControlledRuntimePilot:PortalClientEnabled | false | Future Portal client trial gate. |
| Crm:ControlledRuntimePilot:HealthSmokeEnabled | false | Future health/smoke trial gate. |
| Crm:ControlledRuntimePilot:GatewayRoutesEnabled | false | Future Gateway route gate. |
| Crm:ControlledRuntimePilot:PortalNavigationEnabled | false | Future Portal navigation gate. |

## Markers

- ControlledRuntimePilotFeatureFlagsPrepared: true.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- ProductivePortalNavigationEnabled: false.
