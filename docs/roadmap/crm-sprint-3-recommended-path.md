# CRM Sprint 3 Recommended Path

Recommended path: start with `Sprint3P1DurablePersistenceSetupDesign`.

Sequence:
1. Design non-production durable persistence without activating it.
2. Define common DB connection and secret strategy.
3. Prototype EF/DbContext behind a disabled flag.
4. Validate Portal Auth runtime contract.
5. Draft productive API routes behind a disabled flag.
6. Run Sprint 3 productization review.

Constraint: no productive CRM behavior until a formal GO.

## Sprint 3 P1 status

P1 starts durable persistence setup as `DesignOnly`. Real DB, EF runtime, migrations and connection strings remain not configured.

Next gate: `Sprint3P2CommonDbConnectionContractAndSecretStrategy`.
