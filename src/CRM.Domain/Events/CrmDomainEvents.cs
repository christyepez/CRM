using CRM.Domain.Common;

namespace CRM.Domain.Events;

public sealed record LeadCreatedDomainEvent(CrmId AggregateId, DateTimeOffset OccurredAtUtc) : DomainEvent(AggregateId, OccurredAtUtc);

public sealed record OpportunityWonDomainEvent(CrmId AggregateId, DateTimeOffset OccurredAtUtc) : DomainEvent(AggregateId, OccurredAtUtc);

public sealed record CustomerConvertedDomainEvent(CrmId AggregateId, DateTimeOffset OccurredAtUtc) : DomainEvent(AggregateId, OccurredAtUtc);

public sealed record FollowUpScheduledDomainEvent(CrmId AggregateId, DateTimeOffset OccurredAtUtc) : DomainEvent(AggregateId, OccurredAtUtc);
