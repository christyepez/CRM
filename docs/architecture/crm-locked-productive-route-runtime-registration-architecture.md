# CRM Locked Productive Route Runtime Registration Architecture

P5 introduces a small API registrar that can map future productive CRM route shapes as locked NonProduction stubs.

Architecture decisions:

- Registration is disabled by default.
- Registration is blocked in Production.
- Locked handlers return `423` directly from API infrastructure.
- Handlers do not inject application services, domain services, stores, DB clients, Portal clients or authorization runtime.
- `DELETE` is excluded.
- Foundation status remains available independently from locked route registration.

This keeps the route contract testable while preserving Portal ownership of Auth and avoiding accidental CRM productization.
