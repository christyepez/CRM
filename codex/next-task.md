# Next Task

CRM Sprint 18 S18-06 - Case Local Integration Validation

Base Main Commit: Sprint 18 S18-05 merge commit required
Branch: crm-sprint-18-s18-06-case-local-integration-validation
Prompt: codex/prompts/sprint-18-case-management-s18-06.md
Suggested commit: test(crm): validate case local integration
PR title: CRM Sprint 18 S18-06 - Case Local Integration Validation

## Guardrails
- Validate only foundation Case runtime locally with synthetic data.
- No productive Case route and no DELETE.
- No Customer mutation, assignment/SLA/notification runtime.
- Portal Auth/users runtime and token handling remain disabled.
- Common DB/EF/migrations/schema/SQL/real data remain disabled.
- External connectors, `crm-prod-sim`, port 8094 and Production remain untouched.
