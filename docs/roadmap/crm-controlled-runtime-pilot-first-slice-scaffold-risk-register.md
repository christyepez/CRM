# CRM Controlled Runtime Pilot First Slice Scaffold Risk Register

| Risk | Impact | Mitigation |
| --- | --- | --- |
| Scaffold mistaken for activation | Runtime coupling could be enabled prematurely | Keep feature flags false and status explicit |
| Hidden external call | Portal or secret provider could be contacted | Disabled client has no HTTP dependency |
| Boundary drift | CRM could duplicate Portal capabilities | Guardrails scan Auth/Menu/Permissions/Audit/Notification/Configuration duplication |

## Markers

- FirstSliceScaffoldTestEvidencePrepared: true.
- FirstSliceScaffoldRollbackPrepared: true.
