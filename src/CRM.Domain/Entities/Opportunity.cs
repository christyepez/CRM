using CRM.Domain.Common;
using CRM.Domain.Enums;
using CRM.Domain.Events;
using CRM.Domain.ValueObjects;

namespace CRM.Domain.Entities;

public sealed class Opportunity
{
    private readonly List<DomainEvent> _domainEvents = [];

    private Opportunity(CrmId id, CompanyName accountName, MoneyAmount expectedValue, Probability probability)
    {
        Id = id;
        AccountName = accountName;
        ExpectedValue = expectedValue;
        Probability = probability;
        Status = OpportunityStatus.Open;
    }

    public CrmId Id { get; }
    public CompanyName AccountName { get; }
    public MoneyAmount ExpectedValue { get; }
    public Probability Probability { get; private set; }
    public OpportunityStatus Status { get; private set; }
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public static Opportunity Create(CrmId id, CompanyName accountName, MoneyAmount expectedValue, Probability probability) =>
        new(id, accountName, expectedValue, probability);

    public void MarkWon(DateTimeOffset occurredAtUtc)
    {
        if (Status != OpportunityStatus.Open)
        {
            throw new InvalidOperationException("Only open opportunities can be won.");
        }

        Status = OpportunityStatus.Won;
        Probability = Probability.From(100);
        _domainEvents.Add(new OpportunityWonDomainEvent(Id, occurredAtUtc));
    }
}
