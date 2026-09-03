# CRM Sprint 11 Roadmap

Sprint11RoadmapExists: true
Goal: Return CRM to business functionality after closing the Sprint 10 local simulated Production pilot.

SelectedSlice: S11-LEAD-QUAL - Lead Intake and Qualification Foundation
Sprint11LeadQualificationClosed: true
LeadQualificationFoundationSliceStatus: ClosedSuccessfully
LeadQualificationFoundationOperationalState: ValidatedLocally
LeadQualificationProductiveStatus: NotActivated

Milestones:

1. S11-01: contracts/domain rules.
2. S11-02: application service.
3. S11-03: foundation API endpoints.
4. S11-04: frontend development page/service.
5. S11-05: tests and guardrails.
6. S11-06: local integration validation.
7. S11-07: closure and next slice decision.

ExitCriteria:

- Lead qualification behavior exists in foundation/development scope.
- Existing 281-test baseline remains green.
- Frontend build/test remains green if touched.
- Productive routes remain locked/404 by default.
- No real Production, Portal Auth runtime or Common DB runtime activation.
- Simulated Production baseline remains untouched.

FutureSlices:

- Contact management foundation. Recommended next as Sprint 12 P1.
- Account management foundation.
- Opportunity pipeline foundation.
- Activity/follow-up foundation.

DeferredIntegrations:

- Real Portal Auth.
- Real Common DB persistence.
- Real Production/Azure activation.
- Productive Angular deployment.

Next:

- CRM Sprint 12 P1 - Contact Management Functional Baseline and Backlog.
