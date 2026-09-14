# CRM Sprint 24 S24-02 - Assignment Reference Application + Foundation Store

Status: Implemented locally

## Scope
- Application read/write service for assignment references.
- Typed synthetic in-memory foundation store.
- Deterministic seed record.
- DI registration in CRM.Api.
- No assignment API routes yet.

## Safety
- ProductiveCrudEnabled: false
- PortalIdentityRuntimeEnabled: false
- No Portal Security calls.
- No users, roles or permissions are stored in CRM.
- No Common DB / EF / SQL / real data.
- No DELETE.
