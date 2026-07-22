# CRM Financial Adapter Contracts

CRM Sprint 1 P6 defines future adapter contracts for Financiero without enabling runtime integration.

## Status

- Module: CRM
- Integration mode: Planned
- Runtime mode: NonProduction
- Connected: false
- Capability owner: Financiero
- Integration patterns: API, Events, NoSharedDatabase

## Ports

CRM declares Application ports for customer reference lookup, account status, invoice awareness, payment status, collections signals and conceptual financial event publishing. These ports are marked as `FutureFinancialAdapter`.

## What CRM does not implement

CRM does not implement financial invoices, collections, SRI, ATS, RIDE, XAdES, shared tables, shared databases, direct Financiero queries, connection strings or runtime Financiero HTTP calls.

## Future integration requirement

Real adapters must be added only after Financiero publishes stable API/event contracts and Portal-controlled security rules are approved.
