# CRM Portal Consumer Contract Alignment Risk Register

| Risk | Impact | Mitigation | Status |
| --- | --- | --- | --- |
| CRM duplicates Portal Auth or permissions | Security model fork | Consume Portal contracts only | Controlled |
| Productive navigation is exposed too early | User-visible broken module | Keep navigation contract-only | Controlled |
| Portal private URL leaks into CRM docs/config | Information disclosure | Use placeholders only | Controlled |
| Portal Sprint 21 contract gaps remain | Runtime design rework | Track known gaps and defer to P4 | Open |
| Common DB and Portal contract get coupled | Domain boundary breach | Keep DB activation and Portal consumer contracts separated | Controlled |

## Markers

- PortalRuntimeCouplingEnabled: false.
- ProductivePortalNavigationEnabled: false.
- ProductivePortalGatewayRoutesEnabled: false.
- RealPortalPrivateUrlsPresent: false.
- PortalServicesInCrmCompose: false.
- ProductionActivationDecision: NoGo.
