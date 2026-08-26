# CRM Sprint 10 P44G - Residual Risk Acceptance Matrix

| RiskId | Description | Human decision required | Current decision |
| --- | --- | --- | --- |
| LocalOnlyProductionArtifact | Production candidate artifact is local-only, not registry-published. | LocalOnlyArtifactAcceptedForP45: true | LocalOnlyArtifactAcceptedForP45: false |
| LocalOnlyRollbackArtifact | Rollback artifact is local-only. | LocalOnlyRollbackAccepted: true | LocalOnlyRollbackAccepted: false |
| NoOfficialSBOMScanner | No official image scanner/SBOM is available. | SbomScannerResidualRiskAccepted: true | SbomScannerResidualRiskAccepted: false |

ResidualRiskSetFrozen: true
LocalOnlyArtifactAcceptedForP45: false
LocalOnlyRollbackAccepted: false
SbomScannerResidualRiskAccepted: false
CriticalProductionBlockers: 0
HighBlockingRisks: 0
ProductionApprovalDecision: NoGo

