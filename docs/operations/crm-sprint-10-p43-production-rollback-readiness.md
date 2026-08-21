# P43 Production Rollback Readiness

RollbackReadyForApproval: true

Triggers: health failure, readiness failure, deployment failure, security regression, configuration mismatch, monitoring loss.

RollbackOwnerRole: RollbackAuthorityRole
Procedure: restore previous approved image and previous approved configuration.
PreviousRelease: required before P45.
ConfigurationRollback: required.
RuntimeRollback: previous image redeploy.
DataRollbackIfApplicable: NotApplicable for first slice.
ExpectedResult: health/readiness 200 after rollback.
RecoveryWindow: TBD-business-threshold.
Evidence: rollback command output, health checks and decision record.
RollbackExecutedInP43: false
