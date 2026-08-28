# CRM Sprint 10 P50 - NonProd Isolation Postcheck

NonProdIsolationPostcheckExists: true

NonProdContainer: crm-crm-api-1
NonProdRunning: true
NonProdHealth: HTTP 200
NonProdPort: 8093

SimulatedProductionContainer: crm-api-prod-sim
SimulatedProductionPort: 8094

SeparatePorts: true
SeparateComposeProjects: true
SeparateContainers: true
SeparateNetworks: true

NonProdUnaffected: true
