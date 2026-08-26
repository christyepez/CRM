# CRM Sprint 10 P44G - Approval Expiration Rules

Any future Production approval expires if any of these values drift:

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

ApprovalExpirationRulesDefined: true
P45MustStopOnDrift: true
P45CandidateImageRebuildAllowed: false

