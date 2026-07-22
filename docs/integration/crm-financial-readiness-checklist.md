# CRM Financial Readiness Checklist

Before enabling real CRM-Financiero integration:

- Financiero API/event contracts are published and versioned.
- Portal authorization rules for CRM financial awareness are approved.
- No shared database or cross-domain SQL queries exist.
- Event idempotency and correlation id rules are defined.
- NonProduction adapters are tested before production routes are considered.
- Secrets and environment-specific URLs are configured outside the repository.
- CRM does not implement invoices, collections, SRI, ATS, RIDE or XAdES.

Current P6 result: contracts only; no runtime calls configured.
