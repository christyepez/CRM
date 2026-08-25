# P44B Production Artifact Manifest

ArtifactId: crm-api-prod-candidate-8623c619
Repository: christyepez/CRM
RuntimeTargetCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c
ApprovalGovernanceCommit: P44B feature branch commit after merge
ImageRepository: crm-api
ImageTag: prod-candidate-8623c619
ImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
ImageDigest: crm-api@sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
BuildContextHash: 1395233e92da0392ad9e9b08d4f4b815a0e20970
BuildTimestamp: 2026-08-25T14:51:00Z
BuiltInEnvironment: LocalNonProductionDocker
Published: false
Registry: none
SBOMAvailable: false
VulnerabilityScanStatus: NotAvailableNoOfficialScannerConfigured

ProductionArtifactPublished: false
RegistryDigestAvailable: false
ProductionTargetImageDecision: ImmutableLocallyOnly

Rationale: the local image has an immutable image id and local digest, but it was not pushed to an authorized registry; P45 cannot assume remote retrieval unless a future gate publishes or explicitly accepts local-only execution.
