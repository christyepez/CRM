# CRM Controlled Runtime Pilot Implementation Readiness Verification Plan

## Verification before touching runtime

- Run full P2-P12 aggregate scripts.
- Run build, tests and compose config validation.
- Scan for secrets, tokens, certificates, private endpoints, real environment files and real connection strings.
- Scan compose for Portal services and CRM-owned SQL Server.
- Confirm runtime Auth, DB and migration markers are absent from review-only changes.

## Markers

- ImplementationReadinessVerificationPlanPrepared: true.
- ReadinessReviewOnly: true.
- ConditionalFutureGoExecuted: false.
