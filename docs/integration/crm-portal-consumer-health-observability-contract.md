# CRM Portal Consumer Health and Observability Contract

CRM will expose safe status metadata for future Portal consumer runtime validation. P3 does not configure a real observability provider.

## Expected status dimensions

- CRM API health.
- CRM frontend build status.
- Common DB runtime status.
- Portal consumer contract status.
- productive route lock status.
- sanitized correlation metadata.

## Markers

- CrmPortalHealthObservabilityContractPrepared: true.
- RealObservabilityProviderConfigured: false.
- PortalRuntimeCouplingEnabled: false.
- ProductizationStatus: PreparationOnly.
