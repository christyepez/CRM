# CRM Sprint 6 P2 - Secret Provider Safe Mock Activation

Status: safe mock enabled for non-production contract validation only.

This sprint enables a deterministic in-memory mock provider with synthetic values. It does not read real secrets, `.env`, files, environment variables, Key Vault, Azure secret SDKs or external secret managers.

Default decisions:

- SecretProviderSafeMockExists: true
- SecretProviderSafeMockEnabled: true
- SecretProviderRuntimeConnected: false
- SecretProviderReadsRealSecrets: false
- SecretProviderReadsSyntheticValues: true
- SecretProviderReadsEnabledForMockOnly: true
- RealSecretsConfigured: false
- EnvFileRequired: false
- KeyVaultClientConfigured: false
- AzureSdkForSecretsConfigured: false
- SecretValuesExposedInLogs: false
- CommonDbDryRunApprovalGranted: false
- PortalAuthDryRunApprovalGranted: false
- RealActivationApprovalGranted: false
- NonProductionOnly: true
- NextGate: Sprint6P3CommonDbConnectivityDryRunContract

Warning: `Secret Provider safe mock only; no real secrets are read`.

P2 does not approve DB dry-run, Portal Auth dry-run, locked stubs runtime, productive routes, DELETE or real activation.
