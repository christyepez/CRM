# CRM Sprint 10 P44D - Final Production Approval Packet v2

Environment: Production

RuntimeTargetCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c
CandidateImageTag: crm-api:prod-candidate-8623c619
CandidateImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
CandidateImageDigest: crm-api@sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
ArtifactLocation: LocalDockerHostOnly

ProductionExecutionScope: CRM API first slice only
ProductionExecutionScopeHash: P44BProductionScopeFreezeV2

PortalIncluded: false
CommonDbIncluded: false
DataChangesApproved: false
ExternalDependencies: none

NonProductionRuntimeStable: true

RollbackMechanism: local immutable target validation before P45; abort if unavailable or mismatched
RollbackArtifactIdentity: crm-api@sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37

SBOMAvailable: false
OfficialImageScannerAvailable: false
SecurityCompensatingControls: source guardrails, dependency validation, secret/token/certificate scans, locked productive routes, expected port only, no Portal runtime, no Common DB runtime, no production data changes.

MonitoringReady: true
AbortCriteria: stop if RuntimeTargetCommit, CandidateImageId, scope, configuration, rollback, monitoring, security condition or environment changes.

ResidualRisks: LocalOnlyProductionArtifact; LocalOnlyRollbackArtifact; NoOfficialSbomScanner.

FinalApprovalPacketId: CRM-S10-P44D-PACKET-V2
FinalApprovalPacketHashAlgorithm: SHA256 over this packet file excluding the FinalApprovalPacketHash line.
FinalApprovalPacketHash: 15c4f02bfb5f09824d6facb41629e262db2d7fa571458c548b4bb882c554ca12
