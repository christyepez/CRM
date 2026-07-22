# CRM Common DB Logical Naming

Suggested logical database placeholder: `CrmDb`.

Rules:
- `CrmDb` is a placeholder name, not a connection value.
- CRM must not create a SQL Server container.
- CRM must not share tables or schemas with Financiero.
- CRM must not duplicate Portal user, tenant or permission tables.
- Final naming must be approved by the common DB gate before runtime use.
