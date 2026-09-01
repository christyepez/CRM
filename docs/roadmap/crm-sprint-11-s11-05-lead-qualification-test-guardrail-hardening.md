# CRM Sprint 11 S11-05 - Lead Qualification Test and Guardrail Hardening

## Decision

S11-05 hardens the existing Lead Qualification foundation slice without expanding business scope.

S1104PullRequest: #147  
S1104MergeCommit: ce455ae181140ac1c42570673087dfa65d129f88  
S1105BaseMainCommit: ce455ae181140ac1c42570673087dfa65d129f88  
S1105Decision: ReadyForS1106LocalIntegrationValidation

## Lead Qualification coverage matrix

| Layer | Coverage | Evidence |
| --- | --- | --- |
| Domain | Qualify, disqualify, reason-required, Other reason, invalid decision, invalid transition, idempotent same-state and converted-state restrictions. | `LeadQualificationPolicyTests` |
| Application | Lead found, changed write once, not found zero writes, rejected zero writes, idempotent zero writes, cancellation and policy metadata propagation. | `LeadQualificationServiceTests` |
| API | Contract mapping, status-code mapping, response redaction/runtime flags, HTTP success/error/method/route behavior. | `LeadQualificationApiContractsTests`, `LeadQualificationApiEndpointTests` |
| Frontend | Foundation route, request/response semantics, enum parity, validation, loading state, duplicate-submit protection and safe error states. | `frontend/crm-web/tools/verify-crm-foundation.mjs` |
| Architecture | Foundation-only API route, productive route absence, policy not bypassed by API and no DB/Auth coupling. | `LeadQualificationArchitectureTests` |
| Security | No token storage, no Authorization header reads, no Portal Auth client, no unsafe HTML/DOM injection. | S11-05 verifier and frontend verifier |
| Guardrails | S11-01 through S11-04 verifiers chained; S11-05 verifier adds cross-layer checks. | `tools/verify-crm-sprint-11-s11-05.ps1` |

## Cross-layer scenario matrix

| Scenario | Domain result | Application write | HTTP status | Frontend state |
| --- | --- | --- | --- | --- |
| Qualify | `Allowed=true`, `Changed=true`, `CurrentStatus=Qualified` | One write | 200 | Success result; current status updated |
| Disqualify | `Allowed=true`, `Changed=true`, `CurrentStatus=Disqualified` | One write | 200 | Success result with disqualification reason |
| Idempotent qualify | `Allowed=true`, `Changed=false` | Zero writes | 200 | Informational success with `Changed=false` |
| Lead not found | `Allowed=false`, `LeadNotFound` | Zero writes | 404 | Safe "Lead not found" error |
| Invalid transition | `Allowed=false`, `InvalidTransition` | Zero writes | 409 | Safe "Transition not permitted" error |
| Validation failure | `Allowed=false`, validation error code | Zero writes | 400 | Safe bad-request/validation error |

## Contract consistency

Request semantics remain aligned:

- `Decision`
- `DisqualificationReason`
- `OtherReason`
- `Comment`

Response semantics remain aligned:

- `LeadId`
- `PreviousStatus`
- `CurrentStatus`
- `Decision`
- `DisqualificationReason`
- `Allowed`
- `Changed`
- `ErrorCode`
- `Message`
- foundation/runtime flags

Enum parity is guarded for `Qualify`, `Disqualify`, `InvalidContactInformation`, `Duplicate`, `NoInterest`, `OutOfTarget`, `Unreachable` and `Other`.

## Security and runtime guardrails

- Productive qualification route remains unavailable.
- Frontend calls only `/api/crm/foundation/leads/`.
- No Portal Auth runtime/client was added.
- No Authorization header or token storage behavior was added.
- No Common DB, SQL Server, DbContext, migration or schema behavior was added.
- No `innerHTML`, `bypassSecurityTrustHtml` or direct DOM injection is used by the Lead Qualification workflow.
- User-provided comment/reason values remain normal Angular text/form values.
- Test/sample data remains synthetic and foundation-only.

## Accessibility and responsive checks

The page includes explicit labels, keyboard-accessible form controls, disabled submit state during submission, `aria-live` result/error messaging and non-color-only status text. Responsive source checks verify the page keeps mobile/tablet-safe layout rules.

## Validation totals

Expected after S11-05:

- Unit tests: increased from 209 after S11-04.
- Architecture tests: expected to remain 98.
- Full tests: expected to increase by the S11-05 unit/API endpoint tests.
- Frontend build/test, guardrails, foundation verification and S11-05 verifier must pass before PR.

## Remaining risks

- S11-05 uses in-memory test host verification; full browser-to-API runtime validation is intentionally deferred.
- Angular workflow is still foundation-only and depends on the S11-03 foundation API contract.
- No productive API, persistence, Portal Auth or SimulatedProduction activation is approved.

## S11-06 entry criteria

- Domain, application, API and frontend tests are green.
- Cross-layer contracts are consistent.
- Productive routes remain locked/unavailable.
- Portal/Auth/Common DB guardrails pass.
- Local end-to-end validation can run without touching SimulatedProduction.

NextGate: CRM Sprint 11 S11-06 - Lead Qualification Local Integration Validation
