# CRM Sprint 10 P47V - Missing Operations Inputs

P47VDecision: NotReadyForNewHumanApproval
P48AllowedNow: false

MissingOperationsInputs:

1. DeploymentPlatform
2. TargetHostIdentifier
3. TargetRuntimeIdentifier
4. DeploymentMechanism
5. ProductionConfigurationSource
6. ProductionSecretSource reference only
7. ProductionNetworkBoundary
8. ProductionBaseUrl
9. ProductionServicePort
10. ProductionPublishedPort or NotApplicable with evidence
11. ProductionMonitoringSources
12. CurrentProductionServicePresent

If `CurrentProductionServicePresent` is true, also provide current image tag, image id, image digest, and configuration reference.

If `CurrentProductionServicePresent` is false, explicitly provide `FirstDeploymentConfirmed: true` and `RollbackTarget: PreDeploymentNoCRMState`.
