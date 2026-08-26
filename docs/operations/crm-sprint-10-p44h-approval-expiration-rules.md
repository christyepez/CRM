# CRM Sprint 10 P44H - Approval Expiration Rules

ApprovalExpirationRulesDefined: true
P45MustStopOnDrift: true

The P44H `Go` expires if any of these values drift:

- FinalApprovalPacketId
- FinalApprovalPacketHash
- RuntimeTargetCommit
- CandidateImageId
- CandidateImageDigest
- Scope
- ScopeHash
- Configuration
- Runbook
- Rollback
- Monitoring
- Security
- Infrastructure
- Environment

