# CRM Sprint 6 P1 - NonProduction Runtime Approval Package

Status: Created, approval not granted.

This package exists to collect the approvals required before any non-production runtime trial. It does not approve real activation, secret reads, database connections, Portal Auth runtime, productive routes, locked stubs runtime, DELETE, login, token storage or productive UI.

Default decisions:

- NonProductionRuntimeApprovalPackageExists: true
- NonProductionRuntimeApprovalGranted: false
- SecretProviderMockApprovalGranted: false
- CommonDbDryRunApprovalGranted: false
- PortalAuthDryRunApprovalGranted: false
- LockedStubRuntimeTrialApprovalGranted: false
- RealActivationApprovalGranted: false
- ProductiveRoutesApprovalGranted: false
- DeleteApprovalGranted: false
- SyntheticDataApprovalRequired: true
- RollbackApprovalRequired: true
- ObservabilityApprovalRequired: true
- SecurityReviewRequired: true
- ArchitectureReviewRequired: true
- NextGate: Sprint6P2SecretProviderSafeMockActivation

Required approvals:

| Capability | Approval | Evidence required |
| --- | --- | --- |
| Secret Provider Safe Mock | Not granted | mock-only provider, no real secret reads, rollback, logs |
| Common DB Dry-Run Contract | Not granted | synthetic data, common SQL ownership, no connection before approval |
| Portal Auth Token Propagation Dry-Run Contract | Not granted | Portal boundary, no HTTP/token/header reads before approval |
| Locked Stub Runtime Trial | Not granted | locked response, runtime flag, negative route checks, rollback |

Exit criteria for this package are documentation, endpoint and guardrails only. Runtime trials remain blocked until their specific Sprint 6 gates.
