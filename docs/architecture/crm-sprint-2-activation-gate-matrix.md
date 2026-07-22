# CRM Sprint 2 Activation Gate Matrix

| Gate | Current state | Ready | Minimum requirement |
| --- | --- | --- | --- |
| DB real | Not configured | No | Shared SQL strategy, migrations, rollback, backup/restore, retention and secrets. |
| Portal Auth runtime | Simulation only | No | Portal-owned user, tenant and permission runtime adapter. |
| Productive CRUD API | Foundation only | No | Real Auth, durable persistence, audit, observability and data policy. |
| Audit | Planned via Portal | No | Event taxonomy, retention and correlation. |
| Observability | Foundation | No | Health, logs, metrics, alerts and dashboards. |
| Feature flags | Foundation flags | No | Controlled rollout and rollback flags. |
