# CRM Common DB Connectivity Dry-Run Contract

Foundation endpoint:

- `GET /api/crm/foundation/sprint-6/common-db-connectivity-dry-run`

Contract response must report:

- `status=CommonDbConnectivityDryRunContract`
- `commonDbDryRunApprovalGranted=false`
- `commonDbDryRunEnabled=false`
- `commonDbConnectionAttempted=false`
- `usesSecretProviderSafeMockMetadata=true`
- `usesSyntheticConnectionReference=true`
- `syntheticConnectionReference=mock://crm/common-db`
- `realConnectionStringUsed=false`
- `connectionStringResolved=false`
- `sqlConnectionCreated=false`
- `dbConnectionCreated=false`
- `efRuntimeEnabled=false`
- `migrationsCreated=false`
- `apiRequiresDatabase=false`

This contract is not a database adapter.
