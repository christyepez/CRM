# CRM Secret Provider Runtime Enablement Trial Contract

Status endpoint:

`GET /api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial`

Probe endpoint:

`POST /api/crm/foundation/sprint-9/secret-provider-runtime-enablement-trial/probe`

Request:

```json
{
  "secretName": "crm-common-db-connection"
}
```

Response is metadata-only:
- `secretName`
- `readAttempted`
- `readSucceeded`
- `providerConfigured`
- `valueReturned=false`
- `valueLogged=false`
- `valuePersisted=false`
- `valueCached=false`
- `redactionApplied=true`
- `productionBlocked=true`
- `elapsedMs`
- sanitized `errorCategory`
