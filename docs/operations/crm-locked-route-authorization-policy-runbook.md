# CRM Locked Route Authorization Policy Runbook

Default local run:

1. Keep `Crm:ProductiveRoutes:LockedRegistrationEnabled=false`.
2. Keep `Crm:ProductiveRoutes:LockedAuthorizationPolicyEnabled=false`.
3. Verify `/api/crm/leads`, `/api/crm/accounts`, `/api/crm/contacts` return 404.
4. Verify the P5 foundation endpoint reports disabled/fail-closed status.

Controlled NonProduction test:

1. Enable locked registration only in a disposable NonProduction fixture.
2. Optionally enable locked authorization policy in the fixture.
3. Validate GET/POST/PUT/PATCH return 423.
4. Validate DELETE does not exist.
5. Validate no side effects, DB calls, Portal HTTP, token reads, or header reads occur.

Do not use production data, real tokens, real secrets, or private Portal URLs.
