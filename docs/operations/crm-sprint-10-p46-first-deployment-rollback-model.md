# CRM Sprint 10 P46 - First Deployment Rollback Model

ProductionDeploymentState: UnknownUntilP47

If P47 proves this is the first CRM Production deployment, rollback may be defined as:

- stop/remove the newly introduced CRM service;
- restore previous no-service state;
- revert routing if routing was introduced;
- restore previous configuration;
- validate health/traffic state after reversal.

RollbackBaselineType: NotResolved
RollbackTarget: MissingRequiredExternalConfiguration

