# CRM Sprint 10 P4 - Controlled Runtime Integration Design

Status: designed contract only.

This package designs a future controlled NonProduction runtime pilot between CRM and Portal after CRM Sprint 10 P3 and Portal Sprint 21. It does not activate runtime coupling, Portal routes, productive navigation, Common DB runtime, SSO/OIDC, real providers, secrets, certificates or production.

## Decision

- CrmSprint10P4ControlledRuntimeIntegrationDesignExists: true.
- CrmSprint10P3PortalConsumerAlignmentReviewed: true.
- PortalSprint21ContractAlignmentReviewed: true.
- ProductizationStatus: PreparationOnly.
- ProductionActivationDecision: NoGo.
- CrmProductionReady: false.
- ControlledRuntimeIntegrationDesignAttempted: true.
- ControlledRuntimeIntegrationDesignReadiness: DesignedContractOnly.
- NextGate: CrmSprint10P5ControlledRuntimePilotScaffold.

## Scope

- Future runtime topology.
- Controlled NonProduction activation sequence.
- Rollback, preflight, health/smoke and observability design.
- Gateway/navigation, Auth/claims, Common DB and crosscutting boundaries.

## Out of scope

Runtime implementation, production activation, real Portal URLs, real SSO/OIDC, client secrets, token storage, Portal services in CRM compose, productive Gateway routes, productive Portal navigation, Common DB runtime, shared tables, cross-domain migrations and direct Portal database access.
