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

## Sprint 3 P2 status

P2 defines the common DB connection and secret strategy as `ContractOnly`. Real DB, secret provider runtime, EF runtime, migrations and connection string values remain not configured.

Next gate: `Sprint3P3EfDbContextPrototypeBehindDisabledFlag`.
# Sprint 3 P3 completed path

P3 adds the EF/DbContext prototype behind disabled flags. Next Gate: `Sprint3P4PortalAuthRuntimeContractValidation`.

# Sprint 3 P4 completed path

P4 validates Portal Auth runtime contracts without real Auth activation. Next Gate: `Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag`.

# Sprint 3 P5 completed path

P5 creates the productive API route draft without registering active productive routes. Next Gate: `Sprint3P6Sprint3ProductizationReview`.
# Sprint 3 P6 result

P6 completed the productization review and recommends no real activation. Sprint 4 should begin with runtime environment readiness and local tooling hardening.
