# CRM Sprint 12 P1 - Contact Management Functional Baseline and Backlog

Repository:
https://github.com/christyepez/CRM

Objective:
Plan the Contact Management Foundation slice after Sprint 11 Lead Qualification closure, without implementing runtime behavior yet.

Base:
S11-07 merge commit required.

Expected branch:
crm-sprint-12-p1-contact-management-functional-baseline-backlog

Suggested commit:
docs(crm): plan contact management foundation slice

PR title:
CRM Sprint 12 P1 - Contact Management Functional Baseline and Backlog

Scope:

- Validate main contains the S11-07 merge commit.
- Review existing Contact domain concepts, value objects, foundation preview service, API surface and frontend state.
- Define the Contact Management Foundation slice boundary.
- Decide whether Sprint 12 starts with contracts/domain rules or application/API foundation work.
- Create Sprint 12 backlog and implementation order.
- Preserve foundation-only runtime.
- Keep Productive routes, Portal Auth runtime, Common DB runtime and SimulatedProduction untouched.

Guardrails:

- Do not unlock productive `/api/crm/contacts` routes.
- Do not activate Portal Auth runtime.
- Do not activate Common DB runtime.
- Do not add migrations, schemas, EF runtime or SQL Server.
- Do not touch `crm-prod-sim`.
- Do not deploy, restart, rollback or rebuild simulated Production.
- Do not add secrets, `.env`, tokens, certificates or real data.
- Do not implement Contact Management in P1 unless explicitly requested by a later task.

Validations:

- `git diff --check`
- `dotnet build CRM.sln`
- `dotnet test CRM.sln --no-build`
- `npm run build`
- `npm run test`
- `tools/check-crm-guardrails.ps1`
- `tools/verify-crm-foundation.ps1`
- `tools/verify-crm-sprint-11-s11-07.ps1`

Expected close:

- Contact Management Foundation baseline documented.
- Sprint 12 backlog created.
- First Sprint 12 implementation story prepared.
- No production, simulated production, Common DB or Portal runtime activation.
