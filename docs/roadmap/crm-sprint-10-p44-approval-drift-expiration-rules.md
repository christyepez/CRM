# P44 Approval Drift and Expiration Rules

ProductionApprovalDriftDetected: false
ProductionApprovalValidUntilDrift: true

A future approval is invalidated by:

- target commit change
- target image change
- release change
- configuration change
- scope change
- runbook change
- rollback plan change
- monitoring regression
- test plan change
- critical vulnerability
- new critical blocker
- high blocking risk
- environment drift
- infrastructure drift
- production target drift

P45 must revalidate drift before any execution.
