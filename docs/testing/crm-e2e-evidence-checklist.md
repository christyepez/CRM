# CRM E2E Evidence Checklist

Required evidence:

- [ ] `docker compose ps`.
- [ ] Health endpoint results.
- [ ] Sprint 3/4 foundation endpoint results.
- [ ] Negative checks for `/api/crm/leads`, `/api/crm/accounts`, `/api/crm/contacts`.
- [ ] `tools/check-crm-guardrails.ps1`.
- [ ] `tools/verify-crm-foundation.ps1`.
- [ ] `tools/check-crm-e2e-foundation.ps1`.
- [ ] Confirmation that no real data was used.
- [ ] Confirmation that no DB/Auth/Portal runtime was activated.
