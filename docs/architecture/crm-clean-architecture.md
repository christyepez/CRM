# CRM Clean Architecture

## Layers

| Layer | Project | Responsibility |
|---|---|---|
| Domain | `src/CRM.Domain` | Domain constants, entities and rules when CRM core starts. |
| Application | `src/CRM.Application` | Use cases, DTOs, ports and orchestration. |
| Infrastructure | `src/CRM.Infrastructure` | Future adapters to Portal, persistence and external integrations. |
| API | `src/CRM.Api` | HTTP host and foundation endpoints. |

## Dependency rules

- Domain depends on no CRM layer.
- Application may depend on Domain.
- Infrastructure may depend on Application and Domain.
- API may depend on Application and Infrastructure.
- Domain must not reference API, Infrastructure, Identity or persistence.

## Current endpoints

- `GET /health`
- `GET /health/live`
- `GET /health/ready`
- `GET /api/crm/readiness`

No mutating business endpoints exist in P1.
