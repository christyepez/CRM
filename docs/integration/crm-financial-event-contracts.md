# CRM Financial Event Contracts

Conceptual events prepared in P6:

- `CustomerConvertedForFinancialIntegration`
- `OpportunityWonForFinancialIntegration`
- `InvoiceCreatedFinancialSignal`
- `PaymentOverdueFinancialSignal`
- `CollectionsRiskRaisedFinancialSignal`

These are foundation contracts only. There is no broker, outbox, queue, topic, publication runtime or Financiero callback.

Future delivery must decide API vs events per use case and preserve `NoSharedDatabase`.
