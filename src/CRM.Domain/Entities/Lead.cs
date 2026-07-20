using CRM.Domain.Common;
using CRM.Domain.Enums;
using CRM.Domain.Events;
using CRM.Domain.ValueObjects;

namespace CRM.Domain.Entities;

public sealed class Lead
{
    private readonly List<DomainEvent> _domainEvents = [];

    private Lead(CrmId id, PersonName name, EmailAddress email, CompanyName? company)
    {
        Id = id;
        Name = name;
        Email = email;
        Company = company;
        Status = LeadStatus.New;
    }

    public CrmId Id { get; }
    public PersonName Name { get; }
    public EmailAddress Email { get; }
    public CompanyName? Company { get; }
    public LeadStatus Status { get; private set; }
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public static Lead Create(CrmId id, PersonName name, EmailAddress email, CompanyName? company, DateTimeOffset occurredAtUtc)
    {
        var lead = new Lead(id, name, email, company);
        lead._domainEvents.Add(new LeadCreatedDomainEvent(id, occurredAtUtc));
        return lead;
    }

    public void Qualify()
    {
        if (Status is LeadStatus.Disqualified or LeadStatus.Converted)
        {
            throw new InvalidOperationException("Only active leads can be qualified.");
        }

        Status = LeadStatus.Qualified;
    }
}
