# CRM Sprint 3 Security No-Go Review

Security decision: `NoGo`.

CRM still must not create login/logout, Identity, JWT/cookie auth, token storage, persisted CRM roles or Portal Auth replacement.

Portal Auth Runtime: `NoGo`.

Portal Permissions Runtime: `NoGo`.

Required before GO: PortalCorporativo runtime authorization probe behind disabled flag, negative permission tests and no CRM-owned identity.
