# CRM Sprint 10 P44E - Residual Risk Acceptance Matrix

| RiskId | Description | Technical State | HumanAcceptanceRequired | HumanAcceptanceRecorded | HumanApproverReference | Decision |
| --- | --- | --- | --- | --- | --- | --- |
| R1 LocalOnlyProductionArtifact | Production candidate artifact is local-only. | ProductionArtifactPublished: false; ProductionTargetImageDecision: ImmutableLocallyOnly | true | false | none | NotAccepted |
| R2 LocalOnlyRollbackArtifact | Rollback artifact is local-only. | RollbackArtifactPresent: true; RollbackArtifactPublished: false | true | false | none | NotAccepted |
| R3 NoOfficialSBOMScanner | SBOM and official image scanner are unavailable. | SBOMAvailable: false; OfficialImageScannerAvailable: false | true | false | none | NotAccepted |

LocalOnlyArtifactAcceptedForP45: false
LocalOnlyRollbackAccepted: false
SbomScannerResidualRiskAccepted: false

ProductionApprovalDecision: NoGo
P45Authorized: false
