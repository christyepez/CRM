# CRM NonProduction Activation Scaffold Validation Compose

Compose validation result expected for this sprint:

- PortalServicesInCrmCompose: false.
- CommonDbRuntimeEnabled: false.
- SharedPortalTablesAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
- PortalDatabaseDirectAccessEnabled: false.

CRM compose may define the CRM API service only. It must not add Portal services, a CRM-owned SQL Server service, or cross-domain database setup.

Marker: FirstSliceNonProductionActivationScaffoldValidationComposePrepared: true.
