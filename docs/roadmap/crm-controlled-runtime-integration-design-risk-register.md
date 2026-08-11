# CRM Controlled Runtime Integration Design Risk Register

| Risk | Impact | Mitigation | Status |
| --- | --- | --- | --- |
| Runtime coupling is enabled before approval | Uncontrolled dependency on Portal | Keep all runtime flags disabled and add guardrails | Controlled |
| Productive navigation appears in Portal | User-visible dead routes | Keep navigation contract-only until pilot scaffold | Controlled |
| Real Portal URL or secret leaks | Security disclosure | Use placeholders only and scan changed files | Controlled |
| Common DB gets tied to Portal DB internals | Domain boundary breach | Keep CRM DB and Portal DB boundaries separate | Controlled |
| Observability emits sensitive metadata | Privacy/security issue | Metadata-only and redacted payloads | Open |

## Markers

- RuntimePortalCouplingEnabled: false.
- ProductivePortalNavigationEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- CommonDbRuntimeEnabled: false.
- ProductionActivationDecision: NoGo.
