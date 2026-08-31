# CRM Sprint 11 S11-04 - Lead Intake Frontend Foundation Page

## Page route

`/foundation/leads/qualification`

## Workflow

The page lets a user select a synthetic foundation lead, review safe lead information, choose a qualification decision, submit the decision to the S11-03 foundation API and view the resulting qualification state.

## Screen sections

- Page header with `Development / Foundation` scope.
- Lead selection and lead summary.
- Qualification form.
- Result panel.
- Safe error panel.

## API dependency

The Angular service calls only:

`POST /api/crm/foundation/leads/{leadId}/qualification`

It does not call productive `/api/crm/leads` routes.

## Validation

- Decision is required.
- Disqualification reason is required when disqualifying.
- `Other` reason requires an explanation.
- Other reason max length: 250.
- Comment max length: 500.

Backend validation remains authoritative.

## Qualification decisions

- `Qualify`
- `Disqualify`

## Disqualification reasons

- `InvalidContactInformation`
- `Duplicate`
- `NoInterest`
- `OutOfTarget`
- `Unreachable`
- `Other`

## Error states

- `400`: validation issue.
- `404`: lead not found.
- `409`: transition not permitted.
- Unexpected: safe generic unavailable message.

No stack traces, raw exception JSON, tokens or infrastructure details are displayed.

## Responsive behavior

The page uses a two-column desktop layout and collapses to one column on smaller screens.

## Accessibility

Controls have labels, focus states are visible, errors use readable text with alert roles, and color is not the only signal.

## Security

No login/logout, Portal Auth runtime, token storage, auth interceptor, local/session storage, Common DB dependency or real customer data is introduced.

## Foundation scope

- `DevelopmentOnly`: true.
- `FoundationOnly`: true.
- `NonProductionOnly`: true.
- `PortalRuntimeEnabled`: false.
- `CommonDbRuntimeEnabled`: false.

## S11-05 entry conditions

- Domain, application service, API and frontend foundation flow are implemented.
- Frontend build/test passes.
- Backend build/test passes.
- Guardrails and foundation verification pass.
- Productive routes remain disabled.

