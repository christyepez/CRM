# CRM Portal Readiness Checklist

Before enabling real Portal integration:

- Portal consumer contracts are published and versioned.
- Security/Auth and permission checks are available through Portal.
- Menu registration process is approved by Portal.
- Audit, notification and configuration adapters have sandbox endpoints.
- Gateway routing is approved.
- Correlation id propagation is tested end to end.
- No CRM-owned login, identity or Portal capability duplication exists.
- Secrets and environment-specific URLs are provided outside the repository.

Current P5 result: contracts only; no runtime calls configured.
