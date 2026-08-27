# CRM P47W NonProduction Isolation Evidence

NonProdUnaffected: true
SeparateComposeProject: true
SeparateContainer: true
SeparatePort: true
SeparateNetwork: true

NonProduction:

- Project: `crm`
- Port: `8093`
- Readiness: HTTP 200

SimulatedProduction:

- Project: `crm-prod-sim`
- Container: `crm-api-prod-sim`
- Network: `crm-prod-sim-net`
- Port binding: `127.0.0.1:8094->8080`
