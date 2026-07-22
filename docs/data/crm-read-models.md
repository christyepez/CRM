# CRM Read Models

P4 introduces read model contracts and foundation mock services only.

## Read model contracts

- LeadReadModel.
- AccountReadModel.
- ContactReadModel.
- CrmReadModelQuery.
- CrmReadModelResponse.
- CrmReadModelStatusResponse.

## Runtime response policy

- source: FoundationMock.
- persistence: None.
- runtimeMode: NonProduction.
- warning: Read model preview only, not persisted.

## Future approach

Read models should support list/search screens only after persistence is approved. They must not bypass Portal Security, Audit or Configuration.
