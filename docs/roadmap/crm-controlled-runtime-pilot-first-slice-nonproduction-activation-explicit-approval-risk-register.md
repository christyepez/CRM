# CRM Controlled Runtime Pilot First Slice NonProduction Activation Explicit Approval Risk Register

| Risk | Status | Mitigation |
| --- | --- | --- |
| Premature Portal runtime coupling | Open | Keep RuntimePortalCallsEnabled: false and require P27 dry-run execution plan. |
| Accidental production activation | Open | Keep ProductionActivationDecision: NoGo and CrmProductionReady: false. |
| Secret or private URL leakage | Open | Use logical placeholders only and run guardrail scans. |
| Common DB boundary breach | Open | Keep CommonDbRuntimeEnabled: false and PortalDatabaseDirectAccessEnabled: false. |

- FirstSliceNonProductionActivationExplicitApprovalP27ConditionsPrepared: true
- RuntimePortalCallsEnabled: false
- CommonDbRuntimeEnabled: false
- SecretsPresent: false
