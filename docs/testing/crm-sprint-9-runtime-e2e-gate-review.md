# CRM Sprint 9 Runtime E2E Gate Review

Sprint 9 P1 E2E scope is foundation-only.

Checks:
- health/live/ready are OK.
- readiness is OK.
- Sprint 8 gate endpoint is OK.
- Sprint 9 controlled runtime activation decision endpoint is OK.
- leads/accounts/contacts productive routes remain 404 by default.
- locked route behavior remains allowed only under explicit NonProduction fixture/flag and returns 423.
