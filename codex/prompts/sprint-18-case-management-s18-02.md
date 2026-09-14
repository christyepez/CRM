# CRM Sprint 18 S18-02 - Case Application Service and Foundation Store

Repository: https://github.com/christyepez/CRM
Base Main Commit: Sprint 18 S18-01 merge commit required
Branch: crm-sprint-18-s18-02-case-application-service-foundation-store
Suggested commit: feat(crm): add case application service and foundation store
PR title: CRM Sprint 18 S18-02 - Case Application Service and Foundation Store

## Objective
Implement Case Management application orchestration and a deterministic in-memory foundation store over the S18-01 domain policy.

## Scope
- Add explicit Application contracts and `ICaseManagementService`.
- Add `ICaseFoundationStore` port and `InMemoryCaseFoundationStore`.
- Implement list/detail/create/update/start/resolve/close.
- Delegate all business decisions to `CaseManagementPolicy`.
- Suppress persistence for invalid, not-found and no-change results.
- Seed only synthetic Case records.

## Guardrails
- No Case API route or Angular page in S18-02.
- No `/api/crm/cases`, no `/api/crm/foundation/cases`, and no DELETE.
- No Customer creation/mutation, assignment runtime, SLA runtime or notifications runtime.
- No Portal Auth/users runtime, Common DB/EF/migrations/schema/SQL/real data.
- No external connectors, `crm-prod-sim`, port 8094 or real Production changes.

## Validation and handoff
Add S18-02 docs/verifier/tests, prepare S18-03 prompt, and run .NET/Angular/foundation/S18 verifiers before committing.
