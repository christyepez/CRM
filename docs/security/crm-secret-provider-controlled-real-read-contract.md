# CRM Secret Provider Controlled Real Read Contract

Endpoint foundation:

- `GET /api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read`

Probe foundation opcional:

- `POST /api/crm/foundation/sprint-8/secret-provider-controlled-real-nonproduction-read/probe`

El contrato público solo devuelve metadata sanitizada:

- `secretName`
- `readAttempted`
- `readSucceeded`
- `valueObserved=false`
- `valueReturned=false`
- `valueLogged=false`
- `valuePersisted=false`
- `valueCached=false`
- `providerConfigured`
- `redactionApplied=true`
- `redactedFingerprint` opcional irreversible

Nunca devuelve el valor secreto.
