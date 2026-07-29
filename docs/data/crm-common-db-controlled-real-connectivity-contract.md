# CRM Common DB Controlled Real Connectivity Contract

Endpoint foundation:

- `GET /api/crm/foundation/sprint-8/common-db-controlled-real-connectivity`

Probe foundation:

- `POST /api/crm/foundation/sprint-8/common-db-controlled-real-connectivity/probe`

El contrato retorna solo metadata sanitizada:

- `secretName`
- `probeAttempted`
- `providerConfigured`
- `connectionAttempted`
- `connected`
- `timeoutApplied`
- `elapsedMs`
- `errorCategory`
- `connectionStringReturned=false`
- `connectionStringLogged=false`

Nunca retorna la connection string.
