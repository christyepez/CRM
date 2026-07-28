# CRM Secret Provider Safe Mock Contract

The mock contract exposes logical names and synthetic values only.

| Logical name | Synthetic value | Synthetic | Sensitive | Runtime usable |
| --- | --- | --- | --- | --- |
| `crm.common-db` | `mock://crm/common-db` | true | false | false |
| `crm.portal-auth-base-url` | `mock://crm/portal-auth-base-url` | true | false | false |
| `crm.client-id` | `mock-client-id` | true | false | false |
| `crm.client-secret` | `mock-client-secret-not-real` | true | false | false |
| `crm.observability` | `mock://crm/observability` | true | false | false |

The values are not credentials and must not be used to connect to DB, Portal, Auth or observability systems.
