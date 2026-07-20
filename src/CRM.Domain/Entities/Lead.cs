using CRM.Domain.Common;
using CRM.Domain.Enums;
using CRM.Domain.Events;
using CRM.Domain.ValueObjects;

namespace CRM.Domain.Entities;

public sealed class Lead
{
    private readonly List<DomainEvent> _domainEvents = [];

    private Lead(CrmId id, PersonName name, EmailAddress email, CompanyName? company, LeadSource? source, CrmId? campaignId)
    {
        Id = id;
        Name = name;
        Email = email;
        Company = company;
        Source = source;
        CampaignId = campaignId;
        Status = LeadStatus.New;
    }

    public CrmId Id { get; }
    public PersonName Name { get; }
    public EmailAddress Email { get; }
    public CompanyName? Company { get; }
    public LeadSource? Source { get; }
    public CrmId? CampaignId { get; }
    public DisqualificationReason? DisqualificationReason { get; private set; }
    public LeadStatus Status { get; private set; }
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public static Lead Create(CrmId id, PersonName name, EmailAddress email, CompanyName? company, DateTimeOffset occurredAtUtc, LeadSource? source = null, CrmId? campaignId = null)
    {
        var lead = new Lead(id, name, email, company, source, campaignId);
        lead._domainEvents.Add(new LeadCreatedDomainEvent(id, occurredAtUtc));
        return lead;
    }

    public void MarkContacted()
    {
        EnsureStatus(LeadStatus.New, "Only new leads can be marked as contacted.");
        Status = LeadStatus.Contacted;
    }

    public void Qualify()
    {
        EnsureStatus(LeadStatus.Contacted, "Only contacted leads can be qualified.");
        Status = LeadStatus.Qualified;
    }

    public void Convert()
    {
        EnsureStatus(LeadStatus.Qualified, "Only qualified leads can be converted.");
        Status = LeadStatus.Converted;
    }

    public void Disqualify(DisqualificationReason reason)
    {
        if (Status is LeadStatus.Converted or LeadStatus.Disqualified)
        {
            throw new InvalidOperationException("Converted or already disqualified leads cannot be disqualified.");
        }

        DisqualificationReason = reason;
        Status = LeadStatus.Disqualified;
    }

    private void EnsureStatus(LeadStatus expected, string message)
    {
        if (Status != expected)
        {
            throw new InvalidOperationException(message);
        }
    }
}
