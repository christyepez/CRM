# CRM OPS-04 NonProduction Isolation Evidence

NonProdUnaffected: true
SimulatedProdUsesDedicatedPort: true
SimulatedProdUsesDedicatedContainer: true
SimulatedProdUsesDedicatedComposeProject: true

NonProduction:

- Compose project: `crm`
- Service/container: `crm-api`
- Host port: `8093`
- Readiness: HTTP 200 Healthy

Simulated Production:

- Compose project: `crm-prod-sim`
- Container: `crm-api-prod-sim`
- Host binding: `127.0.0.1:8094`
- Network: `crm-prod-sim-net`
