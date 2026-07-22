# CRM Sprint 1 Open Risks

- Persistence activation could break boundaries if CRM jumps directly to SQL without design review.
- Portal authorization must be approved before productive CRM APIs.
- Financiero integration must remain API/events based; shared DB is blocked.
- Reporting requires Portal authorization and BI platform governance.
- Docker build depends on external MCR registry availability.
- UI productization must avoid local auth/token storage.
