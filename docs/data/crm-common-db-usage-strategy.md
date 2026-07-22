# CRM Common DB Usage Strategy

CRM will target the shared local database infrastructure only when approved by a later gate.

Rules:
- One SQL Server container per local environment, owned by platform composition, not CRM.
- Logical CRM database separate from PortalCorporativo, Financiero and future domains.
- No shared database tables across bounded contexts.
- No direct reads of Portal security tables.
- No direct reads of Financiero data.

Sprint 3 P1 does not add SQL Server, database creation scripts or runtime settings.
