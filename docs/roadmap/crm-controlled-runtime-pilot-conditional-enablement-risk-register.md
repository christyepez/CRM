# CRM Controlled Runtime Pilot Conditional Enablement Risk Register

| Risk | Impact | Mitigation |
| --- | --- | --- |
| Accidental runtime coupling | CRM could call Portal before approval | Disabled-by-default flags and guardrail scans |
| Private endpoint leakage | Sensitive topology could enter repository | Logical placeholders only |
| Secret exposure | Credentials could be logged or committed | No real secret values; only logical secret names |
| Portal capability duplication | CRM could reimplement Portal ownership | Explicit duplication guardrails |
| Common DB boundary breach | Cross-domain data coupling | No shared tables, migrations or direct Portal DB access |
| Premature production activation | Pilot confused with production readiness | Productization remains PreparationOnly and ProductionActivationDecision remains NoGo |

## Markers

- ControlledRuntimePilotConditionalEnablementDesignPrepared: true.
- ConditionalEnablementBlockersPrepared: true.
- ProductionActivationDecision: NoGo.
- CrmProductionReady: false.
