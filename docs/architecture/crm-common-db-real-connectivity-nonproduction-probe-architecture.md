# CRM Common DB Real Connectivity NonProduction Probe Architecture

CRM does not own SQL Server containers and does not own cross-platform persistence infrastructure. P3 introduces a contract-only Common DB real connectivity probe that is skipped until Secret Provider real approval is granted.

Architecture rules:
- reuse the common SQL Server environment later; do not add CRM SQL Server compose;
- keep logical database ownership separate as `CrmDb`;
- do not enable EF runtime or migrations in P3;
- do not activate productive CRM routes;
- keep Portal authorization external.

The next gate is `Sprint7P4PortalAuthRealRuntimeProbe`.
