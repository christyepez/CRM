# CRM Common DB Runtime Connectivity Trial Contract

Status endpoint:

`GET /api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial`

Probe endpoint:

`POST /api/crm/foundation/sprint-9/common-db-runtime-connectivity-trial/probe`

Request:

```json
{
  "secretName": "crm-common-db-connection"
}
```

Response is sanitized metadata only:

- logical `secretName`
- connection attempted/connected booleans
- redaction booleans
- schema/migration/EF/productive persistence booleans
- elapsed time
- sanitized status and category

The response must never include connection string values.
