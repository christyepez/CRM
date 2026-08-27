# CRM OPS-04 Rollback Evidence

EnvironmentClassification: SimulatedProduction
RealProduction: false
LocalSimulation: true

RollbackBaselineType: NoPreviousDeployment
RollbackTarget: PreDeploymentNoCRMState
RollbackMechanismDefined: true
RollbackMechanismDeterministic: true
RollbackTargetDeterministic: true
RollbackValidationDefined: true
RollbackMonitoringAvailable: true
RollbackReadyForRetry: true

RollbackTestExecuted: true
RollbackTestResult: Passed

Validation:

1. `docker compose -p crm-prod-sim --env-file .env.prod-sim.example -f docker-compose.prod-sim.yml down`
2. `crm-api-prod-sim` absent.
3. Port `8094` closed.
4. NonProduction `8093` remained healthy.
5. Redeployed exact frozen candidate.
6. Redeploy identity matched expected image id.
