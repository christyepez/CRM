# CRM Common DB Prerequisites Checklist

## Before any runtime activation

- Portal Sprint 21 consumer contract approved.
- Common SQL Server environment identified by platform, not CRM.
- CRM logical database name approved.
- Secret Provider metadata contract approved and redaction verified.
- No real connection strings committed.
- No `.env` committed.
- No schema or migration created by P2.
- No Portal DB table accessed directly.
- Rollback plan reviewed.
- NonProduction approval recorded.

## Required status

- CommonDbPrerequisitesChecklistPrepared: true.
- PortalSprint21ContractAlignmentReviewed: true.
- RealCommonDbConnectionConfigured: false.
- RealConnectionStringsPresent: false.
- EnvRealFileCommitted: false.
- SecretsPresent: false.
