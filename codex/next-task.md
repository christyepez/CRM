# Next Task

CRM Sprint 18 S18-05 - Case Test and Guardrail Hardening

Base Main Commit: Sprint 18 S18-04 merge commit required
Branch: crm-sprint-18-s18-05-case-test-guardrail-hardening
Prompt: codex/prompts/sprint-18-case-management-s18-05.md
Suggested commit: test(crm): harden case foundation guardrails
PR title: CRM Sprint 18 S18-05 - Case Test and Guardrail Hardening

## Guardrails
- Keep Case runtime foundation-only and synthetic.
- No productive Case route and no DELETE.
- No Customer creation/mutation, assignment runtime, SLA runtime or notifications runtime.
- Portal Auth/users runtime, token/header reads and token storage remain disabled.
- Common DB/EF/migrations/schema/SQL/real data remain disabled.
- External connectors, `crm-prod-sim`, port 8094 and real Production remain disabled.
