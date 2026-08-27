# CRM OPS-04 Local Simulated Production Architecture

This architecture supersedes no real Production design. The Azure Container Apps path remains deferred for future real enterprise Production. OPS-04 provisions only a local Docker Compose simulated Production target.

Traffic:

`127.0.0.1:8094 -> crm-api-prod-sim:8080`

Isolation:

- Compose project: `crm-prod-sim`
- Container: `crm-api-prod-sim`
- Network: `crm-prod-sim-net`
- Image: `crm-api:prod-candidate-8623c619`
- No `build:` directive.
- No Portal service.
- No Common DB service.
- No SQL Server service.

Rollback:

`docker compose -p crm-prod-sim --env-file .env.prod-sim.example -f docker-compose.prod-sim.yml down`

The rollback target is `PreDeploymentNoCRMState` for the simulated Production environment.
