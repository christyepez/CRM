# CRM Portal Adapter Contracts

CRM Sprint 1 P5 defines future adapter contracts for PortalCorporativo without enabling runtime integration.

## Status

- Module: CRM
- Integration mode: Planned
- Runtime mode: NonProduction
- Connected: false
- Capability owner: PortalCorporativo

## Ports

CRM declares Application ports for user context, tenant context, permissions, menu registration, audit publishing, notification publishing, configuration and correlation id propagation. These ports are marked as `FuturePortalAdapter`.

## What CRM does not implement

CRM does not implement login, identity, roles, permissions, menu, audit, notification, configuration, gateway, Portal shell, credential storage or Portal HTTP calls.

## Future integration requirement

Real adapters must be added only after PortalCorporativo publishes stable consumer contracts, environments and security rules.
