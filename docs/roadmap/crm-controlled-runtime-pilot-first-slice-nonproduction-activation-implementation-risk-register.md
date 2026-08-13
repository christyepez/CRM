# CRM Controlled Runtime Pilot First Slice NonProduction Activation Implementation Risk Register

| Risk | Mitigation | Status |
| --- | --- | --- |
| Premature flag enablement | Future PR sequence keeps enablement last and approval gated | Controlled |
| Portal runtime coupling beyond first slice | Scope restricted to disabled client and planned limited activation | Controlled |
| Secret or private endpoint leakage | Logical placeholders only | Controlled |
| Rollback not ready before activation | Rollback plan is mandatory entry criterion | Controlled |
| Cross-domain persistence | No shared database, tables or migrations | Controlled |

## Marker

- FirstSliceNonProductionActivationImplementationRiskRegisterPrepared: true.
