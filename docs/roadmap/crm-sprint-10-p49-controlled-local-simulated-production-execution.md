# CRM Sprint 10 P49 - Controlled Local Simulated Production Execution and Validation

P49ControlledLocalSimulatedProductionExecutionExists: true
P48PullRequest: #136
P48MergeCommit: 1db2ef042058324bce490ab0e2346cee8c50c480
P49BaseMainCommit: 1db2ef042058324bce490ab0e2346cee8c50c480

EnvironmentClassification: SimulatedProduction
RealProduction: false
LocalSimulation: true
HumanApprovalReference: explicit-user-chat-approval-p48-local-simulated-production-2026-08-27
P49Authorized: true

TargetManifestId: CRM-S10-P47W-SIMPROD-TARGET-V1
TargetManifestHash: 075b67f6bf492e446908b21f365523252d91c76c5cc62e70faa62831313b61b5
TargetManifestHashMatched: true

RollbackManifestId: CRM-S10-P47W-SIMPROD-ROLLBACK-V1
RollbackManifestHash: 9d4e5a95f5be179516f7fac160f855adb8595e7b8012acc9270fe6f6a93edf1d
RollbackManifestHashMatched: true

ApprovalPacketId: CRM-S10-P47W-SIMPROD-PACKET-V5
ApprovalPacketHash: f33a6af176066e90dbc674ae9393318dd934646cc6a747ef5ffd31ca988593a9
ApprovalPacketHashMatched: true

RuntimeTargetCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c
CandidateImageTag: crm-api:prod-candidate-8623c619
ExpectedCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
ActualCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
CandidateImageIdentityMatched: true

RuntimeSourceDriftDetected: false
DockerBuildInputDriftDetected: false
RuntimeConfigurationDriftDetected: false
DependencyDriftDetected: false

P49ExecutionStarted: true
ApprovalConsumed: true
ApprovalConsumedAt: 2026-08-27T22:36:50Z

DeploymentCommandExecuted: docker compose -p crm-prod-sim --env-file .env.prod-sim.example -f docker-compose.prod-sim.yml up -d --force-recreate
NoBuildExecuted: true
NoPullExecuted: true
NoRuntimeConfigChanged: true

ContainerRunning: true
DockerHealth: healthy
ContainerUser: 65532:65532
ContainerName: crm-api-prod-sim
ComposeProject: crm-prod-sim
Network: crm-prod-sim-net
PublishedPort: 127.0.0.1:8094
ServicePort: 8080

Health: HTTP 200
Liveness: HTTP 200
Readiness: HTTP 200
CRMReadiness: HTTP 200 ReadyForFoundationOnly

RootStatus: HTTP 404
SwaggerStatus: HTTP 404
WebAccessStatus: ExpectedBehavior
ApplicationType: APIOnly

StabilitySamples: 5
HealthSuccessCount: 5
HealthFailureCount: 0
RestartCountBefore: 0
RestartCountAfter: 0
LatencyMinMs: 3.58
LatencyAverageMs: 4.73
LatencyP95Ms: 6.87

CriticalLogErrorsDetected: false
BenignLogWarningsDetected: true
BenignLogWarnings: ASPNETCORE_URLS overrides HTTP_PORTS; expected Kestrel startup warning.

PortalRuntimeCallsDetected: false
CommonDbRuntimeCallsDetected: false
CRMOwnedSqlServerDetected: false
ProductionDataWritesDetected: false
UnexpectedExternalDependencyDetected: false

NonProdHealthBefore: HTTP 200
NonProdHealthAfter: HTTP 200
NonProdPortBefore: 8093
NonProdPortAfter: 8093
NonProdUnaffected: true

RollbackExecuted: false
RollbackResult: NotRequired

SimulatedProductionActivated: true
RealProductionActivated: false

UnitTests: 185/185 PASS
ArchitectureTests: 96/96 PASS
FullTests: 281/281 PASS
SecurityGuardrails: PASS

CriticalExecutionBlockers: 0
P48ApprovalReusable: false
ExistingHumanApprovalStillValidForRetry: false
NewHumanApprovalRequired: true

P49Decision: ExecutedSuccessfully
NextGate: CRM Sprint 10 P50 - Local Simulated Production Post-Execution Validation and Pilot Closure

## Notes

This execution consumed the P48 approval. Any later retry or target change requires a new explicit approval gate.

This was a local Docker Compose simulated Production execution only. It did not authorize or perform corporate Production, Azure Production, Portal activation, Common DB activation, production data changes, image rebuilds or scope changes.
