# CRM Controlled Runtime Pilot Conditional Implementation Risk Register

| Risk | Impact | Mitigation |
| --- | --- | --- |
| Implementation plan mistaken for execution approval | Runtime could be enabled too early | P11 keeps ImplementationPlanOnly true |
| Unsafe configuration | Private endpoints or secrets could be committed | Logical placeholders only; config supplied out of repo |
| Gateway/navigation drift | Productive routes could appear before Portal approval | Separate future PR and explicit Portal governance |
| Common DB boundary breach | Shared schema or Portal DB access | No cross-domain migrations or direct Portal database access |
| Portal duplication | CRM could reimplement cross-cutting capabilities | Duplication guardrails and explicit ownership review |

## Markers

- ConditionalImplementationPlanPrepared: true.
- ConditionalFutureGoExecuted: false.
- ProductionActivationDecision: NoGo.
