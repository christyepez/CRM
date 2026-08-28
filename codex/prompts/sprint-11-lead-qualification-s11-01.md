# CRM Sprint 11 S11-01 - Lead Qualification Contracts and Domain Rules

Repository:
https://github.com/christyepez/CRM

Phase:
CRM Sprint 11 S11-01 - Lead Qualification Contracts and Domain Rules

Canonical Base Commit:
b2d09708ada9db76b6e125c22f1b976e3ec2ae4a

Expected Branch:
crm-sprint-11-s11-01-lead-qualification-contracts-domain-rules

Suggested Commit:
feat(crm): add lead qualification foundation contracts

PR Title:
CRM Sprint 11 S11-01 - Lead Qualification Contracts and Domain Rules

Objective:
Add development-safe lead qualification contracts and domain rules for the selected Sprint 11 functional slice without unlocking productive routes or activating Portal/Common DB runtime.

Scope:

- Preserve Sprint 10 simulated Production baseline.
- Add/extend lead qualification contracts in domain/application layers.
- Add unit/architecture tests for validation and guardrails.
- Keep changes small and layered.

Guardrails:

- Do not redeploy, restart or alter `crm-prod-sim`.
- Do not create P51 or another production gate.
- Do not activate real Production or Azure.
- Do not unlock productive `/api/crm/leads`.
- Do not add DELETE endpoints.
- Do not activate Portal Auth runtime.
- Do not read Authorization headers/tokens by default.
- Do not activate Common DB runtime, EF runtime, migrations or schema changes.
- Do not add SQL Server containers.
- Do not add secrets, `.env`, tokens, certificates or real data.

Acceptance Criteria:

- Lead qualification statuses/rules are represented in contracts/domain-safe code.
- Foundation scope remains explicit.
- Productive routes remain locked/404 by default.
- Portal/Common DB dependencies remain disabled or absent.
- Existing 281 tests remain green.

Validation:

- git diff --check
- dotnet build CRM.sln
- dotnet test CRM.sln --no-build
- powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/check-crm-guardrails.ps1
- powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/verify-crm-foundation.ps1
