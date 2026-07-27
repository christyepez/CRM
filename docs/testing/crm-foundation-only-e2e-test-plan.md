# CRM Foundation-Only E2E Test Plan

Run the pilot only against foundation endpoints and synthetic/non-real data.

Plan:

1. Build and start Docker.
2. Validate health/readiness endpoints.
3. Validate Sprint 3/4 foundation status endpoints.
4. Validate productive route negative checks.
5. Run guardrails and foundation verifier.
6. Capture evidence commands and HTTP results.

Do not use DB, Auth runtime, Portal runtime, productive routes or DELETE.
