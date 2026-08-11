# CRM Controlled Runtime Pilot Health Smoke Contract

## Purpose

Define safe checks for the next controlled pilot validation.

## Checks

- Verify P5 scaffold documents exist.
- Verify P5 guardrails pass.
- Verify CRM compose does not define Portal services or an owned SQL Server.
- Verify no production or real provider activation marker is enabled.
- Verify pilot smoke remains local, metadata-only and disabled.

## Markers

- ControlledRuntimePilotHealthSmokeContractPrepared: true.
- PortalServicesInCrmCompose: false.
- CommonDbRuntimeEnabled: false.
- RealNotificationProviderConfigured: false.
- RealObservabilityProviderConfigured: false.
