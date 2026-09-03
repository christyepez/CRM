# CRM Sprint 12 - Contact Management Foundation Roadmap

## Goal

Deliver Contact Management Foundation as the next CRM business capability after Sprint 11 Lead Qualification closure.

## Current baseline

- Sprint 10 local simulated Production pilot: closed.
- Sprint 11 Lead Qualification Foundation: closed successfully.
- Contact domain: partially implemented.
- Contact API: foundation CRUD exists.
- Contact frontend: no dedicated page yet.
- Persistence: Foundation/NonProduction seam only.
- Productive routes: locked/unavailable by default.
- Portal Auth runtime: disabled.
- Common DB runtime: disabled.
- Real Production: deferred.

## Functional scope

- Contact identity/details.
- Create/update contact foundation workflow.
- Contact list/search foundation workflow.
- Contact detail foundation workflow.
- Contact preference validation.
- Angular foundation page.
- Synthetic/foundation persistence.
- Tests and local integration.

## Out of scope

- Productive `/api/crm/contacts` routes.
- DELETE.
- Real DB persistence.
- EF runtime, migrations or schema changes.
- Portal Auth runtime.
- CRM-owned Identity/login.
- Lead conversion.
- Account Management dependency.
- Deduplication, MDM, consent, marketing automation and bulk import/export.
- SimulatedProduction or real Production activation.

## Architecture

- Domain rules belong in `CRM.Domain`.
- Application orchestration belongs in `CRM.Application`.
- Foundation persistence remains behind existing foundation store ports.
- Infrastructure stays in-memory/foundation-only.
- API remains under `/api/crm/foundation`.
- Angular remains development/foundation-only.

## Stories

| StoryId | Title | FunctionalScope | TechnicalScope | Dependencies | Complexity |
| --- | --- | --- | --- | --- | --- |
| S12-01 | Contact Contracts and Domain Rules | Explicit Contact behavior and validation | Domain contracts, policy, result/error model, tests | S12 P1 | M |
| S12-02 | Contact Application Service | Contact create/update/list/detail orchestration | Application service over foundation seam | S12-01 | M |
| S12-03 | Contact Foundation API | Deterministic foundation endpoints/contracts | API request/response mappings and tests | S12-02 | M |
| S12-04 | Contact Foundation Angular Page | User-visible contact workflow | Angular route/service/page with safe forms | S12-03 | M |
| S12-05 | Contact Test and Guardrail Hardening | Cross-layer quality | Unit/API/architecture/frontend guardrails | S12-04 | M |
| S12-06 | Contact Local Integration Validation | Local end-to-end validation | backend/frontend local smoke | S12-05 | M |
| S12-07 | Contact Management Sprint Closure | Close slice and choose next business capability | closure docs/verifier/handoff | S12-06 | S |

## Milestones

- M1 Domain/Contract Ready: S12-01.
- M2 Application Ready: S12-02.
- M3 API Ready: S12-03.
- M4 UI Ready: S12-04.
- M5 Quality Hardened: S12-05.
- M6 Local Integration Validated: S12-06.
- M7 Sprint Closed: S12-07.

## Dependencies

- PortalDependency: none for Sprint 12 foundation.
- CommonDbDependency: none for Sprint 12 foundation.
- AccountRelationshipRequiredForFoundation: false.
- LeadContactDecision: ContractOnlyLater.

## Security

- Contact information is PII-like even when synthetic.
- No PII logging.
- No secrets or `.env`.
- No token/header reads.
- No unsafe DOM injection.
- No mass assignment.
- Safe errors only.

## Testing

- Domain tests for Contact policy.
- Application service tests.
- API contract/endpoint tests.
- Architecture tests preserving productive route locks and boundaries.
- Frontend build/tests.
- Local integration smoke.

## Exit criteria

- Contact Management Foundation workflow implemented and locally validated.
- Productive routes remain locked/unavailable.
- Portal/Common DB remain disabled.
- SimulatedProduction untouched.
- Real Production remains deferred.
- No critical blockers.

## Future opportunities

- Lead-to-contact conversion.
- Account relationship workflow.
- Opportunity creation from qualified lead/contact.
- Activity/follow-up workflow.
- Real persistence under explicit Common DB plan.
- Portal Auth integration under explicit approved plan.
