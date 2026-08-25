# CRM Sprint 10 P44C - Production Approval Drift Validation

RuntimeTargetCommit: 8623c6191f5b59397d1243d2e0f8b30ee5caae6c
ApprovalGovernanceCommit: PendingP44CMergeCommit
ImageId: sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
ImageDigest: crm-api@sha256:b0a75dc3986d433ba18207fea518c2a3e264eb89cf7298fd4fdb9bf860caec37
Scope: CRM API first slice only; Portal excluded; Common DB excluded; Production data changes excluded; External dependencies none.
ScopeHash: P44BProductionScopeFreezeV2
Configuration: unchanged from P44B
Runbook: P44B runbook remains approval-review-only
Monitoring: ready for approval review
Rollback: ready with local-only condition
Security: residual SBOM/scanner condition remains unaccepted

ProductionApprovalDriftDetected: true
ProductionApprovalDriftReason: NonProduction runtime stable state changed from true in P44B to false in P44C safe revalidation.

ProductionApprovalDecision: NoGo
ProductionExecutionAuthorized: false
