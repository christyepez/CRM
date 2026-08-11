# CRM Controlled Runtime Pilot Conditional Implementation Change Matrix

| Change type | Future status |
| --- | --- |
| Documentation and validation tooling | Allowed |
| Disabled-by-default adapter wiring | Future PR only |
| Real Portal calls | Prohibited until explicit Go |
| Productive Gateway routes | Prohibited |
| Productive navigation | Prohibited |
| Common DB runtime | Prohibited until separate approval |
| Shared Portal tables or migrations | Prohibited |
| Real secrets, tokens, certificates or endpoints | Prohibited |

## Markers

- ConditionalImplementationChangeMatrixPrepared: true.
- ImplementationPlanOnly: true.
- RuntimePortalCouplingEnabled: false.
- RuntimePortalCallsEnabled: false.
