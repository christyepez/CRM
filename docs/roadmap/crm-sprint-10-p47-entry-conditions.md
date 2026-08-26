# CRM Sprint 10 P47 - Entry Conditions

P47EntryConditionsPrepared: true
P47Task: CRM Sprint 10 P47 - Production Target and Rollback Baseline Resolution

P47 must:

- resolve exact Production target;
- create canonical Production Target Manifest;
- capture current Production baseline;
- determine FirstDeployment vs ExistingDeployment;
- freeze rollback target;
- validate monitoring against real Production target;
- determine approval drift;
- generate a new Approval Packet version if required;
- prepare new human approval if material fields changed;
- resolve or explicitly gate ArchitectureTests non-conclusive status before retrying P45.

P45RetryAllowedBeforeP47: false
NewHumanApprovalRequiredForRetry: true

