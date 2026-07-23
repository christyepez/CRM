# CRM Sprint 3 Open Risks

- Productive API routes could be confused with foundation CRUD unless route registration stays blocked.
- Real database activation requires common SQL usage, secret provider validation and rollback planning.
- Portal authorization must remain owned by PortalCorporativo; CRM must not add login or token storage.
- DELETE requires audit, retention, backup/restore and legal/data governance approval.
- Productive CRM UI must wait for productive API, Auth and persistence gates.
