# CRM Controlled Runtime Pilot Preflight Checklist

## Required preflight

- Run inherited CRM guardrails.
- Run Portal consumer contract alignment verification.
- Run controlled runtime integration design verification.
- Run P5 scaffold guardrails.
- Run P5 scaffold verification.
- Validate Docker compose config without introducing Portal services or CRM-owned SQL Server.

## Markers

- ControlledRuntimePilotPreflightPrepared: true.
- PortalServicesInCrmCompose: false.
- CommonDbRuntimeEnabled: false.
- RealCommonDbConnectionConfigured: false.
- SharedPortalTablesAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
- PortalDatabaseDirectAccessEnabled: false.
