# CRM Secret Provider Real NonProduction Runtime Probe Contract

Endpoint:

`GET /api/crm/foundation/sprint-7/secret-provider-real-nonproduction-runtime-probe`

Expected default response markers:
- `status = SecretProviderRealNonProductionRuntimeProbe`
- `secretProviderRealNonProductionRuntimeProbeExists = true`
- `secretProviderRealNonProductionApprovalGranted = false`
- `secretProviderRealRuntimeProbeEnabled = false`
- `secretProviderRealRuntimeProbeAttempted = false`
- `secretProviderRealRuntimeConnected = false`
- `realSecretReadAttempted = false`
- `realSecretValueMaterialized = false`
- `realSecretValueLogged = false`
- `secretValueReturnedToApi = false`
- `keyVaultRuntimeClientCreated = false`
- `keyVaultRuntimeCallAttempted = false`
- `azureSecretSdkRuntimeEnabled = false`
- `envSecretReadAttempted = false`
- `envFileRequired = false`
- `logicalSecretNamesValidated = true`
- `secretValuesValidated = false`
- `probeSkippedBecauseApprovalNotGranted = true`
- `nextGate = Sprint7P3CommonDbRealConnectivityNonProductionProbe`
