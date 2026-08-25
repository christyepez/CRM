# CRM Sprint 10 P44D - NonProduction Runtime Stability Root Cause

NonProductionRuntimeBefore: Exited
NonProductionExitRootCause: ContainerRuntimeFailure
RootCauseEvidence: docker inspect reported Exited 255, restart policy no, restart count 0, no application exception in the last logs and prior health/readiness requests returned 200 before the container stopped.
CorrectiveAction: Restarted only the scoped NonProduction crm-api service using docker compose with .env.example.

ExitCode: 255
RestartPolicy: no
ApplicationExceptionDetected: false
ConfigurationErrorDetected: false
DependencyFailureDetected: false
HealthFailureDetected: false
ResourceFailureDetected: false
ManualStopEvidence: inconclusive

NonProductionRuntimeAfter: Running
NonProductionRuntimeStable: true
