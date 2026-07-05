# CRM Integration Hub

## Proposito

Capa transaccional para integrar CRM con Salesforce, Dynamics 365 / Dataverse u otros CRMs.

## Patrones

- Anti-Corruption Layer
- Canonical Data Model
- Connector Pattern
- Outbox e Inbox
- Idempotencia
- Reintentos
- Resolucion de conflictos

## Conectores iniciales

- Generic REST Connector
- Salesforce Connector stub
- Dynamics Dataverse Connector stub

## Entidades

ExternalSystem, IntegrationConnector, IntegrationTransaction, IntegrationEntityMapping, IntegrationFieldMapping, ExternalEntityReference, IntegrationConflict, IdempotencyKey, OutboxMessage e InboxMessage.
