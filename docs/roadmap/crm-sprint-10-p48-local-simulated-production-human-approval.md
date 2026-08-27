# CRM Sprint 10 P48 - Local Simulated Production Explicit Human Approval Gate

P48LocalSimulatedProductionHumanApprovalExists: true
P47WPullRequest: #135
P47WMergeCommit: af2e80993e09ec42e623a291e4db2c2a6c973e46
P48BaseMainCommit: af2e80993e09ec42e623a291e4db2c2a6c973e46

ApprovalDecision: APPROVE
ApprovalDate: 2026-08-27
ApprovalReference: explicit-user-chat-approval-p48-local-simulated-production-2026-08-27

EnvironmentClassification: SimulatedProduction
RealProduction: false
LocalSimulation: true

FinalApprovalPacketId: CRM-S10-P47W-SIMPROD-PACKET-V5
FinalApprovalPacketHash: f33a6af176066e90dbc674ae9393318dd934646cc6a747ef5ffd31ca988593a9
FinalApprovalPacketHashMatched: true

ProductionTargetManifestId: CRM-S10-P47W-SIMPROD-TARGET-V1
ProductionTargetManifestHash: 075b67f6bf492e446908b21f365523252d91c76c5cc62e70faa62831313b61b5
ProductionTargetManifestHashMatched: true

RollbackBaselineId: CRM-S10-P47W-SIMPROD-ROLLBACK-V1
RollbackBaselineHash: 9d4e5a95f5be179516f7fac160f855adb8595e7b8012acc9270fe6f6a93edf1d
RollbackBaselineHashMatched: true

RuntimeTargetCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c
CandidateImageTag: crm-api:prod-candidate-8623c619
CandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
CandidateImageIdentityMatched: true

DeploymentPlatform: DockerCompose
ComposeProject: crm-prod-sim
Container: crm-api-prod-sim
Network: crm-prod-sim-net
ProductionBaseUrl: http://127.0.0.1:8094
ProductionPublishedPort: 8094
ProductionServicePort: 8080

ApprovedScope: CRM API first slice only
PortalIncluded: false
CommonDbIncluded: false
ProductionDataChangesApproved: false
ApprovedExternalDependencies: none

ResidualRisksAccepted: true

- R1: This is Local Simulated Production, not corporate Production.
- R2: Root "/" returns 404 because the accepted scope is API-only.
- R3: Swagger is unavailable in SimulatedProduction.
- R4: `/api/crm/readiness` reports `ReadyForFoundationOnly` because integrations remain Planned/Disabled by the current contract.
- R5: Azure Container Apps real Production remains deferred.

HumanApprovalRecorded: true
HumanApprovalDecision: Go
SimulatedProductionExecutionAuthorized: true
RealProductionAuthorized: false
P49Authorized: true
ApprovalConsumed: false
ProductionActivated: false

CriticalProductionBlockers: 0
P48Decision: GoForP49LocalSimulatedProductionControlledExecution
NextGate: CRM Sprint 10 P49 - Local Simulated Production Controlled Execution

## Approval constraints

This approval applies only to the exact frozen local simulated Production target, rollback baseline, runtime commit, candidate image and approval packet listed above.

Any later change to packet hash, target hash, rollback hash, runtime commit, candidate image, Docker Compose target, port, network, configuration, monitoring, rollback, scope, Portal/Common DB boundaries or data-change boundary invalidates this approval and requires a new approval gate.

This approval does not authorize real corporate Production, Azure Production, Portal activation, Common DB activation, production data changes, additional external dependencies, image rebuilds or target changes.
