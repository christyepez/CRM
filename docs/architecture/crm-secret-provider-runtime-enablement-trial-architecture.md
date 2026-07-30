# CRM Secret Provider Runtime Enablement Trial Architecture

P2 wraps the existing `ISecretProviderRuntime` abstraction with `SecretProviderRuntimeTrialService`.

Boundary:
- Application exposes decision/status contracts.
- Infrastructure owns the runtime trial adapter.
- API exposes read-only status and a locked/sanitized probe.

The adapter enforces:
- NonProduction-only.
- Explicit flag.
- Allow-list.
- Metadata-only response.
- Production blocked.
- No DB, EF, Portal Auth, token/header reads, productive routes, CRUD or DELETE.
