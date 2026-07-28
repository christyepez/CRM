# CRM Common DB Real Connectivity NonProduction Probe Contract

Endpoint:

`GET /api/crm/foundation/sprint-7/common-db-real-connectivity-nonproduction-probe`

Expected default markers:
- `status = CommonDbRealConnectivityNonProductionProbe`
- `commonDbRealConnectivityNonProductionProbeExists = true`
- `commonDbRealConnectivityApprovalGranted = false`
- `secretProviderRealNonProductionApprovalGranted = false`
- `connectionStringResolved = false`
- `connectionStringValueMaterialized = false`
- `connectionStringLogged = false`
- `connectionStringReturnedToApi = false`
- `commonDbProbeEnabled = false`
- `commonDbProbeAttempted = false`
- `commonDbConnected = false`
- `sqlConnectionCreated = false`
- `dbConnectionCreated = false`
- `useSqlServerEnabled = false`
- `efRuntimeEnabled = false`
- `addDbContextRuntimeEnabled = false`
- `migrationsCreated = false`
- `databaseSchemaChanged = false`
- `usesSyntheticFallback = true`
- `syntheticConnectionReference = mock://crm/common-db`
- `connectionProbeSkippedBecauseSecretProviderApprovalNotGranted = true`
- `nextGate = Sprint7P4PortalAuthRealRuntimeProbe`
