# CRM Sprint 10 P47W - Freeze Local Simulated Production Target and Approval Packet V5

P47WFreezeLocalSimulatedProductionTargetExists: true
OPS04PullRequest: #134
OPS04MergeCommit: cb7b1cc3cf9fd632cb83f4eb56a6787aa1ddbbc6
P47WBaseMainCommit: cb7b1cc3cf9fd632cb83f4eb56a6787aa1ddbbc6

EnvironmentClassification: SimulatedProduction
RealProduction: false
LocalSimulation: true
ApprovalReference: explicit-user-chat-approval-local-simulated-production

ApplicationType: APIOnly
WebUISourcePresent: false
StaticFilesConfigured: false
SwaggerConfigured: false
SwaggerEnabledInSimulatedProduction: false
RootRouteConfigured: false
FrontendProjectPresent: true
FrontendProjectPath: frontend/crm-web
FrontendIncludedInCurrentProductionScope: false

RootUrlStatusCode: 404
RootUrlResponseClassification: 404
SwaggerStatus: 404
ExpectedSimulatedProductionAccess: APIHealthOnly
WebAccessStatus: ExpectedBehavior

Health: HTTP 200
Liveness: HTTP 200
Readiness: HTTP 200
CRMReadiness: HTTP 200 ReadyForFoundationOnly
ProductiveRoutesExposureValid: true

ContainerRunning: true
DockerHealth: healthy
RestartCount: 0
BoundToLoopback: true
NetworkBoundaryValidated: true

CandidateImageTag: crm-api:prod-candidate-8623c619
ExpectedCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
ActualCandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
CandidateImageIdentityMatched: true

ProductionTargetManifestId: CRM-S10-P47W-SIMPROD-TARGET-V1
ProductionTargetManifestHash: 075b67f6bf492e446908b21f365523252d91c76c5cc62e70faa62831313b61b5
ProductionTargetFrozen: true

RollbackBaselineId: CRM-S10-P47W-SIMPROD-ROLLBACK-V1
RollbackBaselineHash: 9d4e5a95f5be179516f7fac160f855adb8595e7b8012acc9270fe6f6a93edf1d
RollbackBaselineIdentified: true
RollbackMechanismDefined: true
RollbackMechanismDeterministic: true
RollbackTargetDeterministic: true
RollbackReadyForRetry: true
RollbackBaselineFrozen: true

ProductionMonitoringTargetResolved: true
ProductionMonitoringReadyForRetry: true
ProductionLogSource: docker logs crm-api-prod-sim
ProductionMetricSource: docker stats / docker inspect local simulation
ProductionAvailabilitySource: HTTP health/readiness probes
ProductionRestartSignalSource: Docker RestartCount
ProductionErrorRateSource: HTTP responses + container/application logs
ProductionLatencySource: controlled HTTP probe timing

NonProdUnaffected: true
SeparateComposeProject: true
SeparateContainer: true
SeparatePort: true
SeparateNetwork: true

RuntimeSourceDriftDetected: false
DockerBuildInputDriftDetected: false
RuntimeConfigurationDriftDetected: false
DependencyDriftDetected: false

ArchitectureTestsStatus: Passed
ArchitectureTestsBlocking: false
FullTestsStatus: Passed

PortalIncluded: false
CommonDbIncluded: false
ProductionDataChangesApproved: false
ApprovedProductionExternalDependencies: none

PreviousHumanApprovalReusable: false
ExistingHumanApprovalStillValidForRetry: false
NewHumanApprovalRequiredForRetry: true

NewFinalApprovalPacketId: CRM-S10-P47W-SIMPROD-PACKET-V5
NewFinalApprovalPacketHash: f33a6af176066e90dbc674ae9393318dd934646cc6a747ef5ffd31ca988593a9
CanonicalPacketHashStable: true
FinalApprovalPacketFrozen: true

CriticalProductionBlockers: 0
P47WDecision: ReadyForNewHumanSimulatedProductionApproval
P48AllowedNow: true
NextGate: CRM Sprint 10 P48 - Local Simulated Production Explicit Human Approval Gate

## Web/API decision

The user-reported browser issue is expected for this accepted scope. The current runtime is an API-only .NET service for the CRM API first slice. `/` returns 404 and Swagger is not configured/enabled in Production. Health and CRM readiness APIs are healthy, and the Angular frontend under `frontend/crm-web` is not included in the current simulated Production scope.
