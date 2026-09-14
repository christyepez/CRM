# CRM Sprint 24 P1 - CRM Assignment Reference Foundation

Define the functional baseline for `CrmAssignment` from repository evidence.

Ownership boundary: CRM may own assignment metadata/reference only. Portal Security remains authoritative for users, roles, identity and authorization.

Baseline proposal: Id generated GUID, RelatedEntityType + RelatedEntityId structural reference, AssigneeReferenceId opaque required reference, optional assignment role/label metadata, status Active/Archived.

Lifecycle: create Active, update Active only, normalized no-change => Changed=false, archive idempotent, Archived read-only.

Foundation only, synthetic data, no productive routes, no DELETE, no Portal Security runtime, no Common DB/EF/SQL, no connectors, no real data, no port 8094 or Production.

Create P1 backlog S24-01 through S24-07 using the established sprint pattern.
