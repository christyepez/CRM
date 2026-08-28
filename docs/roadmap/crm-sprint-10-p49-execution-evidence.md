# CRM Sprint 10 P49 - Execution Evidence

ExecutionEvidenceExists: true
ExecutionStartUtc: 2026-08-27T22:36:50Z
ExecutionEndUtc: 2026-08-27T22:38:20Z

ApprovalConsumed: true
ApprovalConsumedAt: 2026-08-27T22:36:50Z

DeploymentCommandExecuted: docker compose -p crm-prod-sim --env-file .env.prod-sim.example -f docker-compose.prod-sim.yml up -d --force-recreate

ExactTarget:

- ComposeProject: crm-prod-sim
- ContainerName: crm-api-prod-sim
- NetworkName: crm-prod-sim-net
- PublishedPort: 127.0.0.1:8094
- ServicePort: 8080

ExactImage:

- CandidateImageTag: crm-api:prod-candidate-8623c619
- CandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37

ContainerIdentityAfter:

- ContainerRunning: true
- DockerHealth: healthy
- RestartCount: 0
- ContainerUser: 65532:65532

HealthResponses:

- /health: HTTP 200 Healthy
- /health/live: HTTP 200 Healthy
- /health/ready: HTTP 200 Healthy
- /api/crm/readiness: HTTP 200 ReadyForFoundationOnly

ExpectedApiOnlySurface:

- /: HTTP 404
- /swagger: HTTP 404
- WebAccessStatus: ExpectedBehavior

FinalDecision: ExecutedSuccessfully
