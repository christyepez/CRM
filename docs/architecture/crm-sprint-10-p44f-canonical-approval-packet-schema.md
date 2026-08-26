# CRM Sprint 10 P44F - Canonical Approval Packet Schema

CanonicalizationVersion: crm-approval-packet-canonical-json-v1
HashAlgorithm: SHA-256
HashEncoding: lowercase hexadecimal
Encoding: UTF-8 without BOM
LineEndings: canonical JSON string, independent from physical file line endings
ObjectKeyOrdering: ordinal ascending
Whitespace: none outside string values
BooleanFormat: true|false lowercase
NullHandling: rejected unless explicitly allowed by schema
ArrayOrdering: deterministic as authored for semantic lists
TrailingNewline: none in canonical representation

CanonicalPacketSchema: structured JSON object
CanonicalPacketPath: docs/roadmap/crm-sprint-10-p44f-final-approval-packet-v3.json
CanonicalHashTool: tools/approval-packet-hash.ps1

RequiredFields:
- schemaVersion
- packetId
- environment
- runtimeTargetCommit
- candidateImageTag
- candidateImageId
- candidateImageDigest
- artifactPublished
- targetImageDecision
- executionScope
- executionScopeHash
- portalIncluded
- commonDbIncluded
- productionDataChangesApproved
- approvedExternalDependencies
- deploymentStrategy
- rollbackMechanism
- rollbackTargetImmutable
- rollbackArtifactId
- rollbackArtifactPublished
- monitoringReady
- sbomAvailable
- officialImageScannerAvailable
- residualRiskIds
- configurationManifestVersion
- runbookVersion
- rollbackPlanVersion
- monitoringPlanVersion
- testPlanVersion

ForbiddenHashedFields:
- timestamp
- generatedAt
- validatedAt
- machineName
- absolutePath
- workingDirectory
- containerId
- currentContainerId
- currentProcessId
- dockerRuntimeTimestamp
- lastHealthCheckTimestamp
- gitBranchTemporaryName
- prNumber
- reviewerName
- humanApprovalTimestamp
