# CRM Sprint 10 P47U - Human Operations Input Request

P47UHumanOperationsInputRequestExists: true
P47UDecision: NotReadyForNewHumanApproval
P48AllowedNow: false
ProductionExecutionStarted: false
ProductionDeploymentExecuted: false
ProductionActivated: false

| RequiredField | WhyRequired | ExampleFormat | SecretOrNonSecret | Blocking |
| --- | --- | --- | --- | --- |
| DeploymentPlatform | Identifies where CRM would run in Production. | DockerCompose, Kubernetes, AzureContainerApps, VM | NonSecret | true |
| TargetHostIdentifier | Binds approval to a real host/resource. | prod-crm-host-01 or azure resource id redacted to non-secret reference | NonSecret | true |
| TargetRuntimeIdentifier | Identifies the target service/container/app. | crm-api service name or container app name | NonSecret | true |
| DeploymentMechanism | Defines how deployment would be executed later. | pipeline name, script path, or command template without secrets | NonSecret | true |
| ProductionConfigurationSource | Identifies immutable production config source. | GitHub Environment, config manifest id, host config path reference | NonSecret | true |
| ProductionSecretSource | Identifies where secrets are resolved without exposing values. | AzureKeyVault name reference, DockerSecrets, GitHub Environment Secrets | SecretReferenceOnly | true |
| ProductionNetworkBoundary | Defines ingress/reverse proxy/firewall exposure. | reverse proxy name, ingress name, load balancer reference | NonSecret | true |
| ProductionBaseUrl | Required for health/readiness monitoring. | https://crm.example.com | NonSecret | true |
| ProductionServicePort / PublishedPort | Required for route and rollback validation. | servicePort=8080, publishedPort=443 | NonSecret | true |
| ProductionMonitoringSources | Required to detect failure and support rollback decisions. | logs, metrics, uptime check, health probe, alert references | NonSecret | true |
| CurrentProductionServicePresent | Determines if rollback is first-deployment removal or previous-version restore. | true, false, Unknown with evidence | NonSecret | true |
| ProductionDeploymentState | Required rollback classification. | FirstDeployment or ExistingDeployment | NonSecret | true |

## Required response from Operations

Provide the fields above with evidence references. Do not provide passwords, tokens, client secrets, private URLs that cannot be stored in repo, or certificate material.

Until this request is answered, P48 and any new human Production approval remain blocked.
