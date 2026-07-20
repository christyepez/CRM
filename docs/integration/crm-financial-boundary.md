# CRM / Financiero Boundary

Financiero integration is deferred and must be contract/event based.

## P2 status

- No direct Financiero calls.
- No hardcoded Financiero URLs.
- No shared database.
- No customer-to-invoice workflow.

## Future candidates

- Customer conversion event.
- Opportunity won event.
- Account/customer reference contract.
- Portal Integration API or outbox-mediated adapter.

## Guardrail

CRM must not depend on Financiero internals. Any future integration must be versioned, explicit and auditable through Portal capabilities.
