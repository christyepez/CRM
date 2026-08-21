# P42 Pilot Lessons Learned

WorkedWell:
- Explicit approval and drift gates prevented accidental production or scope expansion.
- Docker Compose health/smoke checks provided fast runtime confidence.
- Negative productive-route checks confirmed safe-by-default behavior.
- Portal-first guardrails kept CRM from duplicating Portal capabilities.

UnnecessarilyComplex:
- Many sequential approval documents added governance weight for a small runtime slice.
- Several guardrail scripts overlap and should be consolidated before production operations.

UsefulGuardrails:
- Production NoGo markers.
- Portal/Common DB disabled markers.
- Productive route 404 and locked probe 423 checks.
- Secret/private content scans.

RisksDetected:
- Observability remains basic and should mature before production.
- Portal and Common DB runtime were intentionally not activated and remain separate readiness gaps.

AutomationOpportunities:
- Generate evidence bundles from commands automatically.
- Consolidate guardrail runners into one sprint-aware verifier.
- Add structured health and monitoring export.

MissingEvidence:
- No performance/load test baseline.
- No production deployment strategy execution.
- No advanced APM/dashboard/alert evidence.
- No production support/on-call model with named owners.

ImproveBeforeProduction:
- Add production-grade observability and alerting.
- Define production secrets/config injection.
- Validate Portal and Common DB production integration paths.
- Complete performance, resilience, backup and incident-response exercises.
