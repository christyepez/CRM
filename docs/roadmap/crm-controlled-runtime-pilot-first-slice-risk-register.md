# CRM Controlled Runtime Pilot First Slice Risk Register

| Risk | Mitigation |
| --- | --- |
| First slice grows beyond scaffold | Limit P14 to disabled-by-default structure |
| Runtime call is accidentally added | Guardrail rejects runtime Portal calls |
| Private configuration leaks into repository | Use logical placeholders only |
| Portal capability duplication appears | Keep Portal ownership checks in verifier |
| Common DB runtime is activated early | Keep CommonDbRuntimeEnabled false |

## Markers

- FirstSliceSecurityChecklistPrepared: true.
- FirstImplementationSliceDesignOnly: true.
- ProductionActivationDecision: NoGo.
