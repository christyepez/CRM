# CRM Foundation Store Contracts

Foundation stores expose only preview methods:

- `GetPreviewAsync`.
- `SavePreviewAsync`.
- `ClearPreviewAsync`.
- `GetStatusAsync`.

Ports live in `src/CRM.Application/Ports/Persistence` and use `FoundationStore` naming to avoid implying productive repositories.

Adapters live in `src/CRM.Infrastructure/Persistence/Foundation` and keep data only in memory. The current stores are Lead, Account and Contact. No external I/O, DB, files, connection configuration or real customer data is allowed.
