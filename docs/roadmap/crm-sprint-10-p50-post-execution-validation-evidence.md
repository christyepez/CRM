# CRM Sprint 10 P50 - Post-Execution Validation Evidence

PostExecutionValidationEvidenceExists: true

Target:

- ComposeProject: crm-prod-sim
- ContainerName: crm-api-prod-sim
- Network: crm-prod-sim-net
- PublishedPort: 127.0.0.1:8094
- ServicePort: 8080

TargetIdentity:

- ContainerRunning: true
- DockerHealth: healthy
- RestartCountCurrent: 0
- OOMKilled: false
- ExitCode: 0
- ContainerUser: 65532:65532
- ContainerImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37

Health:

- /health: HTTP 200
- /health/live: HTTP 200
- /health/ready: HTTP 200
- /api/crm/readiness: HTTP 200 ReadyForFoundationOnly

ExpectedApiOnlySurface:

- /: HTTP 404
- /swagger: HTTP 404
- WebAccessStatus: ExpectedBehavior

ClosureAssessment: Passed
