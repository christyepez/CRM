# CRM Controlled Runtime Pilot First Slice NonProduction Activation Rollback

## Rollback plan

Rollback for future activation must disable feature flags, restore disabled clients, remove any temporary NonProduction-only configuration and verify the P14/P15 disabled-only checks again.

## Markers

- FirstSliceNonProductionActivationRollbackPrepared: true.
