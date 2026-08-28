# CRM Sprint 10 P49 - NonProd Isolation Evidence

NonProdIsolationEvidenceExists: true

NonProdContainer: crm-crm-api-1
NonProdPortBefore: 8093
NonProdHealthBefore: HTTP 200
NonProdContainerStateBefore: running

SimulatedProductionContainer: crm-api-prod-sim
SimulatedProductionPort: 8094
SimulatedProductionComposeProject: crm-prod-sim
SimulatedProductionNetwork: crm-prod-sim-net

SeparateComposeProject: true
SeparateContainer: true
SeparatePort: true
SeparateNetwork: true

NonProdPortAfter: 8093
NonProdHealthAfter: HTTP 200
NonProdContainerStateAfter: running
NonProdUnaffected: true
