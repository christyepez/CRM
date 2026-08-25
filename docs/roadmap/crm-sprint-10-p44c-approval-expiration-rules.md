# CRM Sprint 10 P44C - Approval Expiration Rules

ApprovalExpirationRules: true

Any future approval expires if any of the following changes:
- RuntimeTargetCommit
- ImageId
- ImageDigest
- Scope
- ScopeHash
- Configuration
- Runbook
- Rollback
- Monitoring
- Tests
- Security findings
- Infrastructure
- Environment

CurrentP44CApprovalState: NoGo
ProductionExecutionAuthorized: false
