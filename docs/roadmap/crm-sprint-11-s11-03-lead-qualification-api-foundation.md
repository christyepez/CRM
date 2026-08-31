# CRM Sprint 11 S11-03 - Lead Qualification API Foundation Endpoints

## Route

`POST /api/crm/foundation/leads/{leadId}/qualification`

## Request schema

```json
{
  "decision": "Qualify",
  "disqualificationReason": null,
  "otherReason": null,
  "comment": "Synthetic foundation-only comment"
}
```

Supported `decision` values:

- `Qualify`
- `Disqualify`

Supported `disqualificationReason` values:

- `InvalidContactInformation`
- `Duplicate`
- `NoInterest`
- `OutOfTarget`
- `Unreachable`
- `Other`

## Response schema

```json
{
  "leadId": "lead-preview-001",
  "previousStatus": "New",
  "currentStatus": "Qualified",
  "decision": "Qualify",
  "disqualificationReason": null,
  "allowed": true,
  "changed": true,
  "errorCode": "None",
  "message": "Lead qualification state changed.",
  "foundationMode": true,
  "persistenceMode": "NonProductionSeam",
  "productiveLeadQualificationRouteEnabled": false,
  "portalRuntimeEnabled": false,
  "commonDbRuntimeEnabled": false
}
```

## HTTP status table

| Result | Status |
| --- | ---: |
| Successful changed transition | 200 |
| Successful idempotent transition | 200 |
| Validation failure | 400 |
| Lead not found | 404 |
| Invalid transition | 409 |

## Validation behavior

The API maps request DTOs into `LeadQualificationRequest` and delegates validation and transition rules to `LeadQualificationService` and `LeadQualificationPolicy`. Invalid enum payloads, missing decisions, malformed lead identifiers and bounded string failures return safe deterministic responses.

## Idempotency

Same-state qualification requests return `200 OK` with `changed=false`.

## Error mapping

Errors expose `LeadQualificationErrorCode` and safe messages only. Responses must not expose stack traces, file paths, repository implementation names, database details, configuration values or tokens.

## Security boundary

The route is foundation-only and does not require or read Portal tokens. It does not add `[Authorize]`, CRM Identity, login/logout, bearer parsing or token storage.

## Foundation classification

- `FoundationOnly`: true.
- `DevelopmentOnly`: true.
- `NonProductionOnly`: true.
- `PortalRuntimeEnabled`: false.
- `CommonDbRuntimeEnabled`: false.

## Productive route negative behavior

The productive equivalent `/api/crm/leads/{id}/qualification` is not registered and remains unavailable by default.

## S11-04 entry criteria

- Foundation endpoint exists.
- HTTP contract is documented.
- Status mappings are deterministic.
- API/security tests are green.
- Productive route remains locked.
- Portal and Common DB runtime remain disabled.

