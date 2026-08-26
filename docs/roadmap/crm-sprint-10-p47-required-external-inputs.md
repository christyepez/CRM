# CRM Sprint 10 P47 - Required External Inputs

P47RequiredExternalInputsExists: true
ProductionTargetResolutionDecision: NotResolved
ExternalInputsBlocking: true

| InputId | Description | WhyRequired | ExpectedFormat | Secret | SourceOwner | Blocking |
| --- | --- | --- | --- | --- | --- | --- |
| P47-I01 | Production host or platform | Determines exactly where the CRM image will run. | Hostname, platform resource id, or deployment platform identifier. | false | DevOps/Platform Owner | true |
| P47-I02 | Docker context or deployment executor | Required to make deployment commands reproducible. | Docker context name, CI/CD environment, or controlled executor reference. | false | DevOps/Platform Owner | true |
| P47-I03 | Runtime/service identifier | Required to distinguish first deployment vs existing deployment. | Service/container/app name and environment identifier. | false | DevOps/Platform Owner | true |
| P47-I04 | Routing/DNS/load-balancer target | Required for traffic and rollback validation. | DNS name, route id, reverse proxy route, or load balancer target. | false | DevOps/Platform Owner | true |
| P47-I05 | Production configuration source | Required to validate config without embedding secrets. | Config store id, file path convention, or environment manifest reference. | false | DevOps/Platform Owner | true |
| P47-I06 | Secret provider source | Required to bind runtime secrets without exposing values. | Secret store name and logical secret names only. | true | Security/Platform Owner | true |
| P47-I07 | Monitoring/log source | Required for health, readiness, logs, restart signals, and abort criteria. | Monitoring workspace, dashboard, log stream, or alert catalog reference. | false | SRE/Platform Owner | true |
| P47-I08 | Existing production baseline evidence | Required to decide FirstDeployment vs ExistingDeployment and rollback target. | Current image tag/id/digest and configuration version, or signed no-existing-deployment statement. | false | DevOps/Release Owner | true |

