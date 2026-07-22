# CRM Sprint 2 Productization Decision Matrix

| Capability | Owner | Current status | Evidence | Decision | Required before GO | Target sprint | Risk |
|---|---|---|---|---|---|---|---|
| Durable Persistence | CRM Data Architect | NonProductionSeam | No DB configured | NoGo | Schema, rollback, backup/restore | Sprint3P1 | High |
| Real DB / Common DB | DevOps + Portal | NotConfigured | No SQL Server in CRM compose | NoGo | Common DB contract and secrets | Sprint3P2 | High |
| EF/Migrations | CRM Backend | NotCreated | No DbContext/DbSet/migrations | NoGo | Disabled prototype approval | Sprint3P3 | High |
| Portal Auth Runtime | PortalCorporativo | Simulated | FoundationSimulation only | NoGo | Runtime permission contract | Sprint3P4 | High |
| Portal Permissions Runtime | PortalCorporativo | Simulated | No Portal HTTP client | NoGo | Audited permission evaluation | Sprint3P4 | High |
| Productive CRUD API | CRM Backend | Disabled | No productive routes | NoGo | DB + Auth + audit gates | Sprint3P5 | High |
| Productive CRM UI | CRM Frontend | NotImplemented | Readiness only | NoGo | Productive API and auth | Sprint3+ | High |
| Audit Runtime | PortalCorporativo | Planned | No runtime adapter | NoGo | Portal audit contract | Sprint3+ | Medium |
| Notification Runtime | PortalCorporativo | Planned | No runtime adapter | NoGo | Notification policy | Sprint3+ | Medium |
| Reporting Runtime | Reporting/BI | Planned | Foundation catalog only | NoGo | Analytics governance | Sprint3+ | Medium |
| Financial Runtime | Financiero | Planned | Contracts only | NoGo | Cross-domain contract | Sprint3+ | Medium |
| Docker Runtime | DevOps | ConfigValid | crm-api 8093 only | GoForConfigOnly | MCR availability | Sprint3 | Medium |
| Observability | Platform | Planned | Health checks only | NoGo | Metrics/logging contract | Sprint3+ | Medium |
| Secrets Management | DevOps/Security | NotConfigured | No secrets committed | NoGo | Secret store contract | Sprint3P2 | High |
| Backup/Restore | Data Architect | NotDefined | No durable DB | NoGo | RPO/RTO policy | Sprint3P1 | High |
| Test Data Management | QA/Data | Planned | No real personal data | NoGo | Synthetic data rules | Sprint3 | Medium |
