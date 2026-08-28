# CRM Sprint 11 Backlog

Sprint11BacklogExists: true
SelectedSprint11SliceId: S11-LEAD-QUAL
SelectedSprint11SliceName: Lead Intake and Qualification Foundation
Sprint11PlanningStatus: ReadyForImplementation

## Stories

### S11-01 - Lead qualification contracts and domain rules

UserStory: As a CRM user, I need leads to expose explicit qualification data so early sales intake can move beyond generic preview CRUD.

AcceptanceCriteria:

- Lead qualification statuses and validation rules are represented in contracts/domain-safe code.
- No productive route is unlocked.
- No Common DB or Portal runtime is activated.
- Existing tests remain green.

ResponsibleAgent: Backend Agent + Domain Agent + Security Agent
ReviewAgents: Architecture Governance Agent + QA Lead Agent
EstimatedComplexity: M

### S11-02 - Lead qualification application service

Add explicit qualify/disqualify service operations using foundation seam persistence.

### S11-03 - Lead qualification API foundation endpoints

Add `/api/crm/foundation/leads/{id}/qualify` and `/disqualify` only after service readiness.

### S11-04 - Lead intake frontend foundation page

Add a small Angular development-only page/service for foundation lead intake.

### S11-05 - Test and guardrail hardening

Add/update unit, architecture, API contract and frontend checks.

### S11-06 - Local integration validation

Validate local dev API/frontend without touching simulated Production.

### S11-07 - Sprint 11 closure

Close the selected slice and propose the next business slice.
