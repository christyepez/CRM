# CRM Sprint 10 P44C - Security Condition Acceptance

SBOMAvailable: false
OfficialImageScannerAvailable: false
SbomScannerResidualRiskAccepted: false

SecurityCompensatingControls:
- Source guardrails executed.
- Secret/token/certificate guardrails executed by repository scripts.
- Docker compose contains no SQL Server and no Portal service.
- Runtime target scope excludes Portal, Common DB and production data writes.
- Candidate image identity is frozen and must be revalidated before P45.

SecurityProductionApprovalDecision: ApprovedWithUnacceptedResidualCondition
TechnicalProductionApprovalPassed: false

ProductionApprovalDecision: NoGo
