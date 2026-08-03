# CRM Common DB Controlled Activation Risk Register

| Risk | Impact | Mitigation | Status |
| --- | --- | --- | --- |
| CRM points to Portal-owned tables | Domain boundary breach | Require CRM logical database and reject Portal direct DB access | Open |
| Connection material leaks into repo | Secret exposure | Use logical secret references only; scan before PR | Controlled |
| Migrations run before approval | Schema drift | No migrations in P2; future scripts require explicit gate | Controlled |
| Portal Sprint 21 contract changes | Rework | Keep this plan contract-only and reference alignment gate | Open |
| DB runtime accidentally enabled | Data/write risk | Fail-closed defaults and guardrail script | Controlled |

## Markers

- CommonDbRuntimeEnabled: false.
- RealConnectionStringsPresent: false.
- SharedPortalTablesAccessEnabled: false.
- CrossDomainMigrationsPresent: false.
- PortalDatabaseDirectAccessEnabled: false.
- ProductionActivationDecision: NoGo.
