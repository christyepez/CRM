# CRM Sprint 24 S24-01 - Assignment Reference Contracts and Domain Rules

Status: Implemented

Implemented deterministic CRM assignment-reference contracts and policy.

Rules: generated GUID identity, structural CRM related-entity reference, required opaque assignee reference max 200, optional label max 120, Active/Archived lifecycle, normalized no-change suppression and idempotent archive.

Portal Security remains authoritative for user, role, identity and authorization semantics. The domain does not resolve or mutate Portal identities.

Safety: foundation-only scope; no API/frontend yet; no productive routes, DELETE, Common DB/EF/SQL, connectors, real data, port 8094 or Production.
