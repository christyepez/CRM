# CRM Sprint 12 S12-01 - Contact Contracts and Domain Rules

Repository:
https://github.com/christyepez/CRM

Objective:
Implement explicit Contact Management foundation contracts and domain rules as the first Sprint 12 implementation story.

Base:
Sprint 12 P1 merge commit required.

Expected branch:
crm-sprint-12-s12-01-contact-contracts-domain-rules

Suggested commit:
feat(crm): add contact management domain rules

PR title:
CRM Sprint 12 S12-01 - Contact Contracts and Domain Rules

Scope:

- Validate main contains the Sprint 12 P1 merge commit.
- Add or refine Contact Management domain contracts in `CRM.Domain`.
- Define deterministic Contact create/update/preference rules.
- Define controlled Contact error codes and result model.
- Preserve existing `Contact` entity behavior unless a narrow rule extraction is needed.
- Add domain tests for Contact Management rules.
- Update documentation and task index.
- Prepare S12-02 prompt for Contact Application Service.

Guardrails:

- Do not unlock productive `/api/crm/contacts`.
- Do not add DELETE.
- Do not activate Portal Auth runtime.
- Do not read Authorization headers or tokens.
- Do not activate Common DB runtime.
- Do not add EF runtime, migrations, schema changes or SQL Server.
- Do not touch `crm-prod-sim`.
- Do not deploy, restart, rollback or rebuild simulated Production.
- Do not add secrets, `.env`, tokens, certificates or real data.
- Do not implement Angular UI in S12-01.

Acceptance criteria:

- Contact Management domain rules are explicit and deterministic.
- Existing Contact foundation CRUD remains compatible.
- Productive Contact route remains unavailable.
- New domain tests pass.
- Existing backend/frontend tests remain green.
- Guardrails pass.
- Sprint 12 S12-02 prompt is prepared.
