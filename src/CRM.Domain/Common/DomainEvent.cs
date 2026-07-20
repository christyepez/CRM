namespace CRM.Domain.Common;

public abstract record DomainEvent(CrmId AggregateId, DateTimeOffset OccurredAtUtc);

public readonly record struct CrmId
{
    public Guid Value { get; }

    private CrmId(Guid value) => Value = value;

    public static CrmId New() => new(Guid.NewGuid());

    public static CrmId From(Guid value) =>
        value == Guid.Empty ? throw new ArgumentException("CRM id cannot be empty.", nameof(value)) : new(value);

    public override string ToString() => Value.ToString("D");
}
