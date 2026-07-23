# CRM Auth Runtime Activation Gates

Current status: `PortalAuthRuntimeContractValidation`.

Before productive Auth activation:

1. Portal runtime user contract approved.
2. Portal tenant context contract approved.
3. Portal permission policy result contract approved.
4. Correlation id propagation approved.
5. CRM middleware integration approved.
6. No CRM credential storage confirmed.
7. No CRM role/permission persistence confirmed.
8. Productive API routes remain behind disabled flag.

Next Gate: `Sprint3P5ProductiveApiRouteDraftBehindDisabledFlag`.

Sprint 3 P5 keeps Auth Runtime Enabled: `false` and Productive Authorization Enabled: `false` while drafting productive API route contracts.
