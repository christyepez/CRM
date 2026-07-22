# CRM Sprint 2 P5 Open Risks

| Risk | Severity | Blocks productization | Mitigation |
| --- | --- | --- | --- |
| Foundation CRUD mistaken for productization | High | Yes | Keep warnings, foundation route prefix and architecture tests. |
| Durable persistence without migration governance | High | Yes | Require migration, rollback, retention, backup and restore plans. |
| Auth runtime bypassing Portal ownership | High | Yes | Require Portal-owned authorization integration and audit trail. |
| Docker registry timeout | Medium | No | Track as `BLOCKED_EXTERNAL_REGISTRY`, not application failure. |
