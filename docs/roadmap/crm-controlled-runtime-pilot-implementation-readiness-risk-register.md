# CRM Controlled Runtime Pilot Implementation Readiness Risk Register

| Risk | Readiness impact | Required control |
| --- | --- | --- |
| Runtime activation confused with readiness | High | Keep ReadinessReviewOnly true and ConditionalFutureGoExecuted false |
| Portal boundary drift | High | Preserve Portal ownership and reject duplication |
| Unsafe runtime configuration | High | Use logical placeholders only in repository content |
| Common DB activation before approval | High | Keep CommonDbRuntimeEnabled false |
| Missing rollback owner | Medium | Require rollback owner before later implementation PRs |

## Markers

- ImplementationReadinessResidualRisksPrepared: true.
- ReadinessReviewOnly: true.
- ProductionActivationDecision: NoGo.
