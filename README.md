# CRM Corporativo

## Sprint 1 P1 - Foundation Architecture Baseline

CRM starts as an independent repository foundation using .NET 8, Angular and Docker Compose. The current runtime is non-production and foundation-only.

### Local execution

```powershell
dotnet restore CRM.sln
dotnet build CRM.sln
dotnet test CRM.sln
docker compose config
docker compose up -d --build
Invoke-WebRequest -UseBasicParsing http://localhost:8093/health
Invoke-WebRequest -UseBasicParsing http://localhost:8093/api/crm/readiness
```

Frontend:

```powershell
cd frontend/crm-web
pnpm install --frozen-lockfile
pnpm run build
pnpm test
```

### Foundation endpoints

- `GET /health`
- `GET /health/live`
- `GET /health/ready`
- `GET /api/crm/readiness`

Readiness returns `module=CRM`, `status=ReadyForFoundationOnly`, `portalIntegration=Planned`, `financialIntegration=Planned` and `runtimeMode=NonProduction`.

### Guardrails

- No login, Identity, token storage or roles propios.
- No SQL Server propio in Docker Compose.
- No CRM CRUD endpoints yet.
- No hardcoded integration with Financiero.
- Portal Auth/Menu/permissions/Audit/Notification/Configuration remain planned reuse points.

Módulo CRM corporativo integrado a `PortalCorporativo`.

## Objetivo

Implementar capacidades de gestión comercial y relación con clientes reutilizando las APIs transversales del portal y evitando duplicación de seguridad, auditoría, notificaciones, menús, configuración visual, catálogos y reporting.

## Repos relacionados

```text
PortalCorporativo: https://github.com/christyepez/PortalCorporativo
CRM: https://github.com/christyepez/CRM
CodexCommonAgents: https://github.com/christyepez/CodexCommonAgents
```

## Capacidades propias CRM

Este repositorio puede crear componentes propios para:

- Customers.
- Contacts.
- Leads.
- Opportunities.
- Activities.
- Cases.
- Campaigns.
- Interactions.
- CRM Integration Hub.
- Salesforce/Dynamics/Generic REST connectors.
- Mapeos y transacciones de integración.

## Capacidades reutilizadas del portal

CRM debe reutilizar o extender:

- Security API.
- Menu API.
- Configuration API.
- Catalog API.
- Audit API.
- Notification API.
- Content/File API.
- Reporting API.
- Integration API base.
- Portal Angular Shell.
- API Gateway.

## Reglas Codex

Codex debe leer primero:

1. `AGENTS.md`.
2. `codex/COORDINADOR_SOLUCION.md`.
3. `codex/INSTRUCTIONS.md`.
4. `codex/ARCHITECTURE_RULES.md`.
5. `codex/PORTAL_INTEGRATION_CONTRACTS.md`.
6. `codex/TASKS.md`.

## Clasificación obligatoria

Toda tarea debe clasificar sus componentes como:

```text
REUSE   = usar componente del portal.
EXTEND  = extender configuración, permisos, catálogos, menús o contratos del portal.
ADAPT   = crear adaptador hacia API/servicio del portal.
CREATE  = crear componente propio del dominio CRM.
BLOCKED = no implementar hasta revisar portal.
```

## Principio de integración

CRM Core no debe acoplarse directamente a Salesforce, Dynamics u otro CRM externo. Toda integración externa debe pasar por `CRM Integration Hub`.

## Modo bajo consumo de tokens

No leer todo el repositorio si la tarea no lo requiere. Usar `AGENTS.md`, `codex/PORTAL_INTEGRATION_CONTRACTS.md`, `codex/TASKS.md` y el playbook aplicable de `CodexCommonAgents`.
