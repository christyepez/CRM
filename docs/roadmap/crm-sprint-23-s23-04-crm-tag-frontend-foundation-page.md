# CRM Sprint 23 S23-04 - CRM Tag Frontend Foundation Page

Status: Implemented
S2304Decision: Implemented

Angular route /foundation/tags consumes only /api/crm/foundation/tags.
Supports list/detail/create/update/archive and optional structural entity reference.
Archived tags are read-only. Safe 400/404/409 messages are displayed.
No DELETE, productive tag route, Portal identity/config runtime or related-entity mutation.
