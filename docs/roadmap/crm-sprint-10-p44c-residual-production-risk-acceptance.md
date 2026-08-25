# CRM Sprint 10 P44C - Residual Production Risk Acceptance

| RiskId | Description | Severity | BlockingWithoutAcceptance | Mitigation | ResidualRisk | HumanAcceptanceRequired | HumanAcceptanceRecorded | HumanApproverReference | Decision |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| R1 LocalOnlyProductionArtifact | The approved artifact exists only on the validated Docker host. | High | true | Validate ImageId immediately before P45 and abort if it differs. | Artifact loss, host change or rebuild could prevent exact reproduction. | true | false | none | NotAccepted |
| R2 LocalOnlyRollbackArtifact | Rollback depends on a local immutable target instead of a published external registry artifact. | High | true | Validate rollback image identity before execution and abort if unavailable. | Rollback may be delayed if the local artifact disappears. | true | false | none | NotAccepted |
| R3 NoOfficialSbomScanner | SBOM and official vulnerability scanner are unavailable. | Medium | true | Use source guardrails, dependency validation, no secrets, no productive external dependencies and locked scope. | Unknown image-level vulnerabilities may remain. | true | false | none | NotAccepted |

LocalOnlyArtifactAcceptedForP45: false
LocalOnlyRollbackAccepted: false
SbomScannerResidualRiskAccepted: false

ProductionApprovalDecision: NoGo
ProductionExecutionAuthorized: false
