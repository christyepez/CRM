# CRM Domain Model

Sprint 1 P2 defines a conceptual CRM domain baseline only. It does not add persistence, migrations, production CRUD or real integrations.

## Entities

- Lead: potential customer before qualification.
- Account: organization/customer container.
- Contact: person associated with a lead or account.
- Opportunity: potential revenue tracked through a pipeline.
- Pipeline: sales process.
- PipelineStage: ordered stage in a pipeline.
- Activity: scheduled commercial follow-up.
- Note: internal note linked to a CRM concept.
- Campaign: marketing campaign concept.
- Segment: audience segmentation concept.

## Value objects

- CrmId.
- EmailAddress.
- PhoneNumber.
- PersonName.
- CompanyName.
- MoneyAmount.
- Probability.
- DateRange.

## Events

- LeadCreatedDomainEvent.
- OpportunityWonDomainEvent.
- CustomerConvertedDomainEvent.
- FollowUpScheduledDomainEvent.

## Relationships

- Lead may become Opportunity after qualification.
- Account may have Contacts.
- Pipeline contains PipelineStages.
- Opportunity may have Activities.

Status: Draft.
